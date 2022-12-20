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
    public partial class Usuario : Form
    {
        
        public Usuario(int id)
        {
            InitializeComponent();

            llenarCombo();
            ComboTipoUsuario.SelectedIndex = 0;
            this.Id = id;
            LblID.Text = "ID: " + this.Id.ToString();
            if(this.Id != 0)
            {
                var RESTclient = new RestClient(Models.Util.Get_Server());
                RestRequest Request = new RestRequest("/api/Usuario/" + this.Id.ToString(), Method.GET);
                IRestResponse RestResponse = RESTclient.Execute(Request);
                UsuarioRespuesta Respuesta = JsonConvert.DeserializeObject<UsuarioRespuesta>(RestResponse.Content);
                if (Respuesta.Codigo != "200")
                {
                    MessageBox.Show(Respuesta.Mensaje);
                    Close();
                }
                else 
                {

                    Models.Usuario Usuario = Respuesta.Resultado[0];

                    TxtNombre.Text = Usuario.Nombre;
                    TxtMail.Text = Usuario.Email;
                    TxtTelefono.Text = Usuario.Telefono;
                    TxtContrasena.Text = Usuario.contrasena;
                    if (Usuario.TipoUsuario != null)
                    {
                        ComboTipoUsuario.SelectedIndex = Usuario.TipoUsuario.ID;
                    }
                    else
                    {
                        ComboTipoUsuario.SelectedIndex = 0;
                    }
                }
                
            }
           
        }

        private void llenarCombo()
        {
            var RESTclient = new RestClient(Models.Util.Get_Server());
            RestRequest Request = new RestRequest("/api/TipoUsuario", Method.GET);
            IRestResponse RestResponse = RESTclient.Execute(Request);
            TipoUsuarioRespuesta Respuesta = JsonConvert.DeserializeObject<TipoUsuarioRespuesta>(RestResponse.Content);
            if (Respuesta.Codigo != "200")
            {
                MessageBox.Show(Respuesta.Mensaje);
                Close();
            }
            List <Models.TipoUsuario> Tipos = Respuesta.Resultado;
            Tipos = Tipos.OrderBy(x => x.ID).ToList();
            ComboTipoUsuario.Items.Add("---SELECCIONAR---");
            foreach (var Tipo in Tipos)
            {
                ComboTipoUsuario.Items.Add(Tipo.Nombre);
            }
        }
        int Id { get; set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Models.Usuario Usuario = new Models.Usuario(this.Id, TxtNombre.Text, TxtMail.Text, TxtTelefono.Text, TxtContrasena.Text, new Models.TipoUsuario(ComboTipoUsuario.SelectedIndex, ComboTipoUsuario.Text));
            var RESTclient = new RestClient(Models.Util.Get_Server());
            RestRequest Request = null;
            if (this.Id == 0)
            {
                Request = new RestRequest("/api/Usuario", Method.POST);
            }
            else 
            {
                Request = new RestRequest("/api/Usuario/"+this.Id.ToString(), Method.PUT);
            }
            Request.AddHeader("Content-Type", "application/Json");
            Request.AddJsonBody(Usuario);
            IRestResponse RestResponse = RESTclient.Execute(Request);
            UsuarioRespuesta Respuesta = JsonConvert.DeserializeObject<UsuarioRespuesta>(RestResponse.Content);
            if (Respuesta.Codigo != "200")
            {
                MessageBox.Show(Respuesta.Mensaje);
                Close();
            }
            else 
            {
                Usuario = Respuesta.Resultado[0];
                this.Id = Usuario.ID;
                LblID.Text = "ID: " + this.Id.ToString();
                Close();
            }
                
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
