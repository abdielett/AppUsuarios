using Abp.UI;
using ApiUsuarios.Context;
using ApiUsuarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet]
        public UsuarioRespuesta Get()
        {
            UsuarioRespuesta Respuesta = new UsuarioRespuesta();
            List<UsuarioModelo> Usuarios = new List<UsuarioModelo>();
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    foreach (var Usuario in Context.Usuarios.ToList())
                    {
                        UsuarioModelo UsuarioSalida = new UsuarioModelo();
                        UsuarioSalida.ID = Usuario.Id;
                        UsuarioSalida.Nombre = Usuario.Nombre;
                        UsuarioSalida.Email = Usuario.Email;
                        UsuarioSalida.Telefono = Usuario.Telefono;
                        if (Usuario.TipoUsuario != null)
                        {
                            TipoUsuarioModelo TipoUsuarioSalida = new TipoUsuarioModelo();
                            TipoUsuario TipoUsuario = Context.TipoUsuarios.Find(Usuario.TipoUsuario);
                            TipoUsuarioSalida.ID = TipoUsuario.Id;
                            TipoUsuarioSalida.Nombre = TipoUsuario.Nombre;
                            UsuarioSalida.TipoUsuario = TipoUsuarioSalida;
                        }
                        Usuarios.Add(UsuarioSalida);
                    }
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = Usuarios;
                }
            }
            catch (Exception ex) 
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al traer el listado de Usuarios, revise Log para mas detalle";
                LogController.GenerarLog(ex.Message);
                Usuarios.Clear();
                Respuesta.Resultado = Usuarios;
            }
          
            return Respuesta;
            
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public UsuarioRespuesta Get(int id)
        {
            UsuarioRespuesta Respuesta = new UsuarioRespuesta();
            UsuarioModelo Usuario = new UsuarioModelo();

            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    Usuario UsuarioEncontrado = Context.Usuarios.Find(id);
                    if (UsuarioEncontrado != null)
                    {
                        Usuario.ID = UsuarioEncontrado.Id;
                        Usuario.Nombre = UsuarioEncontrado.Nombre;
                        Usuario.Email = UsuarioEncontrado.Email;
                        Usuario.Telefono = UsuarioEncontrado.Telefono;
                        Usuario.Contrasena = UsuarioEncontrado.Contrasena;
                        if (UsuarioEncontrado.TipoUsuario != null)
                        {
                            TipoUsuarioModelo TipoUsuarioSalida = new TipoUsuarioModelo();
                            TipoUsuario TipoUsuario = Context.TipoUsuarios.Find(UsuarioEncontrado.TipoUsuario);
                            TipoUsuarioSalida.ID = TipoUsuario.Id;
                            TipoUsuarioSalida.Nombre = TipoUsuario.Nombre;
                            Usuario.TipoUsuario = TipoUsuarioSalida;
                        }
                        Respuesta.Codigo = "200";
                        Respuesta.Mensaje = "Solicitud procesada exitosamente";
                        Respuesta.Resultado = new List<UsuarioModelo>();
                        Respuesta.Resultado.Add(Usuario);
                    }
                    else 
                    {
                        throw new UserFriendlyException("No se encontró el usuario con ID: " + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al traer el Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<UsuarioModelo>();
                LogController.GenerarLog(ex.Message);
            }
            return Respuesta;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public UsuarioRespuesta Post([FromBody] UsuarioModelo Value)
        {
            UsuarioRespuesta Respuesta = new UsuarioRespuesta();
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    Usuario NuevoUsuario = new Usuario();
                    NuevoUsuario.Nombre = Value.Nombre;
                    NuevoUsuario.Email = Value.Email;
                    NuevoUsuario.Telefono = Value.Telefono;
                    NuevoUsuario.Contrasena = Value.Contrasena;
                    if (Value.TipoUsuario != null)
                    {
                        NuevoUsuario.TipoUsuario = Value.TipoUsuario.ID;
                    }
                    Context.Add(NuevoUsuario);
                    Context.SaveChanges();
                    Value.ID = NuevoUsuario.Id;
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<UsuarioModelo>();
                    Respuesta.Resultado.Add(Value);
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al dar de alta el Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<UsuarioModelo>();
                LogController.GenerarLog(ex.Message);
            }
            
            return Respuesta;
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public UsuarioRespuesta Put(int id, [FromBody] UsuarioModelo Value)
        {
            UsuarioRespuesta Respuesta = new UsuarioRespuesta();
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    Usuario UsuarioModicar = Context.Usuarios.Find(id);
                    UsuarioModicar.Nombre = Value.Nombre;
                    UsuarioModicar.Email = Value.Email;
                    UsuarioModicar.Telefono = Value.Telefono;
                    UsuarioModicar.Contrasena = Value.Contrasena;
                    if (Value.TipoUsuario != null)
                    {
                        TipoUsuarioModelo TipoUsuarioSalida = new TipoUsuarioModelo();
                        TipoUsuario TipoUsuario = Context.TipoUsuarios.Find(Value.TipoUsuario.ID);
                        if (TipoUsuario != null)
                        {
                            UsuarioModicar.TipoUsuarioNavigation = TipoUsuario;
                        }
                        else 
                        {
                            LogController.GenerarLog("No fue encontrado el Tipo de Usuario: " + Value.TipoUsuario.ID + " - " + Value.TipoUsuario.Nombre);
                             
                        }
                       
                    }
                    Context.Entry(UsuarioModicar).State = EntityState.Modified;
                    Context.SaveChanges();
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<UsuarioModelo>();
                    Respuesta.Resultado.Add(Value);
                    
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al modificar el Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<UsuarioModelo>();
                LogController.GenerarLog(ex.Message);
            }

           
            return Respuesta;
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public UsuarioRespuesta Delete(int id)
        {
            UsuarioRespuesta Respuesta = new UsuarioRespuesta();
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    Usuario UsuarioEliminar = Context.Usuarios.Find(id);
                    Context.Remove(UsuarioEliminar);
                    Context.SaveChanges();
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<UsuarioModelo>();
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al dar de eliminar el Usuario, revise Log para mas detalle"; ;
                Respuesta.Resultado = new List<UsuarioModelo>();
                LogController.GenerarLog(ex.Message);
            }
            return Respuesta;
        }
    }
}