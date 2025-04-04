using camiones_antioquia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace camiones_antioquia.Clases
{
    public class clsPesaje
    {
        private camiones_antioquiaEntities dbcamiones_antioquia = new camiones_antioquiaEntities(); // atributo que gestiona conexion a la BD

        public Pesaje pesaje { get; set; } //propiedad para manipular la info en la BD

        public string InsertarPesaje()
        {
            try
            {
                
                dbcamiones_antioquia.Pesajes.Add(pesaje); //agregar el pesaje a la BD
                dbcamiones_antioquia.SaveChanges(); //guardar los cambios en la BD
                return "Pesaje registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar el pesaje: " + ex.Message;
            }
        }

        public string ActualizarPesaje()
        {
            try
            {
                Pesaje pesa = consultar(pesaje.id); //consultar el pesaje en la BD
                if (pesa != null) //verificar si el pesaje existe
                {
                    dbcamiones_antioquia.Pesajes.AddOrUpdate(pesaje);
                    dbcamiones_antioquia.SaveChanges(); //guardar los cambios en la BD
                    return "Pesaje actualizado correctamente";
                }
                else
                {
                    return "Pesaje no encontrado";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar el pesaje: " + ex.Message;
            }
                       

        }

        public Pesaje consultar(int id)
        {
            return dbcamiones_antioquia.Pesajes.FirstOrDefault(p => p.id == id); //consultar el pesaje en la BD
        }

        public string EliminarPesaje()
        {
            try
            {
                Pesaje pesa = consultar(pesaje.id); //consultar el pesaje en la BD
                if (pesa != null) //verificar si el pesaje existe
                {
                    dbcamiones_antioquia.Pesajes.Remove(pesa); //eliminar el pesaje de la BD
                    dbcamiones_antioquia.SaveChanges(); //guardar los cambios en la BD
                    return "Pesaje eliminado correctamente";

                }
                else
                {
                    return "Pesaje no encontrado";
                }
            }
            catch (Exception ex)
            {
                return "Error al eliminar el pesaje: " + ex.Message;
            }

                   
        }

        public string GrabarImagenPesaje(int id, List<string> imagenes)
        {
            try
            {
                foreach (string imagen in imagenes)
                {
                    FotoPesaje imagenProducto = new FotoPesaje();
                    imagenProducto.idPesaje = id;
                    imagenProducto.ImagenVehiculo = imagen;
                    dbcamiones_antioquia.FotoPesajes.Add(imagenProducto);
                    dbcamiones_antioquia.SaveChanges();
                }
                return "Se grabó la información en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error al grabar la imagen: " + ex.Message;
            }
        }


        public IQueryable ListarImagenes(string PlacaCamion)
        {
            return from P in dbcamiones_antioquia.Set<Pesaje>()
                   join C in dbcamiones_antioquia.Set<Camion>()
                   on P.PlacaCamion equals C.Placa
                   join F in dbcamiones_antioquia.Set<FotoPesaje>()
                   on P.id equals F.idPesaje
                   where P.PlacaCamion == PlacaCamion
                   orderby F.ImagenVehiculo
                   select new
                   {
                       PlacaCamion = P.PlacaCamion,
                       NumeroEjes = C.NumeroEjes,
                       Marca = C.Marca,
                       FechaPesaje = P.FechaPesaje,
                       Peso = P.Peso,
                       ImagenVehiculo=F.ImagenVehiculo
                   };
        }
    }
    
}