using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace camiones_antioquia.Clases
{
	public class clsUpload
	{
        public string Datos { get; set; }
        public HttpRequestMessage request { get; set; }

        private List<string> Archivos;
        public async Task<HttpResponseMessage> GrabarArchivo()
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);
            
            try
            {
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0)
                {
                    Archivos = new List<string>();
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }

                        //Verifico si el archivo existe en la base de datos
                        if (File.Exists(Path.Combine(root, fileName)))
                        {
                            //Eliminar el temporal
                            File.Delete(Path.Combine(root, file.LocalFileName));
                            return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "El archivo ya existe");
                        }

                        //Lista de nombres
                        Archivos.Add(fileName);
                        //Renombra el archivo temporal
                        File.Move(file.LocalFileName, Path.Combine(root, fileName));
                    }

                    //Gestión en la base de datos
                    string RptaBD = ProcesarBD();
                    //Termina el ciclo, responde que se cargó el archivo correctamente
                    return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se cargaron los archivos en el servidor, " + RptaBD);
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private string ProcesarBD()
        {
            clsPesaje Pesaje = new clsPesaje();
            return Pesaje.GrabarImagenPesaje(Convert.ToInt32(Datos), Archivos);
                
        }
    }
}