using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;

namespace Interfaz
{
    public partial class HistorialCompras : Form
    {
        List<BECompra> compras;
        
        public HistorialCompras(BECliente pCliente)
        {
            try
            {
                InitializeComponent();
                BLLCliente cliente = new BLLCliente();
                compras = cliente.listaCompras(pCliente);
            }
            catch (Exception ex) { throw ex; }
        }

        private void HistorialCompras_Load(object sender, EventArgs e)
        {
            try
            {
                if (compras.Count > 0)
                {

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
                    dataGridView1.Enabled = false;
                    foreach (BECompra c in compras)
                    {
                        foreach (BEProducto p in c.productos)
                        {
                            dataGridView1.Rows.Add(new string[] { $"{c.Codigo}", $"{p.Descripcion}", $"{p.Precio}",$"{p.Cantidad}" ,c.garantia == null ? "No" : $"{c.garantia.duracionDias} dias ", $"{c.Monto}" });
                        }
                        dataGridView1.Rows.Add(new string[] { "", "", "", "", "","" });
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
