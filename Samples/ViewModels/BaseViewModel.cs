using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] String name = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected Uri Sanitize(String url)
        {
            Uri uri;
            var lurl = url.ToLower();

            if (!lurl.StartsWith("http://") && !lurl.StartsWith("https://"))
                url = "http://" + url;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                return uri;

            return new Uri("http://www.google.com/search?q=" + url);
        }
    }
}
