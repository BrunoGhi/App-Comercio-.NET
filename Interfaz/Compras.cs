using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace Interfaz
{
    public partial class Compras : Form
    {
        BLLProducto obllProducto;

        List<BEProducto> carrito;

        BLLCliente obllCliente;

        BECompra compra;
        BLLCompra bCompra;

        public Compras()
        {
            InitializeComponent();
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.IndianRed;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Chartreuse;
                dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSalmon;

                reiniciar();
                comboBox1.Items.AddRange(new string[] { "30 días", "60 días", "90 días" });
                comboBox1.SelectedIndex = 0;
                actualizarGrillaProductos();
                actualizarGrillaCarrito();
                actualizarGrillaClientes();
            }
            catch (Exception ex) { throw ex; }
        }
        private void reiniciar()
        {
            try
            {
                compra = new BECompra();
                compra.productos = new List<BEProducto>();
                carrito = compra.productos;
                compra.cliente = new BECliente();
                //cliente = compra.cliente;
            }
            catch (Exception ex) { throw ex; }
        }

        private void actualizarGrillaClientes()
        {
            try
            {
                obllCliente = new BLLCliente();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = obllCliente.listarTodo();

            }
            catch (Exception ex) { throw ex; }
        }

        private void actualizarGrillaProductos()
        {
            try
            {
                obllProducto = new BLLProducto();
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = obllProducto.ListarTodo();
            }
            catch (Exception ex) { throw ex; }
        }

        private void actualizarGrillaCarrito()
        {
            try
            {
                dataGridView3.DataSource = null;
                dataGridView3.DataSource = carrito.ToList();

                if (carrito.Count > 0)
                {
                    dataGridView3.Rows[0].Selected = true;
                    dataGridView1.Enabled = false;
                }
                else { dataGridView1.Enabled = true; }
            }
            catch (Exception ex) { throw ex; }
        }
        private void actualizarPrecioCarrito()
        {
            try
            {
                label6.Text = string.Empty;
                float precio = 0;

                if (compra.cliente is BEEstandar)
                {
                    BLLEstandar std = new BLLEstandar();
                    precio = std.CalcularPrecio(compra);
                }
                else
                {
                    BLLPremium prem = new BLLPremium();
                    precio = prem.CalcularPrecio(compra);
                }
                compra.Monto = precio;
                label6.Text = compra.Monto.ToString();
            }
            catch (Exception ex) { throw ex; }
        }



        // Ver historial de compras
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dataGridView1.SelectedRows.Count > 0))
                {
                    HistorialCompras fr = new HistorialCompras(compra.cliente);
                    fr.Show();
                }
            }
            catch (Exception ex) { throw ex; }
        } 



        // agregar al carrito
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (hayClientes() & hayProductos())
                {
                    BEProducto producto = new BEProducto();
                    BEProducto pr = (BEProducto)dataGridView2.SelectedRows[0].DataBoundItem;
                    producto.Descripcion = pr.Descripcion;
                    producto.Codigo = pr.Codigo;
                    producto.Precio = pr.Precio;
                    producto.Cantidad = pr.Cantidad;
                    if (producto.Cantidad > 0)
                    {
                        if (carrito.Count > 0)
                        {
                            BEProducto productoExistente = carrito.Find(p => p.Codigo == producto.Codigo);
                            if (productoExistente != null)
                            {
                                if(productoExistente.Cantidad + 1 > producto.Cantidad)
                                {
                                    MessageBox.Show("No se puede comprar más de la cantidad disponible! ");
                                }
                                else
                                {
                                    productoExistente.Cantidad += 1;
                                }
                                
                            }
                            else
                            {
                                producto.Cantidad = 1;
                                carrito.Add(producto);
                            }
                        }
                        else
                        {
                            producto.Cantidad = 1;
                            carrito.Add(producto);
                        }
                    }
                    actualizarGrillaCarrito();
                    actualizarPrecioCarrito();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private bool hayClientes()
        {
            try
            {
                bool rta = false;
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    rta = true;
                }
                return rta;
            }
            catch (Exception ex) { throw ex; }
        }

        private bool hayProductos()
        {
            try
            {
                bool rta = false;
                if (dataGridView2.SelectedCells.Count > 0)
                {
                    rta = true;
                }
                return rta;
            }
            catch (Exception ex) { throw ex; }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.SelectedRows.Count > 0)
                {
                    compra.cliente = (BECliente)dataGridView1.SelectedRows[0].DataBoundItem;
                    if (compra.cliente is BEEstandar)
                    {
                        comboBox1.Enabled = false;
                        compra.garantia = null;
                    }
                    else
                    {
                        comboBox1.Enabled = true;
                        comboBox1_SelectedIndexChanged(sender, e);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }



        // quitar del carrito
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (carrito.Count > 0)
                {
                    BEProducto oProd = (BEProducto)dataGridView3.CurrentRow.DataBoundItem;
                    BEProducto p = carrito.Find(x => x.Codigo == oProd.Codigo);
                    if (p != null)
                    {
                        carrito.Remove(p);
                        actualizarGrillaCarrito();
                        actualizarPrecioCarrito();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }



        // finalizar compra
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (carrito.Count > 0)
                {
                    List<BEProducto> lista = obllProducto.ListarTodo();
                    foreach (BEProducto p in compra.productos)
                    {
                        BEProducto prod =  lista.Find(x=>x.Codigo == p.Codigo);
                        if (prod != null)
                        {
                            prod.Cantidad = prod.Cantidad - p.Cantidad;
                            obllProducto.Guardar(prod);
                        }
                    }
                    bCompra = new BLLCompra();
                    bCompra.Guardar(compra);
                    reiniciar();
                    actualizarGrillaCarrito();
                    actualizarPrecioCarrito();
                    actualizarGrillaClientes();
                    actualizarGrillaProductos();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                compra.garantia = new BEGarantia() { Codigo = comboBox1.SelectedIndex + 1 };
            }
            catch (Exception ex) { throw ex; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dataGridView1.SelectedRows.Count > 0))
                {
                    frChart fr = new frChart();
                    fr.Show();
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
