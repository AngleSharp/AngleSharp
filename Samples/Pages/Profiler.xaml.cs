using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using Samples.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Samples.Pages
{
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
