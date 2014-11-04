namespace Samples.Pages
{
    using Samples.ViewModels;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Repl.xaml
    /// </summary>
    public partial class Repl : Page
    {
        public Repl()
        {
            InitializeComponent();
            DataContext = new ReplViewModel();
        }
    }
}
