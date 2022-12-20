using ApiUsuarios.Context;
using ApiUsuarios.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public LogRespuesta Get()
        {
            LogRespuesta Respuesta = new LogRespuesta();
            List<LogModelo> MensajesLog = new List<LogModelo>();

            try
            {
                using (AppUsuariosContext Context = new AppUsuariosContext())
                {
                    foreach (var Log in Context.Logs.ToList())
                    {
                        LogModelo MensajeLog = new LogModelo();
                        MensajeLog.ID = Log.Id;
                        MensajeLog.Mensaje = Log.Mensaje;
                        MensajeLog.Fecha = (DateTime)Log.Fecha;

                        MensajesLog.Add(MensajeLog);
                    }
                    Respuesta.Codigo = "200";
                    Respuesta.Mensaje = "Solicitud procesada exitosamente";
                    Respuesta.Resultado = MensajesLog;
                }
            }
            catch (Exception ex)
            {
                Respuesta.Codigo = "400";
                Respuesta.Mensaje = "Error al traer el mensajes de Log " + ex.Message;
                Respuesta.Resultado = new List<LogModelo>();
            }
            return Respuesta;
        }

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        public static void GenerarLog(string Mensaje)
        {
            using (AppUsuariosContext Context = new AppUsuariosContext())
            {
                Log NuevoLog = new Log();
                NuevoLog.Mensaje = Mensaje;
                NuevoLog.Fecha = DateTime.Now;
                Context.Add(NuevoLog);
                Context.SaveChanges();
            }
        }
    }
}
