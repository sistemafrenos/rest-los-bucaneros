namespace HK.Formas
{
    partial class FrmPedirContornos
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
            this.platosContornoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Aceptar = new System.Windows.Forms.ToolStripButton();
            this.BarraAcciones = new System.Windows.Forms.ToolStrip();
            this.Cancelar = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtContornos = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.txtComentarios = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.platosComentarioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Presentacion0 = new System.Windows.Forms.Button();
            this.Presentacion1 = new System.Windows.Forms.Button();
            this.Presentacion2 = new System.Windows.Forms.Button();
            this.Presentacion3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.platosContornoBindingSource)).BeginInit();
            this.BarraAcciones.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContornos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComentarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.platosComentarioBindingSource)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // platosContornoBindingSource
            // 
            this.platosContornoBindingSource.DataSource = typeof(HK.PlatosContorno);
            // 
            // Aceptar
            // 
            this.Aceptar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Aceptar.Image = global::HK.Properties.Resources.disk_blue_ok;
            this.Aceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(81, 51);
            this.Aceptar.Text = "Aceptar - F12";
            this.Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // BarraAcciones
            // 
            this.BarraAcciones.AutoSize = false;
            this.BarraAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BarraAcciones.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.BarraAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Aceptar,
            this.Cancelar});
            this.BarraAcciones.Location = new System.Drawing.Point(0, 259);
            this.BarraAcciones.Name = "BarraAcciones";
            this.BarraAcciones.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.BarraAcciones.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BarraAcciones.Size = new System.Drawing.Size(576, 54);
            this.BarraAcciones.TabIndex = 46;
            this.BarraAcciones.Text = "toolStrip1";
            // 
            // Cancelar
            // 
            this.Cancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Cancelar.Image = global::HK.Properties.Resources.disk_blue_error;
            this.Cancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(88, 51);
            this.Cancelar.Text = "Cancelar - ESC";
            this.Cancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtContornos);
            this.flowLayoutPanel1.Controls.Add(this.txtComentarios);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(557, 234);
            this.flowLayoutPanel1.TabIndex = 51;
            // 
            // txtContornos
            // 
            this.txtContornos.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtContornos.Appearance.Options.UseFont = true;
            this.txtContornos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtContornos.CheckOnClick = true;
            this.txtContornos.DataSource = this.platosContornoBindingSource;
            this.txtContornos.DisplayMember = "Contorno";
            this.txtContornos.HotTrackItems = true;
            this.txtContornos.Location = new System.Drawing.Point(3, 3);
            this.txtContornos.Name = "txtContornos";
            this.txtContornos.Size = new System.Drawing.Size(271, 141);
            this.txtContornos.TabIndex = 51;
            this.txtContornos.Visible = false;
            // 
            // txtComentarios
            // 
            this.txtComentarios.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtComentarios.Appearance.Options.UseFont = true;
            this.txtComentarios.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtComentarios.CheckOnClick = true;
            this.txtComentarios.DataSource = this.platosComentarioBindingSource;
            this.txtComentarios.DisplayMember = "Comentario";
            this.txtComentarios.HotTrackItems = true;
            this.txtComentarios.Location = new System.Drawing.Point(280, 3);
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(271, 141);
            this.txtComentarios.TabIndex = 52;
            this.txtComentarios.Visible = false;
            // 
            // platosComentarioBindingSource
            // 
            this.platosComentarioBindingSource.DataSource = typeof(HK.PlatosComentario);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.Presentacion0);
            this.flowLayoutPanel2.Controls.Add(this.Presentacion1);
            this.flowLayoutPanel2.Controls.Add(this.Presentacion2);
            this.flowLayoutPanel2.Controls.Add(this.Presentacion3);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 150);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(548, 80);
            this.flowLayoutPanel2.TabIndex = 53;
            // 
            // Presentacion0
            // 
            this.Presentacion0.Location = new System.Drawing.Point(3, 3);
            this.Presentacion0.Name = "Presentacion0";
            this.Presentacion0.Size = new System.Drawing.Size(131, 72);
            this.Presentacion0.TabIndex = 0;
            this.Presentacion0.Text = "Presentacion0";
            this.Presentacion0.UseVisualStyleBackColor = true;
            this.Presentacion0.Visible = false;
            // 
            // Presentacion1
            // 
            this.Presentacion1.Location = new System.Drawing.Point(140, 3);
            this.Presentacion1.Name = "Presentacion1";
            this.Presentacion1.Size = new System.Drawing.Size(131, 72);
            this.Presentacion1.TabIndex = 1;
            this.Presentacion1.Text = "Presentacion1";
            this.Presentacion1.UseVisualStyleBackColor = true;
            this.Presentacion1.Visible = false;
            // 
            // Presentacion2
            // 
            this.Presentacion2.Location = new System.Drawing.Point(277, 3);
            this.Presentacion2.Name = "Presentacion2";
            this.Presentacion2.Size = new System.Drawing.Size(131, 72);
            this.Presentacion2.TabIndex = 2;
            this.Presentacion2.Text = "Presentacion2";
            this.Presentacion2.UseVisualStyleBackColor = true;
            this.Presentacion2.Visible = false;
            // 
            // Presentacion3
            // 
            this.Presentacion3.Location = new System.Drawing.Point(414, 3);
            this.Presentacion3.Name = "Presentacion3";
            this.Presentacion3.Size = new System.Drawing.Size(131, 72);
            this.Presentacion3.TabIndex = 3;
            this.Presentacion3.Text = "Presentacion3";
            this.Presentacion3.UseVisualStyleBackColor = true;
            this.Presentacion3.Visible = false;
            // 
            // FrmPedirContornos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 313);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.BarraAcciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPedirContornos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalles del Plato";
            ((System.ComponentModel.ISupportInitialize)(this.platosContornoBindingSource)).EndInit();
            this.BarraAcciones.ResumeLayout(false);
            this.BarraAcciones.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContornos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComentarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.platosComentarioBindingSource)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource platosContornoBindingSource;
        private System.Windows.Forms.ToolStripButton Aceptar;
        public System.Windows.Forms.ToolStrip BarraAcciones;
        private System.Windows.Forms.ToolStripButton Cancelar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.CheckedListBoxControl txtContornos;
        private DevExpress.XtraEditors.CheckedListBoxControl txtComentarios;
        private System.Windows.Forms.BindingSource platosComentarioBindingSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button Presentacion0;
        private System.Windows.Forms.Button Presentacion1;
        private System.Windows.Forms.Button Presentacion2;
        private System.Windows.Forms.Button Presentacion3;
    }
}