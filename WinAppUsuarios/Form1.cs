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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            this.DataGridUsuarios.Columns.Add("id", "ID");
            this.DataGridUsuarios.Columns.Add("nombre", "Nombre");
            this.DataGridUsuarios.Columns.Add("email", "Email");
            this.DataGridUsuarios.Columns.Add("telefono", "Telefono");
            this.DataGridUsuarios.Columns.Add("tipo", "Tipo");

            DataGridViewButtonColumn Edt = new DataGridViewButtonColumn();
            DataGridUsuarios.Columns.Add(Edt);
            Edt.HeaderText = "Editar";
            Edt.Text = "Edt";
            Edt.Name = "edt";
            Edt.FlatStyle = FlatStyle.Flat;
            Edt.DefaultCellStyle.Font = new Font("Arial Rounded MT", 12);
            Edt.DefaultCellStyle.BackColor = Color.Orange;
            Edt.DefaultCellStyle.SelectionBackColor = Color.Orange;
            Edt.DefaultCellStyle.SelectionForeColor = Color.Black;
            Edt.ToolTipText = "Edit this Item";
            Edt.UseColumnTextForButtonValue = true;
            DataGridUsuarios.Columns["edt"].DefaultCellStyle.SelectionBackColor = Color.Orange;
            DataGridUsuarios.Columns["edt"].DefaultCellStyle.SelectionForeColor = Color.Black;

            DataGridViewButtonColumn Del = new DataGridViewButtonColumn();
            DataGridUsuarios.Columns.Add(Del);
            Del.HeaderText = "Eliminar";
            Del.Text = "x";
            Del.Name = "del";
            Del.FlatStyle = FlatStyle.Flat;
            Del.DefaultCellStyle.Font = new Font("Arial Rounded MT", 12);
            Del.DefaultCellStyle.BackColor = Color.Red;
            Del.DefaultCellStyle.SelectionBackColor = Color.Red;
            Del.DefaultCellStyle.SelectionForeColor = Color.Black;
            Del.ToolTipText = "Delete this Item";
            Del.UseColumnTextForButtonValue = true;
            DataGridUsuarios.Columns["del"].DefaultCellStyle.SelectionBackColor = Color.Red;
            DataGridUsuarios.Columns["del"].DefaultCellStyle.SelectionForeColor = Color.Black;
            cargarUsuarios();
        }

        private void cargarUsuarios()
        {
            DataGridUsuarios.Rows.Clear();
            var RESTclient = new RestClient(Models.Util.Get_Server());
            RestRequest Request = new RestRequest("/api/Usuario", Method.GET);
            IRestResponse RestResponse = RESTclient.Execute(Request);
            UsuarioRespuesta Respuesta = JsonConvert.DeserializeObject<UsuarioRespuesta>(RestResponse.Content);
            if (Respuesta.Codigo != "200")
            {
                MessageBox.Show(Respuesta.Mensaje);
            }
            else 
            {
                List<Models.Usuario> Usuarios = Respuesta.Resultado;

                foreach (var Usuario in Usuarios)
                {
                    if (Usuario.TipoUsuario != null)
                    {
                        DataGridUsuarios.Rows.Add(Usuario.ID, Usuario.Nombre, Usuario.Email, Usuario.Telefono, Usuario.TipoUsuario.Nombre);
                    }
                    else
                    {
                        DataGridUsuarios.Rows.Add(Usuario.ID, Usuario.Nombre, Usuario.Email, Usuario.Telefono, "");
                    }
                }
            }
        }
        private void dataGridUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataGridUsuarios.Columns["del"].Index && e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row2 in DataGridUsuarios.SelectedRows)
                {
                   if(MessageBox.Show("¿Seguro que desea borrar el registro?", "Comfirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var RESTclient = new RestClient(Models.Util.Get_Server());
                        RestRequest Request = new RestRequest("/api/Usuario/"+row2.Cells[0].Value.ToString(), Method.DELETE);
                        IRestResponse RestResponse = RESTclient.Execute(Request);
                        UsuarioRespuesta Respuesta = JsonConvert.DeserializeObject<UsuarioRespuesta>(RestResponse.Content);
                        if (Respuesta.Codigo != "200")
                        {
                            MessageBox.Show(Respuesta.Mensaje);
                        }
                        cargarUsuarios();
                    }
                   
                   
                }
            }
            if (e.ColumnIndex == DataGridUsuarios.Columns["edt"].Index && e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row2 in DataGridUsuarios.SelectedRows)
                {

                    Usuario usuario = new Usuario(Convert.ToInt32(row2.Cells[0].Value.ToString()));
                    usuario.ShowDialog();
                    cargarUsuarios();
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario(0);
            usuario.ShowDialog();
            cargarUsuarios();
        }
    }
}
