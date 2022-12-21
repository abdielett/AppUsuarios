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
            this.DataGridUsuarios.Columns.Add("Id", "ID");
            this.DataGridUsuarios.Columns.Add("Nombre", "Nombre");
            this.DataGridUsuarios.Columns.Add("Email", "Email");
            this.DataGridUsuarios.Columns.Add("Telefono", "Telefono");
            this.DataGridUsuarios.Columns.Add("Tipo", "Tipo");

            DataGridViewButtonColumn Edt = new DataGridViewButtonColumn();
            DataGridUsuarios.Columns.Add(Edt);
            Edt.HeaderText = "Editar";
            Edt.Text = "Edt";
            Edt.Name = "Edt";
            Edt.FlatStyle = FlatStyle.Flat;
            Edt.DefaultCellStyle.Font = new Font("Arial Rounded MT", 12);
            Edt.DefaultCellStyle.BackColor = Color.Orange;
            Edt.DefaultCellStyle.SelectionBackColor = Color.Orange;
            Edt.DefaultCellStyle.SelectionForeColor = Color.Black;
            Edt.ToolTipText = "Edit this Item";
            Edt.UseColumnTextForButtonValue = true;
            DataGridUsuarios.Columns["Edt"].DefaultCellStyle.SelectionBackColor = Color.Orange;
            DataGridUsuarios.Columns["Edt"].DefaultCellStyle.SelectionForeColor = Color.Black;

            DataGridViewButtonColumn Del = new DataGridViewButtonColumn();
            DataGridUsuarios.Columns.Add(Del);
            Del.HeaderText = "Eliminar";
            Del.Text = "x";
            Del.Name = "Del";
            Del.FlatStyle = FlatStyle.Flat;
            Del.DefaultCellStyle.Font = new Font("Arial Rounded MT", 12);
            Del.DefaultCellStyle.BackColor = Color.Red;
            Del.DefaultCellStyle.SelectionBackColor = Color.Red;
            Del.DefaultCellStyle.SelectionForeColor = Color.Black;
            Del.ToolTipText = "Delete this Item";
            Del.UseColumnTextForButtonValue = true;
            DataGridUsuarios.Columns["Del"].DefaultCellStyle.SelectionBackColor = Color.Red;
            DataGridUsuarios.Columns["Del"].DefaultCellStyle.SelectionForeColor = Color.Black;
            CargarUsuarios();
        }

        private void CargarUsuarios()
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
            if (e.ColumnIndex == DataGridUsuarios.Columns["Del"].Index && e.RowIndex >= 0)
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
                        CargarUsuarios();
                    }
                   
                   
                }
            }
            if (e.ColumnIndex == DataGridUsuarios.Columns["Edt"].Index && e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row2 in DataGridUsuarios.SelectedRows)
                {

                    Usuario usuario = new Usuario(Convert.ToInt32(row2.Cells[0].Value.ToString()));
                    usuario.ShowDialog();
                    CargarUsuarios();
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario Usuario = new Usuario(0);
            Usuario.ShowDialog();
            CargarUsuarios();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

       

        private void LogToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Log Logs = new Log();
            Logs.ShowDialog();

        }
    }
}
