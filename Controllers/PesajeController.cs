using camiones_antioquia.Clases;
using camiones_antioquia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace camiones_antioquia.Controllers
{
    [RoutePrefix("api/pesaje")]
    public class PesajeController : ApiController
    {
        [HttpGet]
        [Route("consultar")]
        public Pesaje Consultar(int id)
        {
            clsPesaje pesaje = new clsPesaje();
            return pesaje.consultar(id);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Pesaje pesaje)
        {
            clsPesaje Pesaje = new clsPesaje();
            Pesaje.pesaje = pesaje;
            return Pesaje.InsertarPesaje();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Pesaje pesaje)
        {
            clsPesaje Pesaje = new clsPesaje();
            Pesaje.pesaje = pesaje;
            return Pesaje.ActualizarPesaje();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id)
        {
            clsPesaje pesaje = new clsPesaje();
            pesaje.pesaje = pesaje.consultar(id);
            return pesaje.EliminarPesaje();
        }
    }
}