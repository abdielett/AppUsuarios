using Abp.UI;
using ApiUsuarios.Context;
using ApiUsuarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        // GET: api/<TipoUsuarioController>
        [HttpGet]
        public TipoUsuarioRespuesta Get()
        {
            TipoUsuarioRespuesta Respuesta = new TipoUsuarioRespuesta();
            List<TipoUsuarioModelo> TiposUsuarios = new List<TipoUsuarioModelo>();

            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    foreach (var TipoUsuario in Context.TipoUsuarios.ToList())
                    {
                        TipoUsuarioModelo TipoUsuarioSalida = new TipoUsuarioModelo();
                        TipoUsuarioSalida.ID = TipoUsuario.Id;
                        TipoUsuarioSalida.Nombre = TipoUsuario.Nombre;

                        TiposUsuarios.Add(TipoUsuarioSalida);
                    }
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = TiposUsuarios;
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al traer el listado Tipos de  Usuarios, revise Log para mas detalle";
                Respuesta.Resultado = new List<TipoUsuarioModelo>();
            }
            return Respuesta;
        }

        // GET api/<TipoUsuarioController>/5
        [HttpGet("{id}")]
        public TipoUsuarioRespuesta Get(int id)
        {
            TipoUsuarioRespuesta Respuesta = new TipoUsuarioRespuesta();
            TipoUsuarioModelo Tipo = new TipoUsuarioModelo();

            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    TipoUsuario TipUsuarioEncontrado = Context.TipoUsuarios.Find(id);
                    Tipo.ID = TipUsuarioEncontrado.Id;
                    Tipo.Nombre = TipUsuarioEncontrado.Nombre;
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<TipoUsuarioModelo>();
                    Respuesta.Resultado.Add(Tipo);
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al traer el Tipo de Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<TipoUsuarioModelo>();
            }
            return Respuesta;
        }

        // POST api/<TipoUsuarioController>
        [HttpPost]
        public TipoUsuarioRespuesta Post([FromBody] TipoUsuarioModelo Value)
        {
            TipoUsuarioRespuesta Respuesta = new TipoUsuarioRespuesta();
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    TipoUsuario NuevoTipoUsuario = new TipoUsuario();
                    NuevoTipoUsuario.Nombre = Value.Nombre;
                    Context.Add(NuevoTipoUsuario);
                    Context.SaveChanges();
                    Value.ID = NuevoTipoUsuario.Id;
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<TipoUsuarioModelo>();
                    Respuesta.Resultado.Add(Value);
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al dar de alta el Tipo de Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<TipoUsuarioModelo>();
            }
            return Respuesta;
        }

         // PUT api/<TipoUsuarioController>/5
         [HttpPut("{id}")]
        public TipoUsuarioRespuesta Put(int id, [FromBody] TipoUsuarioModelo Value)
        {
            TipoUsuarioRespuesta Respuesta = new TipoUsuarioRespuesta();
            
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    TipoUsuario TipoUsuarioModicar = Context.TipoUsuarios.Find(id);
                    TipoUsuarioModicar.Nombre = Value.Nombre;
                    Context.Entry(TipoUsuarioModicar).State = EntityState.Modified;
                    Context.SaveChanges();
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<TipoUsuarioModelo>();
                    Respuesta.Resultado.Add(Value);
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al modificar el Tipo de Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<TipoUsuarioModelo>();
            }

            return Respuesta;
        }

        // DELETE api/<TipoUsuarioController>/5
        [HttpDelete("{id}")]
        public TipoUsuarioRespuesta Delete(int id)
        {
            TipoUsuarioRespuesta Respuesta = new TipoUsuarioRespuesta();
            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    TipoUsuario TipoUsuarioEliminar = Context.TipoUsuarios.Find(id);
                    Context.Remove(TipoUsuarioEliminar);
                    Context.SaveChanges();
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = new List<TipoUsuarioModelo>();
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al eliminar el Tipo de Usuario, revise Log para mas detalle";
                Respuesta.Resultado = new List<TipoUsuarioModelo>();
            }
            
            return Respuesta;
        }
    }
}
