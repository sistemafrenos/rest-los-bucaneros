using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;

namespace HK
{
    public partial class FrmBuscarEntidades : Form
    {
        public string Status;
        public object registro = null;
        DatosEntities dc = new DatosEntities();
        public object items;        
        private string Texto = "";
        private string myLayout = "";
        public FrmBuscarEntidades()
        {
            InitializeComponent();
        }
        private void FrmBuscar_Load(object sender, EventArgs e)
        {
            this.txtFiltro.Text = "TODOS";
            this.txtBuscar.Text = Texto;
            this.gridControl1.KeyDown +=new KeyEventHandler(gridControl1_KeyDown);
            this.gridControl1.DoubleClick +=new EventHandler(gridControl1_DoubleClick);
            this.txtBuscar.Click += new EventHandler(txtBuscar_Click);
            this.Imprimir.Click +=new EventHandler(Imprimir_Click);
            Busqueda();
        }
        void txtBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        public void BuscarClientes(string s)
        {
            Texto = s;
            myLayout = "CLIENTES";
            this.ShowDialog();
        }
        public void BuscarProveedores(string s)
        {
            Texto = s;
            myLayout = "PROVEEDORES";
            this.ShowDialog();
        }
        public void BuscarMesoneros(string s)
        {
            Texto = s;
            myLayout = "MESONEROS";
            this.ShowDialog();
        }
        public void BuscarMesas(string s)
        {
            Texto = s;
            myLayout = "MESAS";
            this.ShowDialog();
        }
        public void BuscarMesasDisponibles(string s)
        {
            Texto = s;
            myLayout = "MESAS DISPONIBLES";
            this.ShowDialog();
        }
        public void BuscarPlatos(string s)
        {
            myLayout = "PLATOS";
            Texto = s;
            this.ShowDialog();
        }
        public void BuscarFacturas(string s)
        {
            myLayout = "FACTURAS";
            Texto = s;
            this.ShowDialog();
        }
        public void BuscarCompras(string s)
        {
            myLayout = "COMPRAS";
            Texto = s;
            this.ShowDialog();
        }
        public void BuscarIngredientes(string s)
        {
            myLayout = "INGREDIENTES";
            Texto = s;
            this.ShowDialog();
        }
        private void Busqueda()
        {
            dc = new DatosEntities();
            Texto = this.txtBuscar.Text;            
            switch (myLayout.ToUpper())
            {
                case "CLIENTES":
                    this.bindingSource.DataSource = FactoryClientes.getItems(dc,Texto);
                    break;
                case "MESONEROS":
                    this.bindingSource.DataSource = FactoryUsuarios.getItems(dc, Texto,"MESONERO");
                    break;
                case "ADMINISTRADORES":
                    this.bindingSource.DataSource = FactoryUsuarios.getItems(dc, Texto,"ADMINISTRADOR");
                    break;
                case "CAJEROS":
                    this.bindingSource.DataSource = FactoryUsuarios.getItems(dc, Texto, "CAJERO");
                    break;
                case "MESAS":
                    this.bindingSource.DataSource = FactoryMesas.getItems(dc, Texto);
                    break;
                case "PLATOS":
                    this.bindingSource.DataSource = FactoryPlatos.getItems(dc, Texto);
                    break;
                case "COMPRAS":
                    this.bindingSource.DataSource = FactoryCompras.getComprasEspera(dc, Texto);
                    break;
                case "FACTURAS":
                    this.bindingSource.DataSource = FactoryFacturas.getFacturasPendientes(dc, Texto);
                    break;
                case "INGREDIENTES":
                    this.bindingSource.DataSource = FactoryIngredientes.getItems(dc, Texto);
                    break;
                case "PROVEEDORES":
                    this.bindingSource.DataSource = FactoryProveedores.getItems(dc, Texto);
                    break;
                case "MESAS DISPONIBLES":
                    this.bindingSource.DataSource = FactoryMesas.getMesasDisponibles(dc);
                    break;
            }
            this.gridControl1.DataSource = this.bindingSource;
            gridControl1.ForceInitialize();
            gridView1.OptionsLayout.Columns.Reset();
            this.gridControl1.DefaultView.RestoreLayoutFromXml(Application.StartupPath + "\\Layouts\\" + myLayout + ".XML", DevExpress.Utils.OptionsLayoutGrid.FullLayout);
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Seleccionar();
        }
        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    Eliminar();
                    e.Handled=true;
                    break;
                case Keys.Return:
                    Seleccionar();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    Cancelar();
                    e.Handled = true;
                    break;
            }
        }
        private void Seleccionar()
        {
            if (this.bindingSource.Current != null)
            {
                this.DialogResult = DialogResult.OK;
                registro = this.bindingSource.Current;
                this.Close();
            }
        }
        private void Eliminar()
        {
            if (this.bindingSource.Current != null)
            {
                this.DialogResult = DialogResult.Abort;
                registro = this.bindingSource.Current;
                this.Close();
            }
        }
        private void Cancelar()
        {
            this.DialogResult = DialogResult.Cancel;
            registro =null;
            this.Close();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Busqueda();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Cancelar();
            }
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Imprimir_Click(object sender, EventArgs e)
        {
            this.gridControl1.ShowPrintPreview();
        }
    }
}
