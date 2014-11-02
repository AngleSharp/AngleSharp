namespace Samples.Pages
{
    using AngleSharp;
    using FirstFloor.ModernUI.Windows;
    using FirstFloor.ModernUI.Windows.Navigation;
    using Samples.ViewModels;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for Browser.xaml
    /// </summary>
    public partial class Browser : Page, IContent
    {
        protected DOMViewModel vm;

        public Browser()
        {
            InitializeComponent();
            vm = new DOMViewModel();
            DataContext = vm;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            if (vm.DisplayRecent())
                Url.Text = vm.Address;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        void UrlKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                vm.Load(Url.Text);
        }

        void Button_Click(Object sender, RoutedEventArgs e)
        {
            vm.Load(Url.Text);
        }
    }
}
