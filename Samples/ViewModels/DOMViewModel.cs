namespace Samples.ViewModels
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;

    public class DOMViewModel : RequestViewModel
    {
        ObservableCollection<DOMNodeViewModel> source;

        public DOMViewModel ()
	    {
            Status = "Nothing to display ...";
            source = new ObservableCollection<DOMNodeViewModel>();
	    }

        public ObservableCollection<DOMNodeViewModel> Source
        {
            get { return source; }
        }

        public DOMNodeViewModel Root
        {
            set
            {
                source.Clear();
                source.Add(value);
            }
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            Status = "Constructing the DOM ...";
            Root = new DOMNodeViewModel(document);
            await Task.Yield();
        }
    }
}
