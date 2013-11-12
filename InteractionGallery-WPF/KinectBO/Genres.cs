using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class Genres
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static Genres _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  artistasBO
        /// </summary>
        /// <returns>Regresa el objeto artistasBO</returns>
        public static Genres Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new Genres();
                    }
                }
                return _instancia;
            }
        }

        #endregion

        public List<genero> genreList()
        {
            List<genero> genres;
            using (var bd = new kinectEntities())
            {

                genres = (from g
                            in bd.genero
                           select g).ToList();
            }
            return genres;
        }

        public genero getGenre(int genreID)
        {
            genero genre;
            using (var bd = new kinectEntities())
            {
                genre = (from g
                           in bd.genero
                           where g.idGenero == genreID
                           select g).FirstOrDefault();
            }
            return genre;
        }
    }
}
