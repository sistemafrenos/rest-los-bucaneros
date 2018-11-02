namespace HK.Formas
{
    partial class FrmFacturas
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.VerFactura = new System.Windows.Forms.ToolStripButton();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.Duplicar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Eliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Buscar = new System.Windows.Forms.ToolStripButton();
            this.txtFiltro = new System.Windows.Forms.ToolStripComboBox();
            this.txtBuscar = new System.Windows.Forms.ToolStripTextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCedulaRif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRazonSocial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEfectivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTarjeta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumeroZ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHora = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAnulado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheque = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRetencion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransferencia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsumoInterno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMontoFactura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BarraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // VerFactura
            // 
            this.VerFactura.Image = global::HK.Properties.Resources.note_view;
            this.VerFactura.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VerFactura.Name = "VerFactura";
            this.VerFactura.Size = new System.Drawing.Size(69, 50);
            this.VerFactura.Text = "Ver Factura";
            this.VerFactura.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Duplicar,
            this.toolStripSeparator3,
            this.VerFactura,
            this.toolStripSeparator1,
            this.Eliminar,
            this.toolStripSeparator2,
            this.Buscar,
            this.txtFiltro,
            this.txtBuscar});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 0);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(1184, 53);
            this.BarraAcciones.TabIndex = 17;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // Duplicar
            // 
            this.Duplicar.Image = global::HK.Properties.Resources.printer1;
            this.Duplicar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Duplicar.Name = "Duplicar";
            this.Duplicar.Size = new System.Drawing.Size(91, 50);
            this.Duplicar.Text = "Imprimir Copia";
            this.Duplicar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 53);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // Eliminar
            // 
            this.Eliminar.Image = global::HK.Properties.Resources.note_delete;
            this.Eliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(88, 50);
            this.Eliminar.Text = "Anular Factura";
            this.Eliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            // 
            // Buscar
            // 
            this.Buscar.Image = global::HK.Properties.Resources.note_find;
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(46, 50);
            this.Buscar.Text = "Buscar";
            this.Buscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // txtFiltro
            // 
            this.txtFiltro.Items.AddRange(new object[] {
            "HOY",
            "AYER",
            "ESTA SEMANA",
            "ESTE MES",
            "ESTE AÑO",
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
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.gridControl1.Size = new System.Drawing.Size(1184, 690);
            this.gridControl1.TabIndex = 18;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bs
            // 
            this.bs.DataSource = typeof(HK.Factura);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 45;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFecha,
            this.colNumero,
            this.colCedulaRif,
            this.colRazonSocial,
            this.colEfectivo,
            this.colTarjeta,
            this.colNumeroZ,
            this.colHora,
            this.colAnulado,
            this.colCheque,
            this.colRetencion,
            this.colTransferencia,
            this.colConsumoInterno,
            this.colMontoFactura});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(808, 338, 216, 199);
            styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout);
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseFont = true;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Expression = "[Anulado]";
            styleFormatCondition1.Value1 = "True";
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colFecha
            // 
            this.colFecha.FieldName = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.OptionsColumn.FixedWidth = true;
            this.colFecha.Visible = true;
            this.colFecha.VisibleIndex = 0;
            this.colFecha.Width = 65;
            // 
            // colNumero
            // 
            this.colNumero.FieldName = "Numero";
            this.colNumero.Name = "colNumero";
            this.colNumero.OptionsColumn.FixedWidth = true;
            this.colNumero.Visible = true;
            this.colNumero.VisibleIndex = 1;
            // 
            // colCedulaRif
            // 
            this.colCedulaRif.FieldName = "CedulaRif";
            this.colCedulaRif.Name = "colCedulaRif";
            this.colCedulaRif.OptionsColumn.FixedWidth = true;
            this.colCedulaRif.Visible = true;
            this.colCedulaRif.VisibleIndex = 3;
            this.colCedulaRif.Width = 95;
            // 
            // colRazonSocial
            // 
            this.colRazonSocial.FieldName = "RazonSocial";
            this.colRazonSocial.Name = "colRazonSocial";
            this.colRazonSocial.Visible = true;
            this.colRazonSocial.VisibleIndex = 4;
            this.colRazonSocial.Width = 189;
            // 
            // colEfectivo
            // 
            this.colEfectivo.DisplayFormat.FormatString = "n2";
            this.colEfectivo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEfectivo.FieldName = "Efectivo";
            this.colEfectivo.Name = "colEfectivo";
            this.colEfectivo.OptionsColumn.FixedWidth = true;
            this.colEfectivo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Efectivo", "{0:n2}")});
            this.colEfectivo.Visible = true;
            this.colEfectivo.VisibleIndex = 7;
            // 
            // colTarjeta
            // 
            this.colTarjeta.DisplayFormat.FormatString = "n2";
            this.colTarjeta.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTarjeta.FieldName = "Tarjeta";
            this.colTarjeta.Name = "colTarjeta";
            this.colTarjeta.OptionsColumn.FixedWidth = true;
            this.colTarjeta.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Tarjeta", "{0:n2}")});
            this.colTarjeta.Visible = true;
            this.colTarjeta.VisibleIndex = 8;
            // 
            // colNumeroZ
            // 
            this.colNumeroZ.Caption = "Nro.Z";
            this.colNumeroZ.FieldName = "NumeroZ";
            this.colNumeroZ.Name = "colNumeroZ";
            this.colNumeroZ.OptionsColumn.FixedWidth = true;
            this.colNumeroZ.Visible = true;
            this.colNumeroZ.VisibleIndex = 12;
            this.colNumeroZ.Width = 55;
            // 
            // colHora
            // 
            this.colHora.DisplayFormat.FormatString = "t";
            this.colHora.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colHora.FieldName = "Hora";
            this.colHora.Name = "colHora";
            this.colHora.OptionsColumn.FixedWidth = true;
            this.colHora.Visible = true;
            this.colHora.VisibleIndex = 2;
            this.colHora.Width = 55;
            // 
            // colAnulado
            // 
            this.colAnulado.Caption = "Anulado";
            this.colAnulado.FieldName = "Anulado";
            this.colAnulado.Name = "colAnulado";
            this.colAnulado.Width = 45;
            // 
            // colCheque
            // 
            this.colCheque.DisplayFormat.FormatString = "n2";
            this.colCheque.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCheque.FieldName = "Cheque";
            this.colCheque.Name = "colCheque";
            this.colCheque.OptionsColumn.FixedWidth = true;
            this.colCheque.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Cheque", "{0:n2}")});
            this.colCheque.Visible = true;
            this.colCheque.VisibleIndex = 9;
            // 
            // colRetencion
            // 
            this.colRetencion.DisplayFormat.FormatString = "n2";
            this.colRetencion.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRetencion.FieldName = "Retencion";
            this.colRetencion.Name = "colRetencion";
            this.colRetencion.OptionsColumn.FixedWidth = true;
            this.colRetencion.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Retencion", "{0:n2}")});
            this.colRetencion.Visible = true;
            this.colRetencion.VisibleIndex = 10;
            // 
            // colTransferencia
            // 
            this.colTransferencia.DisplayFormat.FormatString = "n2";
            this.colTransferencia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTransferencia.FieldName = "Transferencia";
            this.colTransferencia.Name = "colTransferencia";
            this.colTransferencia.OptionsColumn.FixedWidth = true;
            this.colTransferencia.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Transferencia", "{0:n2}")});
            this.colTransferencia.Visible = true;
            this.colTransferencia.VisibleIndex = 11;
            // 
            // colConsumoInterno
            // 
            this.colConsumoInterno.Caption = "Interno";
            this.colConsumoInterno.DisplayFormat.FormatString = "n2";
            this.colConsumoInterno.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colConsumoInterno.FieldName = "ConsumoInterno";
            this.colConsumoInterno.Name = "colConsumoInterno";
            this.colConsumoInterno.OptionsColumn.FixedWidth = true;
            this.colConsumoInterno.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ConsumoInterno", "{0:n2}")});
            this.colConsumoInterno.Visible = true;
            this.colConsumoInterno.VisibleIndex = 6;
            this.colConsumoInterno.Width = 72;
            // 
            // colMontoFactura
            // 
            this.colMontoFactura.Caption = "Monto";
            this.colMontoFactura.DisplayFormat.FormatString = "n2";
            this.colMontoFactura.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMontoFactura.FieldName = "MontoTotal";
            this.colMontoFactura.Name = "colMontoFactura";
            this.colMontoFactura.OptionsColumn.FixedWidth = true;
            this.colMontoFactura.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MontoTotal", "{0:n2}")});
            this.colMontoFactura.Visible = true;
            this.colMontoFactura.VisibleIndex = 5;
            this.colMontoFactura.Width = 86;
            // 
            // FrmFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 746);
            this.Controls.Add(this.BarraAcciones);
            this.Controls.Add(this.gridControl1);
            this.Name = "FrmFacturas";
            this.Text = "Facturas";
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.ToolStripButton VerFactura;
        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Eliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton Buscar;
        private System.Windows.Forms.ToolStripComboBox txtFiltro;
        private System.Windows.Forms.ToolStripTextBox txtBuscar;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStripButton Duplicar;
        private DevExpress.XtraGrid.Columns.GridColumn colFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colNumero;
        private DevExpress.XtraGrid.Columns.GridColumn colCedulaRif;
        private DevExpress.XtraGrid.Columns.GridColumn colRazonSocial;
        private DevExpress.XtraGrid.Columns.GridColumn colEfectivo;
        private DevExpress.XtraGrid.Columns.GridColumn colTarjeta;
        private DevExpress.XtraGrid.Columns.GridColumn colNumeroZ;
        private DevExpress.XtraGrid.Columns.GridColumn colHora;
        private DevExpress.XtraGrid.Columns.GridColumn colAnulado;
        private DevExpress.XtraGrid.Columns.GridColumn colCheque;
        private DevExpress.XtraGrid.Columns.GridColumn colRetencion;
        private DevExpress.XtraGrid.Columns.GridColumn colTransferencia;
        private DevExpress.XtraGrid.Columns.GridColumn colConsumoInterno;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoFactura;
    }
}