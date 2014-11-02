namespace Samples.Pages
{
    using FirstFloor.ModernUI.Windows;
    using FirstFloor.ModernUI.Windows.Navigation;
    using Samples.ViewModels;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for Profiler.xaml
    /// </summary>
    public partial class Profiler : Page, IContent
    {
        ProfilerViewModel vm;

        public Profiler()
        {
            InitializeComponent();
            vm = ProfilerViewModel.Data;
            DataContext = vm;
            vm.PropertyChanged += ProfilePropertyChanged;
        }

        void ProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            plot.RefreshPlot(true);
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
