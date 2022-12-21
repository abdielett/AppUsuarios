
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
    public partial class TipoUsuario : Form
    {
        public int Id { get; set; }
        private Models.TipoUsuario TipoUsuarioGenerado { get; set; }
        public Models.TipoUsuario Get_TipoUsuarioGenerado 
        { 
            get 
            {
                return TipoUsuarioGenerado; 
            } 
        }
        public TipoUsuario()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Models.TipoUsuario TipoUsuario = new Models.TipoUsuario(0, TxtNombre.Text);
            var RESTclient = new RestClient(Models.Util.Get_Server());
            RestRequest Request = null;
            Request = new RestRequest("/api/TipoUsuario", Method.POST);
           
            Request.AddHeader("Content-Type", "application/Json");
            Request.AddJsonBody(TipoUsuario);
            IRestResponse RestResponse = RESTclient.Execute(Request);
            TipoUsuarioRespuesta Respuesta = JsonConvert.DeserializeObject<TipoUsuarioRespuesta>(RestResponse.Content);
            if (Respuesta.Codigo != "200")
            {
                MessageBox.Show(Respuesta.Mensaje);
                Close();
            }
            else
            {
                TipoUsuario = Respuesta.Resultado[0];
                this.Id = TipoUsuario.ID;
                this.TipoUsuarioGenerado = TipoUsuario;
                Close();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
