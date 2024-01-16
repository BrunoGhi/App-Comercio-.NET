using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BE;
using BLL;

namespace Interfaz
{
    public partial class frChart : Form
    {
        public frChart()
        {
            InitializeComponent();
        }

        private void frChart_Load(object sender, EventArgs e)
        {
            BLLCliente bLLCliente = new BLLCliente();
            var clientes = bLLCliente.listarTodo();
            if(clientes.Count > 0 )
            {
                Dictionary<string,float> datos = new Dictionary<string,float>();
                foreach (var cliente in clientes)
                {
                    float total = 0;
                    foreach (BECompra compra in bLLCliente.listaCompras(cliente))
                    {
                        total += compra.Monto;
                    }
                    datos.Add($"{cliente.Nombre} {cliente.Apellido}", total);
                }

                chart1.Titles.Clear();
                chart1.ChartAreas.Clear();
                chart1.Series.Clear();
                Title Titulo = new Title("Dinero total gastado por cliente");
                Titulo.Font = new Font("Tahoma", 20, FontStyle.Bold);
                chart1.Titles.Add(Titulo);
                ChartArea Area = new ChartArea();
                //Area.Area3DStyle.Enable3D = true;
                chart1.ChartAreas.Add(Area);

                Series series = new Series("Total gastado $");
                series.ChartType = SeriesChartType.Bar;
                series.Points.DataBindXY(datos.Keys,datos.Values);
                chart1.Series.Add(series);
            }
            
        }
    }
}
