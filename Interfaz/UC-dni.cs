using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Interfaz
{
    public partial class UC_dni : UserControl
    {
        public UC_dni()
        {
            InitializeComponent();
        }

        public bool validar()
        {
            return Regex.IsMatch(textBox1.Text,@"^[0-9]{8}$");
        }
    }
}
