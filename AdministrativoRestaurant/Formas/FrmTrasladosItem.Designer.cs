namespace HK.Formas
{
    partial class FrmTrasladosItem
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.IdTrasladoTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.trasladoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FechaDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.NumeroTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ComentariosTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.DestinoComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ItemForIdTraslado = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForFecha = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForComentarios = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDestino = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForNumero = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.trasladosIngredienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdTrasladoIngrediente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtIngrediente = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colIdTraslado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdIngrediente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIngrediente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.Aceptar = new System.Windows.Forms.ToolStripButton();
            this.Cancelar = new System.Windows.Forms.ToolStripButton();
            this.txtCantidad = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IdTrasladoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trasladoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FechaDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FechaDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumeroTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComentariosTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestinoComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForIdTraslado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForComentarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDestino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNumero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trasladosIngredienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIngrediente)).BeginInit();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dataLayoutControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(648, 100);
            this.groupControl1.TabIndex = 0;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.IdTrasladoTextEdit);
            this.dataLayoutControl1.Controls.Add(this.FechaDateEdit);
            this.dataLayoutControl1.Controls.Add(this.NumeroTextEdit);
            this.dataLayoutControl1.Controls.Add(this.ComentariosTextEdit);
            this.dataLayoutControl1.Controls.Add(this.DestinoComboBoxEdit);
            this.dataLayoutControl1.DataSource = this.trasladoBindingSource;
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForIdTraslado});
            this.dataLayoutControl1.Location = new System.Drawing.Point(2, 22);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(644, 76);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // IdTrasladoTextEdit
            // 
            this.IdTrasladoTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.trasladoBindingSource, "IdTraslado", true));
            this.IdTrasladoTextEdit.Location = new System.Drawing.Point(0, 0);
            this.IdTrasladoTextEdit.Name = "IdTrasladoTextEdit";
            this.IdTrasladoTextEdit.Size = new System.Drawing.Size(0, 20);
            this.IdTrasladoTextEdit.StyleController = this.dataLayoutControl1;
            this.IdTrasladoTextEdit.TabIndex = 4;
            // 
            // trasladoBindingSource
            // 
            this.trasladoBindingSource.DataSource = typeof(HK.Traslado);
            // 
            // FechaDateEdit
            // 
            this.FechaDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("DateTime", this.trasladoBindingSource, "Fecha", true));
            this.FechaDateEdit.EditValue = null;
            this.FechaDateEdit.Location = new System.Drawing.Point(66, 2);
            this.FechaDateEdit.Name = "FechaDateEdit";
            this.FechaDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FechaDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FechaDateEdit.Size = new System.Drawing.Size(253, 20);
            this.FechaDateEdit.StyleController = this.dataLayoutControl1;
            this.FechaDateEdit.TabIndex = 5;
            // 
            // NumeroTextEdit
            // 
            this.NumeroTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.trasladoBindingSource, "Numero", true));
            this.NumeroTextEdit.Location = new System.Drawing.Point(387, 2);
            this.NumeroTextEdit.Name = "NumeroTextEdit";
            this.NumeroTextEdit.Size = new System.Drawing.Size(255, 20);
            this.NumeroTextEdit.StyleController = this.dataLayoutControl1;
            this.NumeroTextEdit.TabIndex = 6;
            // 
            // ComentariosTextEdit
            // 
            this.ComentariosTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.trasladoBindingSource, "Comentarios", true));
            this.ComentariosTextEdit.Location = new System.Drawing.Point(66, 26);
            this.ComentariosTextEdit.Name = "ComentariosTextEdit";
            this.ComentariosTextEdit.Size = new System.Drawing.Size(576, 20);
            this.ComentariosTextEdit.StyleController = this.dataLayoutControl1;
            this.ComentariosTextEdit.TabIndex = 7;
            // 
            // DestinoComboBoxEdit
            // 
            this.DestinoComboBoxEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.trasladoBindingSource, "Destino", true));
            this.DestinoComboBoxEdit.Location = new System.Drawing.Point(66, 50);
            this.DestinoComboBoxEdit.Name = "DestinoComboBoxEdit";
            this.DestinoComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DestinoComboBoxEdit.Size = new System.Drawing.Size(576, 20);
            this.DestinoComboBoxEdit.StyleController = this.dataLayoutControl1;
            this.DestinoComboBoxEdit.TabIndex = 8;
            // 
            // ItemForIdTraslado
            // 
            this.ItemForIdTraslado.Control = this.IdTrasladoTextEdit;
            this.ItemForIdTraslado.CustomizationFormText = "Id Traslado";
            this.ItemForIdTraslado.Location = new System.Drawing.Point(0, 0);
            this.ItemForIdTraslado.Name = "ItemForIdTraslado";
            this.ItemForIdTraslado.Size = new System.Drawing.Size(0, 0);
            this.ItemForIdTraslado.Text = "Id Traslado";
            this.ItemForIdTraslado.TextSize = new System.Drawing.Size(50, 20);
            this.ItemForIdTraslado.TextToControlDistance = 5;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(644, 76);
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
            this.ItemForFecha,
            this.ItemForComentarios,
            this.ItemForDestino,
            this.ItemForNumero});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(644, 76);
            this.layoutControlGroup2.Text = "autoGeneratedGroup0";
            // 
            // ItemForFecha
            // 
            this.ItemForFecha.Control = this.FechaDateEdit;
            this.ItemForFecha.CustomizationFormText = "Fecha";
            this.ItemForFecha.Location = new System.Drawing.Point(0, 0);
            this.ItemForFecha.Name = "ItemForFecha";
            this.ItemForFecha.Size = new System.Drawing.Size(321, 24);
            this.ItemForFecha.Text = "Fecha";
            this.ItemForFecha.TextSize = new System.Drawing.Size(60, 13);
            // 
            // ItemForComentarios
            // 
            this.ItemForComentarios.Control = this.ComentariosTextEdit;
            this.ItemForComentarios.CustomizationFormText = "Comentarios";
            this.ItemForComentarios.Location = new System.Drawing.Point(0, 24);
            this.ItemForComentarios.Name = "ItemForComentarios";
            this.ItemForComentarios.Size = new System.Drawing.Size(644, 24);
            this.ItemForComentarios.Text = "Comentarios";
            this.ItemForComentarios.TextSize = new System.Drawing.Size(60, 13);
            // 
            // ItemForDestino
            // 
            this.ItemForDestino.Control = this.DestinoComboBoxEdit;
            this.ItemForDestino.CustomizationFormText = "Destino";
            this.ItemForDestino.Location = new System.Drawing.Point(0, 48);
            this.ItemForDestino.Name = "ItemForDestino";
            this.ItemForDestino.Size = new System.Drawing.Size(644, 28);
            this.ItemForDestino.Text = "Destino";
            this.ItemForDestino.TextSize = new System.Drawing.Size(60, 13);
            // 
            // ItemForNumero
            // 
            this.ItemForNumero.Control = this.NumeroTextEdit;
            this.ItemForNumero.CustomizationFormText = "Numero";
            this.ItemForNumero.Location = new System.Drawing.Point(321, 0);
            this.ItemForNumero.Name = "ItemForNumero";
            this.ItemForNumero.Size = new System.Drawing.Size(323, 24);
            this.ItemForNumero.Text = "Numero";
            this.ItemForNumero.TextSize = new System.Drawing.Size(60, 13);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.trasladosIngredienteBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(12, 128);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtIngrediente,
            this.txtCantidad});
            this.gridControl1.Size = new System.Drawing.Size(648, 381);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // trasladosIngredienteBindingSource
            // 
            this.trasladosIngredienteBindingSource.DataSource = typeof(HK.TrasladosIngrediente);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdTrasladoIngrediente,
            this.colIdTraslado,
            this.colIdIngrediente,
            this.colIngrediente,
            this.colCantidad});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colIdTrasladoIngrediente
            // 
            this.colIdTrasladoIngrediente.ColumnEdit = this.txtIngrediente;
            this.colIdTrasladoIngrediente.FieldName = "IdTrasladoIngrediente";
            this.colIdTrasladoIngrediente.Name = "colIdTrasladoIngrediente";
            this.colIdTrasladoIngrediente.Visible = true;
            this.colIdTrasladoIngrediente.VisibleIndex = 0;
            // 
            // txtIngrediente
            // 
            this.txtIngrediente.AutoHeight = false;
            this.txtIngrediente.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtIngrediente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIngrediente.Name = "txtIngrediente";
            // 
            // colIdTraslado
            // 
            this.colIdTraslado.FieldName = "IdTraslado";
            this.colIdTraslado.Name = "colIdTraslado";
            this.colIdTraslado.Visible = true;
            this.colIdTraslado.VisibleIndex = 1;
            // 
            // colIdIngrediente
            // 
            this.colIdIngrediente.FieldName = "IdIngrediente";
            this.colIdIngrediente.Name = "colIdIngrediente";
            this.colIdIngrediente.Visible = true;
            this.colIdIngrediente.VisibleIndex = 2;
            // 
            // colIngrediente
            // 
            this.colIngrediente.ColumnEdit = this.txtIngrediente;
            this.colIngrediente.FieldName = "Ingrediente";
            this.colIngrediente.Name = "colIngrediente";
            this.colIngrediente.Visible = true;
            this.colIngrediente.VisibleIndex = 3;
            // 
            // colCantidad
            // 
            this.colCantidad.ColumnEdit = this.txtCantidad;
            this.colCantidad.FieldName = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.Visible = true;
            this.colCantidad.VisibleIndex = 4;
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aceptar,
            this.Cancelar});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 512);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(668, 54);
            this.BarraAcciones.TabIndex = 41;
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
            // txtCantidad
            // 
            this.txtCantidad.AutoHeight = false;
            this.txtCantidad.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.txtCantidad.DisplayFormat.FormatString = "n2";
            this.txtCantidad.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidad.EditFormat.FormatString = "n2";
            this.txtCantidad.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidad.Name = "txtCantidad";
            // 
            // FrmTrasladosItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 566);
            this.Controls.Add(this.BarraAcciones);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmTrasladosItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traslados Item";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IdTrasladoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trasladoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FechaDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FechaDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumeroTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComentariosTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestinoComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForIdTraslado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForComentarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDestino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNumero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trasladosIngredienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIngrediente)).EndInit();
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource trasladosIngredienteBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit IdTrasladoTextEdit;
        private System.Windows.Forms.BindingSource trasladoBindingSource;
        private DevExpress.XtraEditors.DateEdit FechaDateEdit;
        private DevExpress.XtraEditors.TextEdit NumeroTextEdit;
        private DevExpress.XtraEditors.TextEdit ComentariosTextEdit;
        private DevExpress.XtraEditors.ComboBoxEdit DestinoComboBoxEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForIdTraslado;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForFecha;
        private DevExpress.XtraLayout.LayoutControlItem ItemForComentarios;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDestino;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNumero;
        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Aceptar;
        private System.Windows.Forms.ToolStripButton Cancelar;
        private DevExpress.XtraGrid.Columns.GridColumn colIdTrasladoIngrediente;
        private DevExpress.XtraGrid.Columns.GridColumn colIdTraslado;
        private DevExpress.XtraGrid.Columns.GridColumn colIdIngrediente;
        private DevExpress.XtraGrid.Columns.GridColumn colIngrediente;
        private DevExpress.XtraGrid.Columns.GridColumn colCantidad;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit txtIngrediente;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit txtCantidad;
    }
}