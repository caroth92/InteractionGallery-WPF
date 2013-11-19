using Microsoft.Samples.Kinect.InteractionGallery.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    class Songs
    {
        #region ==== Singleton =====
        private static readonly object _mutex = new object();
        private static Songs _instancia;

        /// <summary>
        /// Obtiene la instancia de la clase  CancionesBO
        /// </summary>
        /// <returns>Regresa el objeto CancionesBO</returns>
        public static Songs Instance
        {
            get
            {
                lock (_mutex)
                {
                    if (_instancia == null)
                    {
                        _instancia = new Songs();


                    }
                }
                return _instancia;
            }
        }

        #endregion


        public List<AlbumCover> songsList(int albumID)
        {
            List<AlbumCover> songs;
            using (var bd = new kinectEntities())
            {
                songs = (from c
                         in bd.canciones
                         join x in bd.album on c.idAlbum equals x.idAlbum
                         where c.idAlbum == albumID
                         select new AlbumCover()
                         {
                             idAlbum=c.idAlbum,
                             cover=x.cover,
                             nombre=c.nombre,
                         }).ToList();
            }
            return songs;
        }

        public List<canciones> videoForSong(string title)
        {
            List<canciones> song;
            using (var bd = new kinectEntities())
            {
                song = (from c
                             in bd.canciones
                        where c.nombre == title
                        select c).ToList();
            }
            return song;
        }
    }
}
