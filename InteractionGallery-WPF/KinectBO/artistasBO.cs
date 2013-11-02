using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class artistasBO
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static artistasBO _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  artistasBO
        /// </summary>
        /// <returns>Regresa el objeto artistasBO</returns>
        public static artistasBO Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new artistasBO();


                    }
                }
                return _instancia;
            }
        }

        #endregion

        public List<artista> daListaArtistas()
        {
            List<artista> artistas;
            using (var bd = new kinectEntities())
            {
                
                artistas = (from a
                            in bd.artista
                            select a).ToList();
            }
            return artistas;
        }

        public artista obtenerArtista(int idArtista)
        {
            artista artista;
            using (var bd = new kinectEntities())
            {
                artista = (from a
                           in bd.artista
                           where a.idArtista == idArtista
                           select a).FirstOrDefault();
            }
            return artista;
        }

        
    }

}
