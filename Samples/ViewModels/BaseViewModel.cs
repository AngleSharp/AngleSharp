namespace Samples.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected static readonly Char[] ws = new Char[] { ' ', '\n', '\t' };

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] String name = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected Uri Sanitize(String url)
        {
            Uri uri;

            if (File.Exists(url))
                url = "file://localhost/" + url.Replace('\\', '/');

            var lurl = url.ToLower();

            if (!lurl.StartsWith("file://") && !lurl.StartsWith("http://") && !lurl.StartsWith("https://"))
                url = "http://" + url;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                return uri;

            return new Uri("http://www.google.com/search?q=" + url);
        }
    }
}
