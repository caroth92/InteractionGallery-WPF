using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class Albums
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static Albums _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  artistasBO
        /// </summary>
        /// <returns>Regresa el objeto artistasBO</returns>
        public static Albums Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new Albums();
                    }
                }
                return _instancia;
            }
        }

        #endregion

        public List<album> albumsList(int artistID)
        {
            List<album> albums;
            using (var bd = new kinectEntities())
            {

                albums = (from a
                            in bd.album
                            where a.idArtista == artistID
                          select a).ToList();
            }
            return albums;
        }
        public List<album> albumsListByGenre(int generoID)
        {
            List<album> albums;
            using (var bd = new kinectEntities())
            {

                albums = (from a
                            in bd.album
                          where a.idGenero == generoID
                          select a).ToList();
            }
            return albums;
        }
    }
}
