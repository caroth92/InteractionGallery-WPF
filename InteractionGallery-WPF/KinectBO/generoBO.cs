using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class generoBO
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static generoBO _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  generoBO
        /// </summary>
        /// <returns>Regresa el objeto generoBO</returns>
        public static generoBO Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new generoBO();


                    }
                }
                return _instancia;
            }
        }
        #endregion

        public List<genero> daListaGeneros()
        {
            List<genero> generos;
            using (var bd = new kinectEntities())
            {
                generos = (from g
                           in bd.genero
                           select g).ToList();
            }
            return generos;
        }

        public genero obtenerGenero(int idGenero)
        {
            genero genero;
            using (var bd = new kinectEntities())
            {
                genero = (from g
                          in bd.genero
                          where g.idGenero == idGenero
                          select g).FirstOrDefault();
            }
            return genero;
        }
    }

}