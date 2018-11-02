using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmCompras : Form
    {
        DatosEntities db = new DatosEntities();
        List<Compra> Lista = new List<Compra>();
        public FrmCompras()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmCompras_Load);
            this.FormClosed += new FormClosedEventHandler(FrmCompras_FormClosed);
        }
        void FrmCompras_FormClosed(object sender, FormClosedEventArgs e)
        {
            Pantallas.ComprasLista = null;
        }
        void FrmCompras_Load(object sender, EventArgs e)
        {
            Busqueda();
            Buscar.Click += new EventHandler(Buscar_Click);
            gridControl1.KeyDown += new KeyEventHandler(gridControl1_KeyDown);
            btnNuevo.Click += new EventHandler(btnNuevo_Click);
            btnVer.Click += new EventHandler(btnVer_Click);
            btnEliminar.Click += new EventHandler(btnEliminar_Click);
            btnAplicarRetencion.Click += new EventHandler(btnAplicarRetencion_Click);
            btnImprimirRetencion.Click += new EventHandler(btnImprimirRetencion_Click);
            txtBuscar.KeyDown += new KeyEventHandler(txtBuscar_KeyDown);
            gridView1.OptionsLayout.Columns.Reset();
        }

        void btnImprimirRetencion_Click(object sender, EventArgs e)
        {
            if (this.bs.Current == null)
                return;
            Compra registro = (Compra)this.bs.Current;
            var retencion = (from item in db.Retenciones
                            where item.NumeroDocumento == registro.Numero
                            select item).FirstOrDefault();
            if (retencion == null)
            {
                MessageBox.Show("Esta compra no tiene retencion");
                return;
            }
            FrmReportes f = new FrmReportes();
            f.ImprimirRetencionDoble(retencion);
        }

        void btnAplicarRetencion_Click(object sender, EventArgs e)
        {
            if (this.bs.Current == null)
                return;
            Compra registro = (Compra)this.bs.Current;            
            FrmRetencionesItem f = new FrmRetencionesItem();
            f.CrearRetencion(registro);
            if (f.DialogResult == DialogResult.OK)
            {
                f.registro.CedulaRif = Basicas.CedulaRif(f.registro.CedulaRif);
                Proveedore proveedor = FactoryProveedores.Item(db, f.registro.CedulaRif);
                if (proveedor == null)
                {
                    proveedor = new Proveedore();
                    proveedor.Activo = true;
                }
                proveedor.CedulaRif = f.registro.CedulaRif;
                proveedor.RazonSocial = f.registro.NombreRazonSocial;
                if (proveedor.IdProveedor == null)
                {
                    proveedor.IdProveedor = FactoryContadores.GetMax("IdProveedor");
                    db.Proveedores.AddObject(proveedor);
                }
                f.registro.Id = FactoryContadores.GetMax("IdRetencion");
                db.Retenciones.AddObject(f.registro);
                db.SaveChanges();
                Busqueda();

            }
        }
        void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        void btnVer_Click(object sender, EventArgs e)
        {
            if (this.bs.Current == null)
                return;
            FrmComprasItem f = new FrmComprasItem();
            f.registro = (Compra)this.bs.Current;
            f.Ver();
        }
        void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmComprasItem f = new FrmComprasItem();
            f.Incluir();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Busqueda();
            }
        }              
        private void Busqueda()
        {
            db = new DatosEntities();
            switch (txtFiltro.Text)
            {
                case "TODAS":
                    Lista = (from p in db.Compras
                             where ( p.RazonSocial.Contains(txtBuscar.Text) || txtBuscar.Text.Length == 0 ) && p.Estatus=="CERRADA"
                            orderby p.Fecha
                            select p).ToList();
                    break;
                case "AYER":
                    DateTime ayer = DateTime.Today.AddDays(-1);
                    Lista = (from p in db.Compras
                             where (p.RazonSocial.Contains(txtBuscar.Text) || txtBuscar.Text.Length == 0) && p.Fecha.Value == ayer && p.Estatus == "CERRADA"
                             orderby p.Numero
                             select p).ToList();
                    break;
                case "HOY":
                    Lista = (from p in db.Compras
                             where (p.RazonSocial.Contains(txtBuscar.Text) || txtBuscar.Text.Length == 0) && p.Fecha.Value == DateTime.Today && p.Estatus == "CERRADA"
                             orderby p.Numero
                             select p).ToList();
                    break;
                case "ESTE MES":
                    Lista = (from p in db.Compras
                             where (p.RazonSocial.Contains(txtBuscar.Text) || txtBuscar.Text.Length == 0) && p.Fecha.Value.Month == DateTime.Today.Month && p.Fecha.Value.Year == DateTime.Today.Year && p.Estatus == "CERRADA"
                             orderby p.Numero
                             select p).ToList();
                    break;
            }
            this.bs.DataSource = Lista;
            this.bs.ResetBindings(true);
        }
        private void EliminarRegistro()
        {
            if (this.bs.Current == null)
                return;
            Compra documento = (Compra)this.bs.Current;
            if (MessageBox.Show("Esta seguro de eliminar esta compra", "Atencion", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;
            try
            {
                FactoryLibroCompras.BorrarItem(documento);
                FactoryLibroInventarios.RevertirCompra(documento);
                FactoryCompras.InventarioDevolver(documento);
                db.Compras.DeleteObject(documento);
                db.SaveChanges();
                Busqueda();
                }
                catch (Exception x)
                {
                    Basicas.ManejarError(x);
                }
        }
        #region Eventos
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gridView1.ActiveEditor == null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        btnVer.PerformClick();
                        break;
                    case Keys.Delete:
                        btnEliminar.PerformClick();
                        break;
                    case Keys.Subtract:
                        btnEliminar.PerformClick();
                        break;
                }
            }
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
        }
        #endregion
    }
}
