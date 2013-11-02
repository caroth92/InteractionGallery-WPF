// -----------------------------------------------------------------------
// <copyright file="ArticleViewModel.cs" company="Microsoft">
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
            this.Names = new ObservableCollection<string>();
            this.Images = new ObservableCollection<ImageSource>();
        }

        /// <summary>
        /// Gets the title of the article. Changes to this property 
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
        /// Gets the collection of paragraphs composing the content of the article.
        /// Changes to this property cause the PropertyChanged event to be signaled.
        /// </summary>
        public ObservableCollection<string> Names { get; private set; }

        /// <summary>
        /// Gets the collection of images associated with the article. 
        /// Changes to the paragraphs cause the CollectionChanged event to be signaled.
        /// </summary>
        public ObservableCollection<ImageSource> Images { get; private set; }

    }
}