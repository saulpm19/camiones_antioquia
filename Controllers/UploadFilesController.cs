﻿using camiones_antioquia.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace camiones_antioquia.Controllers
{
    [RoutePrefix("api/UploadFiles")]
    public class UploadFilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> CargarArchivo(HttpRequestMessage request, string Datos)
        {
            clsUpload upload = new clsUpload();
            upload.Datos = Datos;
            upload.request = request;
            return await upload.GrabarArchivo(false);
        }

        [HttpGet]
        public HttpResponseMessage ConsultarArchivo(string NombreImagen)
        {
            clsUpload upload = new clsUpload();
            return upload.DescargarArchivo(NombreImagen);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage request)
        {
            clsUpload upload = new clsUpload();
            upload.request = request;
            return await upload.GrabarArchivo(true);
        }

    }
}