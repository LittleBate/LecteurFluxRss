using WPRssReader.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WPRssReader.ViewModel;
using Buisness;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace WPRssReader
{
    public sealed partial class PivotPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        //private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public PivotPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            DataContext = VMApplication.Instance;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache. Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        /// <summary>
        /// Adds an item to the list when the app bar button is clicked.
        /// </summary>
        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void FluxSelectionneChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Frame.Navigate(typeof(VFlux));
        }

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pivot.SelectedIndex == 0)
            {
                btnBarRefresh.Visibility = Windows.UI.Xaml.Visibility.Visible;
                btnBarValider.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnBarRecherche.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else if(pivot.SelectedIndex == 1)
            {
                btnBarRefresh.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnBarValider.Visibility = Windows.UI.Xaml.Visibility.Visible;
                btnBarRecherche.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else if(pivot.SelectedIndex == 2)
            {
                btnBarRefresh.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnBarValider.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnBarRecherche.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if(pivot.SelectedIndex == 3)
            {
                btnBarRefresh.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnBarValider.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                btnBarRecherche.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

    }
}
