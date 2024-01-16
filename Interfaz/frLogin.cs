using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguridad;
using BLL;
using BE;

namespace Interfaz
{
    public partial class frLogin : Form
    {
        public frLogin()
        {
            InitializeComponent();
        }

        

        private void frLogin_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BEUsuario usuario = new BEUsuario();
            usuario.Nombre = textBox1.Text;
            usuario.Contrasena = textBox2.Text;
            CSeguridad seg = new CSeguridad();
            if (seg.verificar(usuario))
            {
                MessageBox.Show("Inicio de sesión exitoso.");
                if(usuario.Nombre == "admin")
                {
                    frAdmin frAdmin = new frAdmin();
                    frAdmin.Show();
                }
                else
                {
                    Menu menu = new Menu();
                    menu.Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Inicio de sesión fallido. El nombre o contraseña es incorrecto.");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El usuario es 'alumno' y la contraseña es 'lug2023'\n Las credenciales del admin son 'admin' y 'admin23' ");
        }
    }
}
