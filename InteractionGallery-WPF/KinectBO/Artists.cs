using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class Artists
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static Artists _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  artistasBO
        /// </summary>
        /// <returns>Regresa el objeto artistasBO</returns>
        public static Artists Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new Artists();
                    }
                }
                return _instancia;
            }
        }

        #endregion

        public List<artista> artistList()
        {
            List<artista> artists;
            using (var bd = new kinectEntities())
            {

                artists = (from a
                            in bd.artista
                            select a).ToList();
            }
            return artists;
        }

        public artista getArtist(int artistID)
        {
            artista artist;
            using (var bd = new kinectEntities())
            {
                artist = (from a
                           in bd.artista
                           where a.idArtista == artistID
                           select a).FirstOrDefault();
            }
            return artist;
        }
    }
}
