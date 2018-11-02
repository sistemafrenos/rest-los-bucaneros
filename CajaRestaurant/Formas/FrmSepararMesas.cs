using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HK.Clases;

namespace HK.Formas
{
    public partial class FrmSepararMesas : Form
    {
        public FrmSepararMesas()
        {
            InitializeComponent();
            this.Load += new EventHandler(FrmSepararMesas_Load);
        }
        DatosEntities db = new DatosEntities();
        private bool esNuevo = true;
        private Mesa mesa;
        private Mesa newMesa;
        public MesasAbierta mesaAbierta = null;
        private List<MesasAbiertasPlato> mesaAbiertaPlatos = new List<MesasAbiertasPlato>();
        private MesasAbierta newMesaAbierta = new MesasAbierta();
        private List<MesasAbiertasPlato> newMesaAbiertaPlatos = new List<MesasAbiertasPlato>();

        void FrmSepararMesas_Load(object sender, EventArgs e)
        {
            #region Eventos
            this.MesaTextEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(MesaTextEdit_ButtonClick);
            this.Aceptar.Click += new EventHandler(Aceptar_Click);
            this.Cancelar.Click += new EventHandler(Cancelar_Click);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmSolicitarMesonero_KeyDown);
            #endregion
            #region Enlazar
            mesaAbierta = FactoryMesas.MesaAbiertaItem(db, mesaAbierta);
            mesa = FactoryMesas.Item(db, mesaAbierta.IdMesa);
            if (mesaAbierta.IdMesa != null)
            {
                mesaAbiertaPlatos = (from x in db.MesasAbiertasPlatos
                                     where x.IdMesaAbierta == mesaAbierta.IdMesaAbierta
                                     select x).ToList();
            }
            this.mesasAbiertaBindingSource.DataSource = mesaAbierta;
            this.mesasAbiertaBindingSource.ResetBindings(true);
            this.mesasAbiertasPlatoBindingSource.DataSource = mesaAbiertaPlatos;
            this.mesasAbiertasPlatoBindingSource.ResetBindings(true);
            this.newMesasAbiertaBindingSource.DataSource = newMesaAbierta;
            this.newMesasAbiertaBindingSource.ResetBindings(true);
            this.newMesasAbiertasPlatoBindingSource.DataSource = newMesaAbiertaPlatos;
            this.newMesasAbiertasPlatoBindingSource.ResetBindings(true);
            #endregion
        }
        void Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void Aceptar_Click(object sender, EventArgs e)
        {
            Guardar();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void Guardar()
        {
            try
            {
                mesaAbierta.Totalizar(mesa.CobraServicio.GetValueOrDefault(false), mesaAbiertaPlatos, mesa.Descuento);
                if (mesaAbiertaPlatos.Count == 0)
                    throw new Exception("Cuenta original sin platos original");
                newMesaAbierta.Totalizar(newMesa.CobraServicio.GetValueOrDefault(false), newMesaAbiertaPlatos, newMesa.Descuento);
                if (newMesaAbiertaPlatos.Count == 0)
                    throw new Exception("Cuenta nueva sin platos");
                newMesaAbierta.Numero = mesaAbierta.Numero;
                newMesaAbierta.IdMesaAbierta = FactoryContadores.GetMax("IdMesaAbierta");
                newMesaAbierta.IdMesa = newMesaAbierta.IdMesa;
                newMesaAbierta.Apertura = mesaAbierta.Apertura;
                newMesaAbierta.Estatus = mesaAbierta.Estatus;
                newMesaAbierta.IdMesonero = mesaAbierta.IdMesonero;
                newMesaAbierta.Impresa = mesaAbierta.Impresa;
                newMesaAbierta.Mesa = newMesa.Descripcion;
                newMesaAbierta.Mesonero = mesaAbierta.Mesonero;
                foreach (MesasAbiertasPlato p in newMesaAbiertaPlatos)
                {
                        p.IdMesaAbiertaPlato = FactoryContadores.GetMax("IdMesaAbiertaPlato");
                        p.IdMesaAbierta = newMesaAbierta.IdMesaAbierta;
                        db.MesasAbiertasPlatos.AddObject(p);
                }
                db.MesasAbiertas.AddObject(newMesaAbierta);
                db.SaveChanges();
                this.Close();
            }
            catch (Exception x)
            {
                Basicas.ManejarError(x);
            }
        }
        void FrmSolicitarMesonero_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Cancelar.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F12:
                    this.Aceptar.PerformClick();
                    e.Handled = true;
                    break;
            }
        }
        void MesaTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmBuscarEntidades f = new FrmBuscarEntidades();
            f.BuscarMesasDisponibles("");
            if (f.registro != null)
            {
                Mesa item = (Mesa)f.registro;
                newMesa = item;
                newMesaAbierta.IdMesa = item.IdMesa;
                newMesaAbierta.Mesa = item.Descripcion;
            }
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (newMesa == null)
            {
                MessageBox.Show("Debe primero elejir una mesa destino para separar la cuenta");
                return;
            }
            if (mesasAbiertasPlatoBindingSource.Current == null)
                return;
            MesasAbiertasPlato registro = (MesasAbiertasPlato)mesasAbiertasPlatoBindingSource.Current;
            MesasAbiertasPlato nuevoRegistro = new MesasAbiertasPlato();
            nuevoRegistro.Cantidad = registro.Cantidad;
            nuevoRegistro.Codigo = registro.Codigo;
            nuevoRegistro.Comentarios = registro.Comentarios;
            nuevoRegistro.Contornos = registro.Contornos;
            nuevoRegistro.Costo = registro.Costo;
            nuevoRegistro.Descripcion = registro.Descripcion;
            nuevoRegistro.EnviarComanda = registro.EnviarComanda;
            nuevoRegistro.Grupo = registro.Grupo;
            nuevoRegistro.Idplato = registro.Idplato;
            nuevoRegistro.Precio = registro.Precio;
            nuevoRegistro.PrecioConIva = registro.PrecioConIva;
            nuevoRegistro.TasaIva = registro.TasaIva;
            nuevoRegistro.Total = registro.Total;
            newMesaAbiertaPlatos.Add((MesasAbiertasPlato)nuevoRegistro);
            mesaAbiertaPlatos.Remove((MesasAbiertasPlato)registro);
            db.MesasAbiertasPlatos.DeleteObject((MesasAbiertasPlato)registro);
            this.mesasAbiertasPlatoBindingSource.DataSource = mesaAbiertaPlatos;
            this.mesasAbiertasPlatoBindingSource.ResetBindings(true);
            this.newMesasAbiertasPlatoBindingSource.DataSource = newMesaAbiertaPlatos;
            this.newMesasAbiertasPlatoBindingSource.ResetBindings(true);
            mesaAbierta.Totalizar(mesa.CobraServicio.GetValueOrDefault(false),mesaAbiertaPlatos,mesa.Descuento.GetValueOrDefault(0));
            newMesaAbierta.Totalizar(newMesa.CobraServicio.GetValueOrDefault(false), newMesaAbiertaPlatos, newMesa.Descuento.GetValueOrDefault(0));
        }
    }
}
