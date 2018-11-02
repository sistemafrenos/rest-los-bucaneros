namespace HK.Formas
{
    partial class FrmCompras
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAño = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumeroControl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoExento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoGravable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoIva = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCedulaRif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRazonSocial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTasaIva = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLibroCompras = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoSinDerechoCredito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnVer = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.txtFiltro = new System.Windows.Forms.ToolStripComboBox();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.Buscar = new System.Windows.Forms.ToolStripButton();
            this.btnAplicarRetencion = new System.Windows.Forms.ToolStripButton();
            this.btnImprimirRetencion = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            this.BarraAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 45;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMes,
            this.colAño,
            this.colFecha,
            this.colNumero,
            this.colNumeroControl,
            this.colMontoExento,
            this.colMontoGravable,
            this.colMontoIva,
            this.colMontoTotal,
            this.colCedulaRif,
            this.colRazonSocial,
            this.colTasaIva,
            this.colLibroCompras,
            this.colMontoSinDerechoCredito});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(808, 338, 216, 199);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            // 
            // colMes
            // 
            this.colMes.FieldName = "Mes";
            this.colMes.Name = "colMes";
            this.colMes.OptionsColumn.FixedWidth = true;
            this.colMes.Visible = true;
            this.colMes.VisibleIndex = 5;
            this.colMes.Width = 40;
            // 
            // colAño
            // 
            this.colAño.FieldName = "Año";
            this.colAño.Name = "colAño";
            this.colAño.OptionsColumn.FixedWidth = true;
            this.colAño.Visible = true;
            this.colAño.VisibleIndex = 6;
            this.colAño.Width = 44;
            // 
            // colFecha
            // 
            this.colFecha.FieldName = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.OptionsColumn.FixedWidth = true;
            this.colFecha.Visible = true;
            this.colFecha.VisibleIndex = 0;
            this.colFecha.Width = 70;
            // 
            // colNumero
            // 
            this.colNumero.FieldName = "Numero";
            this.colNumero.Name = "colNumero";
            this.colNumero.OptionsColumn.FixedWidth = true;
            this.colNumero.Visible = true;
            this.colNumero.VisibleIndex = 1;
            this.colNumero.Width = 70;
            // 
            // colNumeroControl
            // 
            this.colNumeroControl.FieldName = "NumeroControl";
            this.colNumeroControl.Name = "colNumeroControl";
            this.colNumeroControl.OptionsColumn.FixedWidth = true;
            this.colNumeroControl.Visible = true;
            this.colNumeroControl.VisibleIndex = 2;
            this.colNumeroControl.Width = 70;
            // 
            // colMontoExento
            // 
            this.colMontoExento.DisplayFormat.FormatString = "n2";
            this.colMontoExento.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoExento.FieldName = "MontoExento";
            this.colMontoExento.GroupFormat.FormatString = "n2";
            this.colMontoExento.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoExento.Name = "colMontoExento";
            this.colMontoExento.OptionsColumn.FixedWidth = true;
            this.colMontoExento.SummaryItem.DisplayFormat = "{0:n2}";
            this.colMontoExento.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMontoExento.Visible = true;
            this.colMontoExento.VisibleIndex = 7;
            this.colMontoExento.Width = 65;
            // 
            // colMontoGravable
            // 
            this.colMontoGravable.DisplayFormat.FormatString = "n2";
            this.colMontoGravable.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoGravable.FieldName = "MontoGravable";
            this.colMontoGravable.GroupFormat.FormatString = "n2";
            this.colMontoGravable.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoGravable.Name = "colMontoGravable";
            this.colMontoGravable.OptionsColumn.FixedWidth = true;
            this.colMontoGravable.SummaryItem.DisplayFormat = "{0:n2}";
            this.colMontoGravable.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMontoGravable.Visible = true;
            this.colMontoGravable.VisibleIndex = 9;
            // 
            // colMontoIva
            // 
            this.colMontoIva.DisplayFormat.FormatString = "n2";
            this.colMontoIva.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoIva.FieldName = "MontoIva";
            this.colMontoIva.GroupFormat.FormatString = "n2";
            this.colMontoIva.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoIva.Name = "colMontoIva";
            this.colMontoIva.OptionsColumn.FixedWidth = true;
            this.colMontoIva.SummaryItem.DisplayFormat = "{0:n2}";
            this.colMontoIva.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMontoIva.Visible = true;
            this.colMontoIva.VisibleIndex = 10;
            this.colMontoIva.Width = 66;
            // 
            // colMontoTotal
            // 
            this.colMontoTotal.DisplayFormat.FormatString = "n2";
            this.colMontoTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoTotal.FieldName = "MontoTotal";
            this.colMontoTotal.GroupFormat.FormatString = "n2";
            this.colMontoTotal.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoTotal.Name = "colMontoTotal";
            this.colMontoTotal.OptionsColumn.FixedWidth = true;
            this.colMontoTotal.SummaryItem.DisplayFormat = "{0:n2}";
            this.colMontoTotal.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMontoTotal.Visible = true;
            this.colMontoTotal.VisibleIndex = 11;
            this.colMontoTotal.Width = 66;
            // 
            // colCedulaRif
            // 
            this.colCedulaRif.FieldName = "CedulaRif";
            this.colCedulaRif.Name = "colCedulaRif";
            this.colCedulaRif.OptionsColumn.FixedWidth = true;
            this.colCedulaRif.Visible = true;
            this.colCedulaRif.VisibleIndex = 3;
            this.colCedulaRif.Width = 85;
            // 
            // colRazonSocial
            // 
            this.colRazonSocial.FieldName = "RazonSocial";
            this.colRazonSocial.Name = "colRazonSocial";
            this.colRazonSocial.OptionsColumn.FixedWidth = true;
            this.colRazonSocial.Visible = true;
            this.colRazonSocial.VisibleIndex = 4;
            this.colRazonSocial.Width = 175;
            // 
            // colTasaIva
            // 
            this.colTasaIva.FieldName = "TasaIva";
            this.colTasaIva.Name = "colTasaIva";
            this.colTasaIva.OptionsColumn.FixedWidth = true;
            this.colTasaIva.Width = 50;
            // 
            // colLibroCompras
            // 
            this.colLibroCompras.Caption = "Libro";
            this.colLibroCompras.FieldName = "LibroCompras";
            this.colLibroCompras.Name = "colLibroCompras";
            this.colLibroCompras.OptionsColumn.FixedWidth = true;
            this.colLibroCompras.Visible = true;
            this.colLibroCompras.VisibleIndex = 12;
            this.colLibroCompras.Width = 51;
            // 
            // colMontoSinDerechoCredito
            // 
            this.colMontoSinDerechoCredito.Caption = "Sin Credito Fiscal";
            this.colMontoSinDerechoCredito.DisplayFormat.FormatString = "n2";
            this.colMontoSinDerechoCredito.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoSinDerechoCredito.FieldName = "MontoSinDerechoCredito";
            this.colMontoSinDerechoCredito.GroupFormat.FormatString = "n2";
            this.colMontoSinDerechoCredito.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoSinDerechoCredito.Name = "colMontoSinDerechoCredito";
            this.colMontoSinDerechoCredito.OptionsColumn.FixedWidth = true;
            this.colMontoSinDerechoCredito.SummaryItem.DisplayFormat = "{0:n2}";
            this.colMontoSinDerechoCredito.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMontoSinDerechoCredito.Visible = true;
            this.colMontoSinDerechoCredito.VisibleIndex = 8;
            this.colMontoSinDerechoCredito.Width = 67;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.bs;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Begin;
            this.gridControl1.Location = new System.Drawing.Point(0, 56);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ShowOnlyPredefinedDetails = true;
            this.gridControl1.Size = new System.Drawing.Size(1016, 508);
            this.gridControl1.TabIndex = 20;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bs
            // 
            this.bs.DataSource = typeof(HK.Compra);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnVer,
            this.btnEliminar,
            this.toolStripSeparator3,
            this.txtFiltro,
            this.txtBuscar,
            this.Buscar,
            this.toolStripSeparator1,
            this.btnAplicarRetencion,
            this.btnImprimirRetencion});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 0);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(1016, 53);
            this.BarraAcciones.TabIndex = 19;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::HK.Properties.Resources.note_new;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(78, 50);
            this.btnNuevo.Text = "Nuevo";
            // 
            // btnVer
            // 
            this.btnVer.Image = global::HK.Properties.Resources.note_view;
            this.btnVer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(60, 50);
            this.btnVer.Text = "Ver";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::HK.Properties.Resources.note_delete;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(86, 50);
            this.btnEliminar.Text = "Eliminar";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Items.AddRange(new object[] {
            "HOY",
            "AYER",
            "ESTE MES",
            "TODAS"});
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(121, 53);
            this.txtFiltro.Text = "HOY";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(104, 53);
            // 
            // Buscar
            // 
            this.Buscar.Image = global::HK.Properties.Resources.note_find;
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(46, 50);
            this.Buscar.Text = "Buscar";
            this.Buscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnAplicarRetencion
            // 
            this.btnAplicarRetencion.Image = global::HK.Properties.Resources.clipboard;
            this.btnAplicarRetencion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAplicarRetencion.Name = "btnAplicarRetencion";
            this.btnAplicarRetencion.Size = new System.Drawing.Size(104, 50);
            this.btnAplicarRetencion.Text = "Aplicar Retencion";
            this.btnAplicarRetencion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnImprimirRetencion
            // 
            this.btnImprimirRetencion.Image = global::HK.Properties.Resources.printer;
            this.btnImprimirRetencion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimirRetencion.Name = "btnImprimirRetencion";
            this.btnImprimirRetencion.Size = new System.Drawing.Size(113, 50);
            this.btnImprimirRetencion.Text = "Imprimir Retencion";
            this.btnImprimirRetencion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FrmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 566);
            this.Controls.Add(this.BarraAcciones);
            this.Controls.Add(this.gridControl1);
            this.Name = "FrmCompras";
            this.Text = "Compras";
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Buscar;
        private System.Windows.Forms.ToolStripComboBox txtFiltro;
        private System.Windows.Forms.ToolStripTextBox txtBuscar;
        private DevExpress.XtraGrid.Columns.GridColumn colMes;
        private DevExpress.XtraGrid.Columns.GridColumn colAño;
        private DevExpress.XtraGrid.Columns.GridColumn colFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colNumero;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroControl;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoExento;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoGravable;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoIva;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colCedulaRif;
        private DevExpress.XtraGrid.Columns.GridColumn colRazonSocial;
        private DevExpress.XtraGrid.Columns.GridColumn colTasaIva;
        private DevExpress.XtraGrid.Columns.GridColumn colLibroCompras;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoSinDerechoCredito;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnVer;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnAplicarRetencion;
        private System.Windows.Forms.ToolStripButton btnImprimirRetencion;
    }
}