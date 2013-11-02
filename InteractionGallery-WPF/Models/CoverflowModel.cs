using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Kinect.InteractionGallery.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data model for a coverflow
    /// </summary>
    class CoverflowModel
    {
        private ICollection<Uri> imageUris;
        private ICollection<string> names;

        public CoverflowModel()
        {
            Title = string.Empty;
            imageUris = new List<Uri>();
            names = new List<string>();

        }

        /// <summary>
        /// Title of the article
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Collection of image Uris associated with the article
        /// </summary>
        public ICollection<Uri> ImageUris
        {
            get { return this.imageUris; }
        }

        /// <summary>
        /// Collection of paragraphs composing the article content
        /// </summary>
        public ICollection<string> Names
        {
            get { return this.names; }
        }
    }
}
