using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Productos : Form
    {
        BLLProducto obllProducto;
        List<BEProducto> productos;
        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
                actualizarGrilla();
            }
            catch (Exception ex) { throw ex; }
        }
        private void actualizarGrilla()
        {
            try
            {
                obllProducto = new BLLProducto();
                dataGridView1.DataSource = null;
                productos = obllProducto.ListarTodo();
                dataGridView1.DataSource = productos;
               
            }
            catch (Exception ex) { throw ex; }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    textBox1.Enabled = false;
                    DataGridViewColumn column = dataGridView1.Columns["Codigo"];
                    BEProducto p = productos.Find(pro => pro.Codigo == int.Parse(dataGridView1.SelectedCells[column.Index].Value.ToString() ) );
                    textBox1.Text = p.Codigo.ToString();
                    textBox2.Text = p.Descripcion.ToString();
                    uC_int1.textBox1.Text = p.Precio.ToString();
                    uC_int2.textBox1.Text = p.Cantidad.ToString();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BEProducto prod = new BEProducto();
                if (!uC_int1.validar()) { throw new Exception("El precio debe ser un numero entero y positivo y no tener mas de 9 digitos"); }
                if (!uC_int2.validar()) { throw new Exception("La cantidad debe ser un numero entero y positivo y no tener mas de 9 digitos "); }
                prod.Precio = int.Parse(uC_int1.textBox1.Text);
                prod.Descripcion = textBox2.Text.Trim();
                prod.Cantidad = int.Parse(uC_int2.textBox1.Text.Trim());
                obllProducto.Guardar(prod);
                actualizarGrilla();
            }
            catch (Exception ex) { throw ex; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (!uC_int1.validar()) { throw new Exception("El precio debe ser un numero entero y positivo y no tener mas de 9 digitos"); }
                    if (!uC_int2.validar()) { throw new Exception("La cantidad debe ser un numero entero y positivo y no tener mas de 9 digitos"); }
                    BEProducto prod = new BEProducto();
                    prod.Precio = int.Parse(uC_int1.textBox1.Text);
                    prod.Descripcion = textBox2.Text.Trim();
                    BEProducto aux = productos.Find(p => p.Codigo == int.Parse(textBox1.Text.ToString()));
                    prod.Codigo = aux.Codigo;
                    prod.Cantidad = aux.Cantidad;
                    obllProducto.Guardar(prod);
                    actualizarGrilla();
                } 
            }
            catch (Exception ex) { throw ex; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0) // dataGridView1.SelectedCells[2].Value
                {
                    DialogResult rta = MessageBox.Show($"¿Confirma la eleminación del producto {dataGridView1.SelectedRows[0].Cells["Descripcion"].Value} ?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (rta == DialogResult.Yes)
                    {
                        BEProducto prod = new BEProducto();
                        prod.Codigo = int.Parse(dataGridView1.SelectedRows[0].Cells["Codigo"].Value.ToString());
                        if (obllProducto.Baja(prod) == false) { MessageBox.Show("El producto no puede estar asociado a ninguna compra!"); }
                        else
                        {
                            actualizarGrilla();
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        
    }
}
