// -----------------------------------------------------------------------
// <copyright file="CoverflowViewModel.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.InteractionGallery.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Xaml;
    using Microsoft.Samples.Kinect.InteractionGallery.Models;
    using Microsoft.Samples.Kinect.InteractionGallery.Navigation;
    using Microsoft.Samples.Kinect.InteractionGallery.Properties;

    [ExportNavigable(NavigableContextName = DefaultNavigableContexts.CoverflowScreen)]
    public class CoverflowViewModel : ViewModelBase
    {
        private string title = string.Empty;

        public CoverflowViewModel()
            : base()
        {

        }

        /// <summary>
        /// Gets the title of the coverflow. Changes to this property 
        /// cause the PropertyChanged event to be signaled
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            protected set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Loads an article from the supplied Uri
        /// </summary>
        /// <param name="parameter">Uri pointing to an ArticleModel</param>
        public override void Initialize(Uri parameter)
        {
            if (null == parameter)
            {
                throw new ArgumentNullException("parameter");
            }

            using (Stream coverflowStream = Application.GetResourceStream(parameter).Stream)
            { 
                if (null == coverflowStream)
                {
                    throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, Resources.InvalidArticle , parameter.AbsolutePath));
                }

                var coverflow = XamlServices.Load(coverflowStream) as CoverflowModel;
                if (null == coverflow)
                {
                    throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, Resources.InvalidArticle, parameter.AbsolutePath));
                }

                this.Title = coverflow.Title;

            }
        }

    }
}