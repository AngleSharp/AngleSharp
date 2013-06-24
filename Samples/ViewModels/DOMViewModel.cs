using AngleSharp;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
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

        protected override async Task Use(Uri url, HTMLDocument document, CancellationToken cancel)
        {
            Status = "Constructing the DOM ...";
            Root = new DOMNodeViewModel(document);
            await Task.Yield();
        }
    }
}
