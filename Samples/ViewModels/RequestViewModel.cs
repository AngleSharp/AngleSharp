using AngleSharp;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public abstract class RequestViewModel : BaseViewModel
    {
        Task current;
        CancellationTokenSource cts;
        String status;

        public String Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged();
            }
        }

        public void Load(String url)
        {
            if (current != null && !current.IsCompleted)
                cts.Cancel(false);

            cts = new CancellationTokenSource();
            current = LoadAsync(url, cts.Token);
        }

        async Task LoadAsync(String url, CancellationToken cancel)
        {
            var http = new HttpClient();
            var uri = Sanitize(url);
            Status = "Loading " + uri.AbsoluteUri + " ...";
            var request = await http.GetAsync(uri);
            cancel.ThrowIfCancellationRequested();
            var response = await request.Content.ReadAsStreamAsync();
            cancel.ThrowIfCancellationRequested();
            Status = "Parsing " + uri.AbsoluteUri + " ...";
            var document = DocumentBuilder.Html(response);
            cancel.ThrowIfCancellationRequested();
            await Use(uri, document, cancel);
            Status = "Displaying: " + url;
        }

        protected abstract Task Use(Uri url, HTMLDocument document, CancellationToken cancel);
    }
}
