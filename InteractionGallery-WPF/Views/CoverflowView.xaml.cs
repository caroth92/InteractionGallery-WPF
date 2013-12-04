namespace Microsoft.Samples.Kinect.InteractionGallery.Views
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Toolkit;
    using Microsoft.Kinect.Toolkit.Controls;
    using System.Collections.Generic;
    using Microsoft.Samples.Kinect.InteractionGallery.KinectBO;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Samples.Kinect.InteractionGallery.NewFolder1;
    using Microsoft.Samples.Kinect.InteractionGallery.ViewModels;
    using System.IO;

    /// <summary>
    /// Interaction logic for CoverflowView
    /// </summary>
    public partial class CoverflowView
    {
        /// <summary>
        /// Name of the non-transitioning visual state.
        /// </summary>
        internal const string NormalState = "Normal";

        /// <summary>
        /// Name of the fade in transition.
        /// </summary>
        internal const string FadeInTransitionState = "FadeIn";

        /// <summary>
        /// Name of the fade out transition.
        /// </summary>
        internal const string FadeOutTransitionState = "FadeOut";
        
        public static readonly DependencyProperty PageLeftEnabledProperty = DependencyProperty.Register(
            "PageLeftEnabled", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        public static readonly DependencyProperty PageRightEnabledProperty = DependencyProperty.Register(
            "PageRightEnabled", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        private const double ScrollErrorMargin = 0.001;

        private const int PixelScrollByAmount = 20;

        CoverflowViewModel coverfl = new CoverflowViewModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        /// </summary>
        public CoverflowView()
        {
            this.InitializeComponent();
            
            // Bind listner to scrollviwer scroll position change, and check scroll viewer position
            this.UpdatePagingButtonState();
            scrollViewer.ScrollChanged += (o, e) => this.UpdatePagingButtonState();

            var x = this.txtTitle.Text;
        }

        /// <summary>
        /// CLR Property Wrappers for PageLeftEnabledProperty
        /// </summary>
        public bool PageLeftEnabled
        {
            get
            {
                return (bool)GetValue(PageLeftEnabledProperty);
            }

            set
            {
                this.SetValue(PageLeftEnabledProperty, value);
            }
        }

        /// <summary>
        /// CLR Property Wrappers for PageRightEnabledProperty
        /// </summary>
        public bool PageRightEnabled
        {
            get
            {
                return (bool)GetValue(PageRightEnabledProperty);
            }

            set
            {
                this.SetValue(PageRightEnabledProperty, value);
            }
        }

        /// <summary>
        /// Called when the KinectSensorChooser gets a new sensor
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="args">event arguments</param>
        private static void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    }
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
        }


        /// <summary>
        /// Handle a button click from the wrap panel.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void KinectTileButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (KinectTileButton)e.OriginalSource;
            
            //var selectionDisplay = new SelectionDisplay(button.Label as string);
            //this.kinectRegionGrid.Children.Add(selectionDisplay);

            var filtro = button.Procedence;

            if(filtro.Equals("Artistas"))
            {
                var artistID = button.Key;
                var albums = Albums.Instance.albumsList(Convert.ToInt16(artistID));
                fillAlbumInfo("Album", albums);
            }
            else if (filtro.Equals("Album"))
            {
                var albumID = button.Key;
                var songs = Songs.Instance.songsList(Convert.ToInt16(albumID));
                fillSongInfo("Canciones", songs);
            }
            else if (filtro.Equals("Géneros"))
            {
                var genreID = button.Key;
                var albums = Albums.Instance.albumsListByGenre(Convert.ToInt16(genreID));
                fillAlbumInfo("Album", albums);
            }
            else if (filtro.Equals("Canciones") || filtro.Equals("Éxitos"))
            {
                var songTitle = button.Label as string;
                var video = Songs.Instance.videoForSong(songTitle);
                var source = video[0].claveAudio;
                var songID = button.Key as string;
                Songs.Instance.addRating(Convert.ToInt16(songID));
                OnDisplayFullImage(source);   
            }

            e.Handled = true;
        }

        private void fillArtistInfo(String title, List<artista> lista )
        {
            this.txtTitle.Text = title;
            
            // Clear out placeholder content
            this.wrapPanel.Children.Clear();

            // Add in display content
            for (var index = 0; index < lista.Count; ++index)
            {
                var button = new KinectTileButton { Label = lista[index].nombre , Key = lista[index].idArtista.ToString() , Procedence = "Artistas"};
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(lista[index].picture));
                button.Background = brush;
                this.wrapPanel.Children.Add(button);
            }

        }

        private void fillGenreInfo(String title, List<genero> lista)
        {
            this.txtTitle.Text = title;

            // Clear out placeholder content
            this.wrapPanel.Children.Clear();

            // Add in display content
            for (var index = 0; index < lista.Count; ++index)
            {
                var button = new KinectTileButton { Label = lista[index].nombre, Key = lista[index].idGenero.ToString() , Procedence = "Géneros" };
                this.wrapPanel.Children.Add(button);
            }

        }

        private void fillAlbumInfo(String title, List<album> lista)
        {
            this.txtTitle.Text = title;

            // Clear out placeholder content
            this.wrapPanel.Children.Clear();

            // Add in display content
            for (var index = 0; index < lista.Count; ++index)
            {
                var button = new KinectTileButton { Label = lista[index].nombre, Key = lista[index].idAlbum.ToString() , Procedence = "Album",  };
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(lista[index].cover));
                button.Background = brush;
               // (button.Label as System.Windows.Controls.Label).Height = 200;
                
                this.wrapPanel.Children.Add(button);
            }

        }

        private void fillSongInfo(String title, List<AlbumCover> lista)
        {
            this.txtTitle.Text = title;

            // Clear out placeholder content
            this.wrapPanel.Children.Clear();

            // Add in display content
            for (var index = 0; index < lista.Count; ++index)
            {
                var button = new KinectTileButton { Label = lista[index].nombre, Key = lista[index].idCancion.ToString(), Procedence = "Canciones" };
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(lista[index].cover));
                button.Background = brush;
                this.wrapPanel.Children.Add(button);
            }

        }

        private void fillHitsInfo(String title, List<listaRatingEntity> lista)
        {
            this.txtTitle.Text = title;

            // Clear out placeholder content
            this.wrapPanel.Children.Clear();

            // Add in display content
            for (var index = 0; index < lista.Count; ++index)
            {
                var button = new KinectTileButton { Label = lista[index].nombre, Key = lista[index].idCancion.ToString(), Procedence = "Canciones" };
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(lista[index].cover));
                button.Background = brush;
                this.wrapPanel.Children.Add(button);
            }

        }

        /// <summary>
        /// Handle paging right (next button).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PageRightButtonClick(object sender, RoutedEventArgs e)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + PixelScrollByAmount);
        }

        /// <summary>
        /// Handle paging left (previous button).
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void PageLeftButtonClick(object sender, RoutedEventArgs e)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - PixelScrollByAmount);
        }

        /// <summary>
        /// Change button state depending on scroll viewer position
        /// </summary>
        private void UpdatePagingButtonState()
        {
            this.PageLeftEnabled = scrollViewer.HorizontalOffset > ScrollErrorMargin;
            this.PageRightEnabled = scrollViewer.HorizontalOffset < scrollViewer.ScrollableWidth - ScrollErrorMargin;
        }

        private void txtTitle_Loaded(object sender, RoutedEventArgs e)
        {
            // Add in display content
            var target = this.txtTitle.Text;
            if(target.Equals("Artistas"))
            {
                fillArtistInfo("Artistas", Artists.Instance.artistList());
            }
            else if(target.Equals("Géneros"))
            {
                fillGenreInfo("Géneros", Genres.Instance.genreList());
            }
            else if (target.Equals("Éxitos"))
            {
                fillHitsInfo("Éxitos", Songs.Instance.getHits());
            }
        }

        /// <summary>
        /// Close the full screen view of the image
        /// </summary>
        private void OnCloseFullImage(object sender, RoutedEventArgs e)
        {
            // Always go to normal state before a transition
            VisualStateManager.GoToElementState(OverlayGrid, NormalState, false);
            VisualStateManager.GoToElementState(OverlayGrid, FadeOutTransitionState, true);
            this.VideoPlayer.mediaElement.Stop();
        }

        /// <summary>
        /// Overlay the full screen view of the image
        /// </summary>
        private void OnDisplayFullImage(string source)
        {
            // Always go to normal state before a transition
            var fullPath = Path.GetFullPath(source);
            var sourceUri = new System.Uri(fullPath);
            this.VideoPlayer.Source = sourceUri;
            this.VideoPlayer.mediaElement.Play();
            VisualStateManager.GoToElementState(OverlayGrid, NormalState, false);
            VisualStateManager.GoToElementState(OverlayGrid, FadeInTransitionState, false);
        }

    }
}
