namespace Microsoft.Samples.Kinect.InteractionGallery.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data model for an article
    /// </summary>
    public class ArticleModel
    {

        public ArticleModel()
        {
            Title = string.Empty;
        }

        /// <summary>
        /// Title of the article
        /// </summary>
        public string Title { get; set; }

    }
}