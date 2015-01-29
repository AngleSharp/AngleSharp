namespace Samples.ViewModels
{
    using AngleSharp;
    using AngleSharp.Dom;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class RequestViewModel : BaseViewModel
    {
        #region Fields

        Task current;
        CancellationTokenSource cts;
        String status;
        Int32 state;

        #endregion

        #region Static members

        static Uri recentUrl;
        static String recentAddress;
        static Int32 recentState;
        static IDocument recentDocument;

        #endregion

        #region Properties

        public String Address
        {
            get { return recentAddress; }
        }

        public String Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        public void Load(String url)
        {
            if (current != null && !current.IsCompleted)
                cts.Cancel(false);

            cts = new CancellationTokenSource();
            current = LoadAsync(url, cts.Token);
        }

        public Boolean DisplayRecent()
        {
            if (recentState != state && SettingsViewModel.Instance.IsInSharedDocumentMode)
            {
                if (current != null && !current.IsCompleted)
                    cts.Cancel(false);

                cts = new CancellationTokenSource();
                current = Use(recentUrl, recentDocument, cts.Token);
                state = recentState;
                Status = "Displaying: " + recentUrl;
                return true;
            }

            return false;
        }

        async Task LoadAsync(String url, CancellationToken cancel)
        {
            Stream response;
            var http = new HttpClient();
            var uri = Sanitize(url);
            Status = "Loading " + uri.AbsoluteUri + " ...";

            if (uri.Scheme.Equals("file", StringComparison.Ordinal))
            {
                ProfilerViewModel.Data.Start("Response (HTML)", OxyPlot.OxyColors.Red);
                response = File.Open(uri.AbsolutePath.Substring(1), FileMode.Open);
                ProfilerViewModel.Data.Stop();
            }
            else
            {
                ProfilerViewModel.Data.Start("Response (HTML)", OxyPlot.OxyColors.Red);
                var request = await http.GetAsync(uri, cancel);
                response = await request.Content.ReadAsStreamAsync();
                ProfilerViewModel.Data.Stop();
                cancel.ThrowIfCancellationRequested();
            }

            Status = "Parsing " + uri.AbsoluteUri + " ...";
            ProfilerViewModel.Data.Start("Parsing (HTML)", OxyPlot.OxyColors.Orange);
            var document = DocumentBuilder.Html(response, new Configuration(), uri.AbsoluteUri);
            ProfilerViewModel.Data.Stop();
            response.Close();

            cancel.ThrowIfCancellationRequested();
            await Use(uri, document, cancel);

            UpdateRecent(document, url, uri);
            Status = "Displaying: " + url;
        }

        void UpdateRecent(IDocument document, String url, Uri uri)
        {
            recentAddress = url;
            SettingsViewModel.Instance.AddUrl(url);
            recentDocument = document;
            recentUrl = uri;
            recentState++;
            state = recentState;
        }

        protected abstract Task Use(Uri url, IDocument document, CancellationToken cancel);

        #endregion
    }
}
