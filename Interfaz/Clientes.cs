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
using BE;
using BLL;

namespace Interfaz
{
    public partial class Clientes : Form
    {

        BLLCliente obllCliente;
        List<BECliente> clientes;

        public Clientes()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Clientes_Load(object sender, EventArgs e)
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
                obllCliente = new BLLCliente();
                dataGridView1.DataSource = null;
                clientes = obllCliente.listarTodo();
                dataGridView1.DataSource = clientes;
            }
            catch (Exception ex) { throw ex; }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    BECliente cliente = clientes.Find(c => c.DNI == dataGridView1.SelectedCells[0].Value.ToString());
                    if (cliente is BEEstandar)
                    {
                        radioButton1.Checked = true;
                    }
                    else if (cliente is BEPremium) { radioButton2.Checked = true; }

                    uC_dni1.textBox1.Text = cliente.DNI.ToString();
                    textBox2.Text = cliente.Nombre.ToString();
                    textBox3.Text = cliente.Apellido.ToString();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (!uC_dni1.validar()) { throw new Exception("Ingrese un DNI valido"); }
                    BECliente cliente = clientes.Find(c => c.DNI == uC_dni1.textBox1.Text);
                    if(cliente != null)
                    {
                        throw new Exception("Ya existe un cliente registrado con ese DNI ");
                    }
                    else
                    {
                        if(radioButton1.Checked)
                        {
                            BEEstandar std = new BEEstandar();
                            std.DNI = uC_dni1.textBox1.Text;
                            std.Nombre = textBox2.Text;
                            std.Apellido = textBox3.Text;
                            obllCliente.guardar(std);
                        }
                        else if (radioButton2.Checked)
                        {
                            BEPremium prem = new BEPremium();
                            prem.DNI = uC_dni1.textBox1.Text;
                            prem.Nombre = textBox2.Text;
                            prem.Apellido = textBox3.Text;
                            obllCliente.guardar(prem);
                        }

                        actualizarGrilla();
                    }
                }    
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (!uC_dni1.validar()) { throw new Exception("Ingrese un DNI valido"); }
                    BECliente cliente = clientes.Find(c => c.DNI == uC_dni1.textBox1.Text);
                    if (cliente != null)
                    {
                        if (radioButton1.Checked)
                        {
                            BEEstandar std = new BEEstandar();
                            std.DNI = uC_dni1.textBox1.Text;
                            std.Nombre = textBox2.Text;
                            std.Apellido = textBox3.Text;
                            obllCliente.guardar(std);
                        }
                        else if (radioButton2.Checked)
                        {
                            BEPremium prem = new BEPremium();
                            prem.DNI = uC_dni1.textBox1.Text;
                            prem.Nombre = textBox2.Text;
                            prem.Apellido = textBox3.Text;
                            obllCliente.guardar(prem);
                        }
                        actualizarGrilla();
                    }
                    else
                    {
                        throw new Exception("No se puede modificar el DNI de una persona. No se ha encontrado el DNI en la lista de clientes. Inténtelo nuevamente.");    
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    BECliente cliente = clientes.Find(c => c.DNI == uC_dni1.textBox1.Text);
                    if (cliente != null)
                    {
                        if (obllCliente.eliminar(cliente) == false) { throw new Exception("Este cliente tiene asociadas una o mas compras. No se puede eliminar."); }
                        actualizarGrilla();
                    }
                    else { throw new Exception("El DNI del cliente no se ha encontrado para eliminar el cliente. Verifique e intente nuevamente. "); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
