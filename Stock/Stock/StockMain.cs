using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class StockMain : Form
    {
       
        public StockMain()
        {
            InitializeComponent();
        }

        private void ProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.MdiParent = this;
            products.Show();
        }

        private void StockMain_FormClosing_Click(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void StockMain_Load(object sender, EventArgs e)
        {

        }
    }
}
