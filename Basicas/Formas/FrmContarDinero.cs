using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HK.Formas
{
    public partial class FrmContarDinero : Form
    {
        public FrmContarDinero()
        {
            InitializeComponent();
        }

        private void spinEdit1_Validating(object sender, CancelEventArgs e)
        {
            txt500.EditValue = this.s500.Value * 500;
            txt100.EditValue = this.s100.Value * 100;
            txt50.EditValue = this.s50.Value * 50;
            txt20.EditValue = this.s20.Value * 20;
            txt10.EditValue = this.s10.Value * 10;
            txt5.EditValue = this.s5.Value * 5;
            txt2.EditValue = this.s2.Value * 2;
            txt1.EditValue = this.s1.Value;
            txtTotal.EditValue = 0;
            try
            {
                txtTotal.EditValue = (decimal)txt500.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt100.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt50.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt20.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt10.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt5.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt2.EditValue;
                txtTotal.EditValue = (decimal)txtTotal.EditValue + (decimal)txt1.EditValue;
            }
            catch { }
        }

        private void txt500_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LPrintWriter p = new LPrintWriter();
            p.Font = new Font("Courier New", 12, FontStyle.Bold);
            p.WriteLine("CONTADOR DE DINERO");
            p.WriteLine("==================");
            p.WriteLine(string.Format("{0} x {1}  = {2}", s500.Value.ToString("N2").PadLeft(10), "500",((decimal)txt500.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s100.Value.ToString("N2").PadLeft(10), "100",((decimal)txt100.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s50.Value.ToString("N2").PadLeft(10), " 50",((decimal)txt50.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s20.Value.ToString("N2").PadLeft(10), " 20",((decimal)txt20.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s10.Value.ToString("N2").PadLeft(10), " 10",((decimal)txt10.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s5.Value.ToString("N2").PadLeft(10), "  5", ((decimal)txt5.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s2.Value.ToString("N2").PadLeft(10), "  2", ((decimal)txt2.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine(string.Format("{0} x {1}  = {2}", s1.Value.ToString("N2").PadLeft(10), "  1", ((decimal)txt1.EditValue).ToString("N2").PadLeft(10)));
            p.WriteLine();
            p.WriteLine(string.Format("TOTAL CONTADO:{0}",((decimal)(txtTotal.EditValue)).ToString("N2").PadLeft(10)));
            p.WriteLine(".");
            p.WriteLine(".");
            p.WriteLine(".");
            p.WriteLine(".");
            p.Flush();
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}