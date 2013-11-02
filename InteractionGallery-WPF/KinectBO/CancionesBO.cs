using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class CancionesBO
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static CancionesBO _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  CancionesBO
        /// </summary>
        /// <returns>Regresa el objeto CancionesBO</returns>
        public static CancionesBO Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new CancionesBO();


                    }
                }
                return _instancia;
            }
        }

        #endregion

        public List<canciones> daListaCanciones()
        {
            List<canciones> canciones;
            using (var bd = new kinectEntities())
            {
                canciones = (from c
                             in bd.canciones
                             select c).ToList();
            }
            return canciones;
        }

        public canciones obtenerCancion(int idCancion)
        {
            canciones cancion;
            using (var bd = new kinectEntities())
            {
                cancion = (from c
                           in bd.canciones
                           where c.idCancion == idCancion
                           select c).FirstOrDefault();
            }
            return cancion;
        }

        public int darDeAltaCancion(canciones cancion)
        {
            using (var bd = new kinectEntities())
            {
                bd.canciones.Add(cancion);
                bd.SaveChanges();
                return cancion.idCancion != 0 ? cancion.idCancion : -1;

            }
        }

       
    }
}
