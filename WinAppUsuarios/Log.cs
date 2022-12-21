using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppUsuarios.Models;

namespace WinAppUsuarios
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
            this.DataGridLog.Columns.Add("Fecha", "Fecha");
            this.DataGridLog.Columns.Add("Mensaje", "Mensaje");
            CargarLog();
        }
        private void CargarLog()
        {
            DataGridLog.Rows.Clear();
            var RESTclient = new RestClient(Models.Util.Get_Server());
            RestRequest Request = new RestRequest("/api/Log", Method.GET);
            IRestResponse RestResponse = RESTclient.Execute(Request);
            LogRespuesta Respuesta = JsonConvert.DeserializeObject<LogRespuesta>(RestResponse.Content);
            if (Respuesta.Codigo != "200")
            {
                MessageBox.Show(Respuesta.Mensaje);
            }
            else
            {
                List<Models.Log> Logs = Respuesta.Resultado;

                foreach (var Log in Logs.OrderByDescending(x => x.Fecha).ToList())
                {
                    DataGridLog.Rows.Add(Log.Fecha.ToString(), Log.Mensaje);
                }
            }
        }
        private void DataGridLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
