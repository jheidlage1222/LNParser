using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoopnetScrapper
{
    public partial class LNScrapperGUI : Form
    {
        public LNScrapperGUI()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            //LoopnetScrapperDriver scrapper = new LoopnetScrapperDriver();
            //scrapper.CompileTestRun(tb_UID.Text, tb_Password.Text);
            try
            {
                LoopNetHTTP.LoopNetParser parser = new LoopNetHTTP.LoopNetParser();
                parser.MakeRequests();
            }
            catch (Exception ex)
            {
                tb_Output.Text = ex.Message + Environment.NewLine;
                if (ex.InnerException != null)
                {
                    tb_Output.Text += ex.InnerException.Message + Environment.NewLine;
                }
                //
                tb_Output.Text += ex.StackTrace + Environment.NewLine;
                if (ex.InnerException != null)
                {
                    tb_Output.Text += ex.InnerException.StackTrace + Environment.NewLine;
                }
            }
        }
    }
}
