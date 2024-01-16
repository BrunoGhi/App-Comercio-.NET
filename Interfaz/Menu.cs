using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras fr = new Compras();
            fr.MdiParent = this;
            fr.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes fr = new Clientes();
            fr.MdiParent = this;
            fr.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos fr = new Productos();
            fr.MdiParent = this;
            fr.Show();
        }
    }
}
