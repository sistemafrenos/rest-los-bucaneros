namespace HK.Formas
{
    partial class FrmMesasItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.Aceptar = new System.Windows.Forms.ToolStripButton();
            this.Cancelar = new System.Windows.Forms.ToolStripButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.IdMesaTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.mesaBindingSource = new System.Windows.Forms.BindingSource();
            this.CodigoTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.DescripcionTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.UbicacionComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CobraServicioCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.MesasAbiertasTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ItemForIdMesa = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMesasAbiertas = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForCodigo = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDescripcion = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForCobraServicio = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForUbicacion = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btnCrearCodigo = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IdMesaTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodigoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescripcionTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UbicacionComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CobraServicioCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MesasAbiertasTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForIdMesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMesasAbiertas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCobraServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForUbicacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aceptar,
            this.Cancelar});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 148);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(428, 54);
            this.BarraAcciones.TabIndex = 39;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // Aceptar
            // 
            this.Aceptar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Aceptar.Image = global::HK.Properties.Resources.disk_blue_ok;
            this.Aceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(77, 51);
            this.Aceptar.Text = "Aceptar - F12";
            this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Cancelar
            // 
            this.Cancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Cancelar.Image = global::HK.Properties.Resources.disk_blue_error;
            this.Cancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(82, 51);
            this.Cancelar.Text = "Cancelar - ESC";
            this.Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dataLayoutControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(404, 123);
            this.groupControl1.TabIndex = 40;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.btnCrearCodigo);
            this.dataLayoutControl1.Controls.Add(this.IdMesaTextEdit);
            this.dataLayoutControl1.Controls.Add(this.CodigoTextEdit);
            this.dataLayoutControl1.Controls.Add(this.DescripcionTextEdit);
            this.dataLayoutControl1.Controls.Add(this.UbicacionComboBoxEdit);
            this.dataLayoutControl1.Controls.Add(this.CobraServicioCheckEdit);
            this.dataLayoutControl1.Controls.Add(this.MesasAbiertasTextEdit);
            this.dataLayoutControl1.DataSource = this.mesaBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForIdMesa,
            this.ItemForMesasAbiertas});
            this.dataLayoutControl1.Location = new System.Drawing.Point(2, 22);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(400, 99);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // IdMesaTextEdit
            // 
            this.IdMesaTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mesaBindingSource, "IdMesa", true));
            this.IdMesaTextEdit.Location = new System.Drawing.Point(0, 0);
            this.IdMesaTextEdit.Name = "IdMesaTextEdit";
            this.IdMesaTextEdit.Size = new System.Drawing.Size(0, 20);
            this.IdMesaTextEdit.StyleController = this.dataLayoutControl1;
            this.IdMesaTextEdit.TabIndex = 4;
            // 
            // mesaBindingSource
            // 
            this.mesaBindingSource.DataSource = typeof(HK.Mesa);
            // 
            // CodigoTextEdit
            // 
            this.CodigoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mesaBindingSource, "Codigo", true));
            this.CodigoTextEdit.Location = new System.Drawing.Point(339, 2);
            this.CodigoTextEdit.Name = "CodigoTextEdit";
            this.CodigoTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CodigoTextEdit.Size = new System.Drawing.Size(59, 20);
            this.CodigoTextEdit.StyleController = this.dataLayoutControl1;
            this.CodigoTextEdit.TabIndex = 5;
            // 
            // DescripcionTextEdit
            // 
            this.DescripcionTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mesaBindingSource, "Descripcion", true));
            this.DescripcionTextEdit.Location = new System.Drawing.Point(60, 28);
            this.DescripcionTextEdit.Name = "DescripcionTextEdit";
            this.DescripcionTextEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.DescripcionTextEdit.Size = new System.Drawing.Size(338, 20);
            this.DescripcionTextEdit.StyleController = this.dataLayoutControl1;
            this.DescripcionTextEdit.TabIndex = 6;
            // 
            // UbicacionComboBoxEdit
            // 
            this.UbicacionComboBoxEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mesaBindingSource, "Ubicacion", true));
            this.UbicacionComboBoxEdit.Location = new System.Drawing.Point(60, 2);
            this.UbicacionComboBoxEdit.Name = "UbicacionComboBoxEdit";
            this.UbicacionComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.UbicacionComboBoxEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.UbicacionComboBoxEdit.Size = new System.Drawing.Size(139, 20);
            this.UbicacionComboBoxEdit.StyleController = this.dataLayoutControl1;
            this.UbicacionComboBoxEdit.TabIndex = 7;
            // 
            // CobraServicioCheckEdit
            // 
            this.CobraServicioCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mesaBindingSource, "CobraServicio", true));
            this.CobraServicioCheckEdit.Location = new System.Drawing.Point(201, 52);
            this.CobraServicioCheckEdit.Name = "CobraServicioCheckEdit";
            this.CobraServicioCheckEdit.Properties.Caption = "Cobra Servicio";
            this.CobraServicioCheckEdit.Size = new System.Drawing.Size(197, 19);
            this.CobraServicioCheckEdit.StyleController = this.dataLayoutControl1;
            this.CobraServicioCheckEdit.TabIndex = 8;
            // 
            // MesasAbiertasTextEdit
            // 
            this.MesasAbiertasTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.mesaBindingSource, "MesasAbiertas", true));
            this.MesasAbiertasTextEdit.Location = new System.Drawing.Point(0, 0);
            this.MesasAbiertasTextEdit.Name = "MesasAbiertasTextEdit";
            this.MesasAbiertasTextEdit.Size = new System.Drawing.Size(0, 20);
            this.MesasAbiertasTextEdit.StyleController = this.dataLayoutControl1;
            this.MesasAbiertasTextEdit.TabIndex = 9;
            // 
            // ItemForIdMesa
            // 
            this.ItemForIdMesa.Control = this.IdMesaTextEdit;
            this.ItemForIdMesa.CustomizationFormText = "Id Mesa";
            this.ItemForIdMesa.Location = new System.Drawing.Point(0, 0);
            this.ItemForIdMesa.Name = "ItemForIdMesa";
            this.ItemForIdMesa.Size = new System.Drawing.Size(0, 0);
            this.ItemForIdMesa.Text = "Id Mesa";
            this.ItemForIdMesa.TextSize = new System.Drawing.Size(50, 20);
            this.ItemForIdMesa.TextToControlDistance = 5;
            // 
            // ItemForMesasAbiertas
            // 
            this.ItemForMesasAbiertas.Control = this.MesasAbiertasTextEdit;
            this.ItemForMesasAbiertas.CustomizationFormText = "Mesas Abiertas";
            this.ItemForMesasAbiertas.Location = new System.Drawing.Point(0, 0);
            this.ItemForMesasAbiertas.Name = "ItemForMesasAbiertas";
            this.ItemForMesasAbiertas.Size = new System.Drawing.Size(0, 0);
            this.ItemForMesasAbiertas.Text = "Mesas Abiertas";
            this.ItemForMesasAbiertas.TextSize = new System.Drawing.Size(50, 20);
            this.ItemForMesasAbiertas.TextToControlDistance = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(400, 99);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.CustomizationFormText = "autoGeneratedGroup0";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForCodigo,
            this.ItemForDescripcion,
            this.ItemForCobraServicio,
            this.ItemForUbicacion,
            this.emptySpaceItem1,
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(400, 99);
            this.layoutControlGroup2.Text = "autoGeneratedGroup0";
            // 
            // ItemForCodigo
            // 
            this.ItemForCodigo.Control = this.CodigoTextEdit;
            this.ItemForCodigo.CustomizationFormText = "Codigo";
            this.ItemForCodigo.Location = new System.Drawing.Point(279, 0);
            this.ItemForCodigo.Name = "ItemForCodigo";
            this.ItemForCodigo.Size = new System.Drawing.Size(121, 26);
            this.ItemForCodigo.Text = "Codigo";
            this.ItemForCodigo.TextSize = new System.Drawing.Size(54, 13);
            // 
            // ItemForDescripcion
            // 
            this.ItemForDescripcion.Control = this.DescripcionTextEdit;
            this.ItemForDescripcion.CustomizationFormText = "Descripcion";
            this.ItemForDescripcion.Location = new System.Drawing.Point(0, 26);
            this.ItemForDescripcion.Name = "ItemForDescripcion";
            this.ItemForDescripcion.Size = new System.Drawing.Size(400, 24);
            this.ItemForDescripcion.Text = "Descripcion";
            this.ItemForDescripcion.TextSize = new System.Drawing.Size(54, 13);
            // 
            // ItemForCobraServicio
            // 
            this.ItemForCobraServicio.Control = this.CobraServicioCheckEdit;
            this.ItemForCobraServicio.CustomizationFormText = "Cobra Servicio";
            this.ItemForCobraServicio.Location = new System.Drawing.Point(199, 50);
            this.ItemForCobraServicio.Name = "ItemForCobraServicio";
            this.ItemForCobraServicio.Size = new System.Drawing.Size(201, 49);
            this.ItemForCobraServicio.Text = "Cobra Servicio";
            this.ItemForCobraServicio.TextSize = new System.Drawing.Size(0, 0);
            this.ItemForCobraServicio.TextToControlDistance = 0;
            this.ItemForCobraServicio.TextVisible = false;
            // 
            // ItemForUbicacion
            // 
            this.ItemForUbicacion.Control = this.UbicacionComboBoxEdit;
            this.ItemForUbicacion.CustomizationFormText = "Ubicacion";
            this.ItemForUbicacion.Location = new System.Drawing.Point(0, 0);
            this.ItemForUbicacion.Name = "ItemForUbicacion";
            this.ItemForUbicacion.Size = new System.Drawing.Size(201, 26);
            this.ItemForUbicacion.Text = "Ubicacion";
            this.ItemForUbicacion.TextSize = new System.Drawing.Size(54, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 50);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(199, 49);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnCrearCodigo
            // 
            this.btnCrearCodigo.Location = new System.Drawing.Point(203, 2);
            this.btnCrearCodigo.Name = "btnCrearCodigo";
            this.btnCrearCodigo.Size = new System.Drawing.Size(74, 22);
            this.btnCrearCodigo.StyleController = this.dataLayoutControl1;
            this.btnCrearCodigo.TabIndex = 10;
            this.btnCrearCodigo.Text = "Crear Codigo";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCrearCodigo;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(201, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(78, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmMesasItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 202);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.BarraAcciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmMesasItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mesa";
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IdMesaTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mesaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodigoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescripcionTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UbicacionComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CobraServicioCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MesasAbiertasTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForIdMesa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMesasAbiertas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCobraServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForUbicacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Aceptar;
        private System.Windows.Forms.ToolStripButton Cancelar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit IdMesaTextEdit;
        private System.Windows.Forms.BindingSource mesaBindingSource;
        private DevExpress.XtraEditors.TextEdit CodigoTextEdit;
        private DevExpress.XtraEditors.TextEdit DescripcionTextEdit;
        private DevExpress.XtraEditors.ComboBoxEdit UbicacionComboBoxEdit;
        private DevExpress.XtraEditors.CheckEdit CobraServicioCheckEdit;
        private DevExpress.XtraEditors.TextEdit MesasAbiertasTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForIdMesa;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMesasAbiertas;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCodigo;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDescripcion;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCobraServicio;
        private DevExpress.XtraLayout.LayoutControlItem ItemForUbicacion;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnCrearCodigo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}