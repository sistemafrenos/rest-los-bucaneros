using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HK.Formas
{
    public partial class FrmVisualizarReportes : Form
    {
        public FrmVisualizarReportes()
        {
            InitializeComponent();
            
        }

        public StringWriter texto = new StringWriter();
        #region Private Fields
        private System.Drawing.Printing.PrintDocument printDocument;
        private string[] lines;
        private int linesPrinted;
        private Font font = new Font("Courier New", 10.0f);
        private Color textColor = Color.Black;
        #endregion
        #region Overriddden Methods
        /// <summary>
        /// Closes the current <see cref="T:Util.LPrintWriter"/> and the underlying stream.
        /// </summary>
        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered
        /// data to be written to the underlying device.
        /// </summary>
        public void Flush()
        {
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDocument.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            printDocument.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            //  printDocument.PrinterSettings.PrinterName = "TICKETS";
            printDocument.DefaultPageSettings.Margins.Left = 0;
            printDocument.DefaultPageSettings.Margins.Right = 0;
            printDocument.DefaultPageSettings.Margins.Bottom = 0;
            printDocument.DefaultPageSettings.Margins.Top = 0;
            this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.OnBeginPrint);
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.OnPrintPage);
            if (printDocument == null)
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
                printDialog.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
                printDialog.Document = new System.Drawing.Printing.PrintDocument();
                printDialog.Document.DefaultPageSettings.Margins.Left = 0;
                printDialog.Document.DefaultPageSettings.Margins.Right = 0;
                printDialog.Document.DefaultPageSettings.Margins.Bottom = 0;
                printDialog.Document.DefaultPageSettings.Margins.Top = 0;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument = printDialog.Document;
                    this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.OnBeginPrint);
                    this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.OnPrintPage);
                }
            }
            if (printDocument != null)
            {
                printDocument.Print();
                texto.GetStringBuilder().Length = 0;
            }
            texto.Flush();
            this.printPreviewControl1.Document = printDocument;
            this.printPreviewControl1.Refresh();
        }
        #endregion

        #region Event Handlers
        // OnBeginPrint 
        /// <summary>
        /// Called when [begin print].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintEventArgs"/> instance containing the event data.</param>
        private void OnBeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            char[] param = { '\n' };
            lines = ToString().Split(param);

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        // OnPrintPage
        /// <summary>
        /// Called when [print page].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            SizeF size = e.Graphics.MeasureString("W", font);
            Brush brush = new System.Drawing.SolidBrush(textColor);
            int DeltaY = (int)size.Height;

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++], font, brush, x, y);
                y += DeltaY;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }
        #endregion
    }
}
