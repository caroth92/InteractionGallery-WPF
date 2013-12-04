using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.InteractionGallery.KinectBO
{
    public class listaRatingEntity
    {
        public string nombre { get; set; }
        public string claveAudio { get; set; }
        public int idCancion { get; set; }
        public int? likes { get; set; }
        public string cover { get; set; }
    }
}
