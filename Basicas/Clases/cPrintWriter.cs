
#region Namespace references
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
#endregion

namespace HK
{
	/// <summary>
	/// LPrintWriter - A TextWriter derived class for printing text to a print.
	/// </summary>
    /// <example><pre>
    /// LPrintWriter lprint = new LPrintWiter();
    /// 
    /// lprint.WriteLine("Hello, Nurse!");
    /// 
    /// foreach (Char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
    /// {
    ///     lprint.Write(c);
    /// }
    ///
    /// for(int i = 1; i &lt; 20; i++)
    /// {
    ///     lprint.WriteLine(i);
    /// }
    ///
    /// lprint.Font = new System.Drawing.Font("Arial", 22.0f);
    /// lprint.TextColor = Color.Blue;
    ///
    /// </pre></example>
    ///<remarks>
    /// Copyright &#169; 2006, James M. Curran. <br/>
    /// First published on CodeProject.com, June 2006 <br/>
    /// May be used freely.
    ///</remarks>
	public class LPrintWriter : StringWriter
	{
        #region Private Fields
        private System.Drawing.Printing.PrintDocument printDocument;
        private string[] lines;
        private int linesPrinted;
        private Font     font        = new Font("Courier New", 10.0f);
        private Color    textColor   = Color.Black;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LPrintWriter"/> class.
        /// </summary>
		public LPrintWriter()
		{
        }
        #endregion

        #region Overriddden Methods
        /// <summary>
        /// Closes the current <see cref="T:Util.LPrintWriter"/> and the underlying stream.
        /// </summary>
        public override void Close()
        {
            this.Flush();
            base.Close ();
        }
        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered
        /// data to be written to the underlying device.
        /// </summary>
        public override void Flush()
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
                printDialog.PrinterSettings.DefaultPageSettings.Margins.Left= 0;                
                printDialog.PrinterSettings.DefaultPageSettings.Margins.Top = 0;                
                printDialog.Document = new System.Drawing.Printing.PrintDocument();
                printDialog.Document.DefaultPageSettings.Margins.Left = 0;
                printDialog.Document.DefaultPageSettings.Margins.Right = 0;
                printDialog.Document.DefaultPageSettings.Margins.Bottom = 0;
                printDialog.Document.DefaultPageSettings.Margins.Top = 0;
                if(printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument = printDialog.Document;
                    this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.OnBeginPrint);
                    this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.OnPrintPage);
                }
            }
            if (printDocument != null)
            {
                printDocument.Print();
                base.GetStringBuilder().Length = 0;
            }
                base.Flush ();
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
            char[] param = {'\n'};
            lines = ToString().Split(param);
			
            int i = 0;
            char[] trimParam = {'\r'};
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
            int DeltaY = (int) size.Height;
			
            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString (lines[linesPrinted++], font, brush, x, y);
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

        #region Public Properties
        /// <summary>
        /// Gets or sets the font. (Default: Courier New, 10pt)
        /// </summary>
        /// <value>The font.</value>
        public Font Font
        {
            set
            {
                font = value;
            }
            get
            {
                return font;
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.  (Default: Black)
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
            }
        }
        #endregion
	}
}
