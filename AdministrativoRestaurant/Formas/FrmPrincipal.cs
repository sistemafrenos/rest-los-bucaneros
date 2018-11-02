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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmPrincipal_Load);
        }

        void FrmPrincipal_Load(object sender, EventArgs e)
        {            
            this.barButtonPlatos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonPlatos_ItemClick);
            this.barButtonParametros.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonParametros_ItemClick);
            this.barButtonReporteX.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonReporteX_ItemClick);
            this.barButtonReporteZ.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonReporteZ_ItemClick);
            this.barButtonResumenZ.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonResumenZ_ItemClick);
            this.barButtonFacturas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonFacturas_ItemClick);
            this.barButtonCierreDeCaja.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonCierreDeCaja_ItemClick);
            this.barButtonVentasxLapso.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonVentasxLapso_ItemClick);
            this.barButtonVentasxProducto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonVentasxProducto_ItemClick);
            this.barButtonIngredientes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonIngredientes_ItemClick);
            this.barButtonMesas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonMesas_ItemClick);
            this.barButtonNaturalesJuridicas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonNaturalesJuridicas_ItemClick);
            this.barButtonLibroVentasResumen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonLibroVentasResumen_ItemClick);
            this.barButtonRespaldo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonRespaldo_ItemClick);
            this.barButtonRecuperacion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonRecuperacion_ItemClick);
            this.barButtonMesoneros.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonMesoneros_ItemClick);
            this.barButtonUsuarios.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonUsuarios_ItemClick);
            this.barButtonCajeros.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonCajeros_ItemClick);
            this.barButtonAjustePrecios.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonAjustePrecios_ItemClick);
            this.barButtonComprasEntradas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonComprasEntradas_ItemClick);
            this.barButtonConsumoLapso.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonConsumoLapso_ItemClick);
            this.barConsumoFecha.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barConsumoFecha_ItemClick);
            this.barLibroCompras.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barLibroCompras_ItemClick);
            this.barButtonProveedores.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonProveedores_ItemClick);
            this.barButtonLibroInventarios.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonLibroInventarios_ItemClick);
            this.barButtonLibroCompras.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonLibroCompras_ItemClick);
            this.barButtonLibroVentas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonLibroVentas_ItemClick);
            this.barComprasxLapso.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barComprasxLapso_ItemClick);
            this.barInventarioInsumos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barInventarioInsumos_ItemClick);
            this.barButtonComisionesMesoneros.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonVentasxMesonero_ItemClick);
            this.barButtonImportarDatos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonImportarDatos_ItemClick);
            this.barPasarDatos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonImportarDatos_ItemClick);
            this.barButtonClientes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonClientes_ItemClick);
            this.WindowState = FormWindowState.Maximized;
        }

        void barButtonClientes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.Clientes == null)
            {
                Pantallas.Clientes = new FrmClientes();
                Pantallas.Clientes.MdiParent = this;
                Pantallas.Clientes.Show();
            }
            else
            {
                Pantallas.Clientes.Activate();
            } 
            
        }

        void barButtonImportarDatos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Importar Mesas
            DatosEntities db = new DatosEntities();
            BucaneroEntities dc = new BucaneroEntities();
            foreach (var x in dc.Varios.Where(x => x.Clase == "MESA"))
            {
                Mesa mesa = new Mesa();
                mesa.CobraServicio = x.Valor == 1 ? true : false;
                mesa.Codigo = x.Codigo;
                mesa.Descripcion = x.Descripcion;
                mesa.IdMesa = x.IDVarios;
                mesa.Ubicacion = x.Texto;
                db.Mesas.AddObject(mesa);
            }
            db.SaveChanges();
            foreach (var x in dc.Terceros.Where(x => x.Tipo == "CLIENTE"))
            {
                Cliente c = new Cliente();
                c.CedulaRif = Basicas.CedulaRif(x.CedulaRif);
                c.DiasCredito = x.DiasCredito;
                c.Direccion = x.Direccion;
                c.Email = x.Email;
                c.IdCliente = x.IdTercero;
                c.LimiteCredito = x.LimiteCredito;
                c.RazonSocial = x.RazonSocial.ToUpper();
                c.Telefonos = x.Telefonos;
                db.Clientes.AddObject(c);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    string s = ex.Message;
                    db = new DatosEntities();
                }
            }
            
            foreach (var x2 in dc.Productos.Where(x=>x.Activo==true && x.Tipo =="<<SERVICIO>>"))
            {
                Plato p = new Plato();
                p.Activo = x2.Activo;
                p.Codigo = x2.Codigo;
                p.Costo = 0;
                p.Descripcion = x2.Descripcion;
                p.EnviarComanda = null;
                p.Grupo = x2.Grupo;
                p.IdPlato = x2.IdProducto;
                p.MostrarMenu = true;
                p.Precio = x2.Precio;
                p.PrecioConIva = x2.Precio*1.12;
                p.TasaIva = 12;
                db.Platos.AddObject(p);
            }
            db.SaveChanges();
            //
        }

        void barButtonVentasxMesonero_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ComisionesMesonerosFecha();
        }

        void barInventarioInsumos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.IngredientesInventario == null)
            {
                Pantallas.IngredientesInventario = new FrmIngredientesInventario();
                Pantallas.IngredientesInventario.MdiParent = this;
                Pantallas.IngredientesInventario.Show();
            }
            else
            {
                Pantallas.IngredientesInventario.Activate();
            } 
        }

        void barComprasxLapso_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Basicas.ComprasxGrupo();
        }

        void barButtonLibroVentas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.LibroVentas == null)
            {
                Pantallas.LibroVentas = new FrmLibroVentas();
                Pantallas.LibroVentas.MdiParent = this;
                Pantallas.LibroVentas.Show();
            }
            else
            {
                Pantallas.LibroVentas.Activate();
            } 
        }

        void barButtonLibroCompras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.LibroCompras == null)
            {
                Pantallas.LibroCompras = new FrmLibroCompras();
                Pantallas.LibroCompras.MdiParent = this;
                Pantallas.LibroCompras.Show();
            }
            else
            {
                Pantallas.LibroCompras.Activate();
            } 
        }

        void barButtonLibroInventarios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.LibroInventarios == null)
            {
                Pantallas.LibroInventarios = new FrmLibroInventarios();
                Pantallas.LibroInventarios.MdiParent = this;
                Pantallas.LibroInventarios.Show();
            }
            else
            {
                Pantallas.LibroInventarios.Activate();
            } 
        }

        void barButtonProveedores_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.ProveedoresLista == null)
            {
                Pantallas.ProveedoresLista = new FrmProveedores();
                Pantallas.ProveedoresLista.MdiParent = this;
                Pantallas.ProveedoresLista.Show();
            }
            else
            {
                Pantallas.ProveedoresLista.Activate();
            } 
        }
        void barLibroCompras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.LibroDeCompras();
        }
        void barConsumoFecha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ConsumoxFecha();
        }
        void barButtonConsumoLapso_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ConsumoxLapso();
        }
        void barButtonComprasEntradas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.ComprasLista == null)
            {
                Pantallas.ComprasLista = new FrmCompras();
                Pantallas.ComprasLista.MdiParent = this;
                Pantallas.ComprasLista.Show();
            }
            else
            {
                Pantallas.ComprasLista.Activate();
            }  
        }
        #region Usuarios
        void barButtonCajeros_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.CajerosLista == null)
            {
                Pantallas.CajerosLista = new FrmCajeros();
                Pantallas.CajerosLista.MdiParent = this;
                Pantallas.CajerosLista.Show();
            }
            else
            {
                Pantallas.CajerosLista.Activate();
            }  
        }
        void barButtonMesoneros_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.MesonerosLista == null)
            {
                Pantallas.MesonerosLista = new FrmMesoneros();
                Pantallas.MesonerosLista.MdiParent = this;
                Pantallas.MesonerosLista.Show();
            }
            else
            {
                Pantallas.MesonerosLista.Activate();
            }
        }
        void barButtonUsuarios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.Usuarioslista == null)
            {
                Pantallas.Usuarioslista = new FrmUsuarios();
                Pantallas.Usuarioslista.MdiParent = this;
                Pantallas.Usuarioslista.Show();
            }
            else
            {
                Pantallas.Usuarioslista.Activate();
            }
        }
        #endregion
        void barButtonRecuperacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.RootFolder = Environment.SpecialFolder.MyComputer;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.SelectedPath == string.Empty)
                    return;
                if (!System.IO.File.Exists(f.SelectedPath + "respaldo.bak"))
                {
                    MessageBox.Show("No se encontro un respaldo en ese sitio");
                    return;
                }
                try
                {
                    System.IO.File.Copy(Application.StartupPath + "\\feria.sdf", Application.StartupPath + "\\feria.old",true);
                    System.IO.File.Copy(f.SelectedPath + "\\respaldo.bak", Application.StartupPath + "\\feria.sdf",true);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
        void barButtonRespaldo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.RootFolder = Environment.SpecialFolder.MyComputer;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.SelectedPath == string.Empty)
                    return;
                if (System.IO.File.Exists(f.SelectedPath + "respaldo.bak"))
                {
                    System.IO.File.Copy(f.SelectedPath + "respaldo.bak", f.SelectedPath + "respaldo.old", true);
                }
                try
                {
                    System.IO.File.Copy(Application.StartupPath + "\\Feria.sdf", f.SelectedPath + "respaldo.bak", true);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
        void barButtonLibroVentasResumen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.LibroDeVentasResumido();
        }
        void barButtonNaturalesJuridicas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.ReporteFacturasNaturalesyJuridicas();
        }
        void barButtonMesas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.MesasLista == null)
            {
                Pantallas.MesasLista = new FrmMesas();
                Pantallas.MesasLista.MdiParent = this;
                Pantallas.MesasLista.Show();
            }
            else
            {
                Pantallas.MesasLista.Activate();
            }  
        }
        void barButtonExistenciaAjustes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.IngredientesInventario == null)
            {
                Pantallas.IngredientesInventario = new FrmIngredientesInventario();
                Pantallas.IngredientesInventario.MdiParent = this;
                Pantallas.IngredientesInventario.Show();
            }
            else
            {
                Pantallas.IngredientesLista.Activate();
            }  
        }
        void barButtonIngredientes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.IngredientesLista == null)
            {
                Pantallas.IngredientesLista = new FrmIngredientes();
                Pantallas.IngredientesLista.MdiParent = this;
                Pantallas.IngredientesLista.Show();
            }
            else
            {
                Pantallas.IngredientesLista.Activate();
            }            
        }
        void barButtonVentasxProducto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.VentasxProducto();            
            
        }
        void barButtonVentasxLapso_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.VentasxLapso();            
        }
        void barButtonCierreDeCaja_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReportes f = new FrmReportes();
            f.CierreDeCaja();
        }
        void barButtonFacturas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.Facturaslista == null)
            {
                Pantallas.Facturaslista = new FrmFacturas();
                Pantallas.Facturaslista.MdiParent = this;
                Pantallas.Facturaslista.Show();
            }
            else
            {
                Pantallas.Facturaslista.Activate();
            }
        }
        void barButtonResumenZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmLapso lapso = new FrmLapso();
            lapso.ShowDialog();
            if (lapso.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Fiscal f = new Fiscal();
                   // f.ReporteMensualIVA(lapso.desde, lapso.hasta);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }
        void barButtonReporteZ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Fiscal f = new Fiscal();
                f.ReporteZ();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        void barButtonReporteX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Fiscal f = new Fiscal();
                f.ReporteX();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        void barButtonParametros_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmParametros f = new FrmParametros();
            f.ShowDialog();
        }
        
        #region Platos
        void barButtonPlatos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.PlatosLista == null)
            {
                Pantallas.PlatosLista = new FrmPlatos();
                Pantallas.PlatosLista.MdiParent = this;
                Pantallas.PlatosLista.Show();
            }
            else
            {
                Pantallas.PlatosLista.Activate();
            }
        }
        void barButtonAjustePrecios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Pantallas.PlatosAjustePrecios == null)
            {
                Pantallas.PlatosAjustePrecios = new FrmPlatosAjustePrecios();
                Pantallas.PlatosAjustePrecios.MdiParent = this;
                Pantallas.PlatosAjustePrecios.Show();
            }
            else
            {
                Pantallas.PlatosAjustePrecios.Activate();
            }

        }
        #endregion

    }
}
