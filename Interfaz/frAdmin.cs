using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Interfaz
{
    public partial class frAdmin : Form
    {
        public frAdmin()
        {
            InitializeComponent();
        }

        
        private void frAdmin_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Nombres","Usuario");
            dataGridView1.Columns.Add("Contrasenas","Contraseña");
            dataGridView1.Rows.Clear();
            cargarXML();
        }

        private void cargarXML()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Usuarios.xml");
            var usuarios = xmlDocument.SelectNodes("Usuarios/Usuario");
            foreach (XmlNode usu in usuarios)
            {
                string nombre = usu.SelectSingleNode("Nombre").InnerText;
                string contrasenaEncriptada = usu.SelectSingleNode("Contrasena").InnerText;
                dataGridView1.Rows.Add(nombre,contrasenaEncriptada);
            }
        }
        private void grabarXML()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Usuarios.xml", System.Text.Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.QuoteChar = '\'';
                writer.WriteStartDocument(true);
                writer.WriteStartElement("Usuarios");
                foreach (DataGridViewRow Fila in dataGridView1.Rows)
                {
                    if(((Fila.Cells["Nombres"].Value != null) && (Fila.Cells["Contrasenas"].Value != null)))
                    {
                        writer.WriteStartElement("Usuario");
                        writer.WriteElementString("Nombre", Fila.Cells["Nombres"].Value.ToString());
                        writer.WriteElementString("Contrasena", Fila.Cells["Contrasenas"].Value.ToString());
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndDocument();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            grabarXML();
        }
    }
}
