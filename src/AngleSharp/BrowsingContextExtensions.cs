namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Services;
    using AngleSharp.Css;
    using AngleSharp.Css.Services;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Services;
    using AngleSharp.Extensions;
    using AngleSharp.Io;
    using AngleSharp.Io.Services;
    using AngleSharp.Media;
    using AngleSharp.Media.Services;
    using AngleSharp.Scripting;
    using AngleSharp.Scripting.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for the browsing context.
    /// </summary>
    public static class BrowsingContextExtensions
    {
        #region Open

        /// <summary>
        /// Opens a new document without any content in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The new, yet empty, document.</returns>
        public static Task<IDocument> OpenNewAsync(this IBrowsingContext context, String url = null)
        {
            return context.OpenAsync(m => m.Address(url));
        }

        /// <summary>
        /// Opens a new document created from the response asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (context == null)
            {
                context = BrowsingContext.New();
            }

            var encoding = context.GetDefaultEncoding();
            var factory = context.GetFactory<IDocumentFactory>();
            var options = new CreateDocumentOptions(response, encoding);
            return factory.CreateAsync(context, options, cancel);
        }

        /// <summary>
        /// Opens a new document loaded from the specified request
        /// asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, DocumentRequest request, CancellationToken cancel)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var loader = context.GetService<IDocumentLoader>();

            if (loader != null)
            {
                var download = loader.DownloadAsync(request);
                cancel.Register(download.Cancel);

                using (var response = await download.Task.ConfigureAwait(false))
                {
                    if (response != null)
                    {
                        return await context.OpenAsync(response, cancel).ConfigureAwait(false);
                    }
                }
            }

            return await context.OpenNewAsync(request.Target.Href).ConfigureAwait(false);
        }

        /// <summary>
        /// Opens a new document loaded from the provided url asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url, CancellationToken cancel)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));
            
            var request = DocumentRequest.Get(url);

            if (context != null && context.Active != null)
            {
                request.Referer = context.Active.DocumentUri;
            }

            return context.OpenAsync(request, cancel);
        }

        /// <summary>
        /// Opens a new document loaded from a virtual response that can be 
        /// filled via the provided callback.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request, CancellationToken cancel)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (var response = VirtualResponse.Create(request))
            {
                return await context.OpenAsync(response, cancel).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Opens a new document loaded from a virtual response that can be 
        /// filled via the provided callback without any ability to cancel it.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request)
        {
            return context.OpenAsync(request, CancellationToken.None);
        }

        /// <summary>
        /// Opens a new document loaded from the provided url asynchronously in
        /// the given context without the ability to cancel it.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url)
        {
            return context.OpenAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Opens a new document loaded from the provided address asynchronously
        /// in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="address">The address to load.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, String address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            return context.OpenAsync(Url.Create(address), CancellationToken.None);
        }

        #endregion

        #region Navigate

        /// <summary>
        /// Navigates to the given document. Includes the document in the
        /// session history and sets it as the active document.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="document">The new document.</param>
        public static void NavigateTo(this IBrowsingContext context, IDocument document)
        {
            context.SessionHistory?.PushState(document, document.Title, document.Url);
            context.Active = document;
        }

        #endregion
        
        #region Encoding

        public static Encoding GetDefaultEncoding(this IBrowsingContext context)
        {
            var provider = context.GetProvider<IEncodingProvider>();
            var locale = context.GetLanguage();
            return provider?.Suggest(locale) ?? Encoding.UTF8;
        }

        #endregion

        #region Languages

        public static CultureInfo GetCulture(this IBrowsingContext context)
        {
            return context.GetService<CultureInfo>() ?? CultureInfo.CurrentUICulture;
        }

        public static CultureInfo GetCultureFrom(this IBrowsingContext context, String language)
        {
            try
            {
                return new CultureInfo(language);
            }
            catch (CultureNotFoundException)
            {
                return context.GetCulture();
            }
        }

        public static String GetLanguage(this IBrowsingContext context)
        {
            return context.GetCulture().Name;
        }

        #endregion

        #region Services

        public static TFactory GetFactory<TFactory>(this IBrowsingContext context)
            where TFactory : class
        {
            return context.GetServices<TFactory>().Single();
        }

        public static TProvider GetProvider<TProvider>(this IBrowsingContext context)
            where TProvider : class
        {
            return context.GetServices<TProvider>().SingleOrDefault();
        }

        public static IResourceService<TResource> GetResourceService<TResource>(this IBrowsingContext context, String type)
            where TResource : IResourceInfo
        {
            var services = context.GetServices<IResourceService<TResource>>();

            foreach (var service in services)
            {
                if (service.SupportsType(type))
                {
                    return service;
                }
            }

            return default(IResourceService<TResource>);
        }

        #endregion

        #region Cookies

        public static String GetCookie(this IBrowsingContext context, Url url)
        {
            var provider = context.GetProvider<ICookieProvider>();
            return provider?.GetCookie(url) ?? String.Empty;
        }

        public static void SetCookie(this IBrowsingContext context, Url url, String value)
        {
            var provider = context.GetProvider<ICookieProvider>();
            provider?.SetCookie(url, value);
        }

        #endregion

        #region Spell Check

        public static ISpellCheckService GetSpellCheck(this IBrowsingContext context, String language)
        {
            var substitute = default(ISpellCheckService);
            var services = context.GetServices<ISpellCheckService>();
            var culture = context.GetCultureFrom(language);
            var twoLetters = culture.TwoLetterISOLanguageName;

            foreach (var service in services)
            {
                var otherCulture = service.Culture;
                var otherTwoLetters = otherCulture.TwoLetterISOLanguageName;

                if (otherCulture != null)
                {
                    if (otherCulture.Equals(culture))
                    {
                        return service;
                    }
                    else if (substitute == null && otherTwoLetters.Is(twoLetters))
                    {
                        substitute = service;
                    }
                }
            }

            return substitute;
        }

        #endregion

        #region Parsing Styles

        public static ICssStyleEngine GetCssStyleEngine(this IBrowsingContext context)
        {
            return context.GetStyleEngine(MimeTypeNames.Css) as ICssStyleEngine;
        }

        public static IStyleEngine GetStyleEngine(this IBrowsingContext context, String type)
        {
            var provider = context.GetProvider<IStylingProvider>();
            return provider?.GetEngine(type);
        }

        #endregion

        #region Parsing Scripts

        public static Boolean IsScripting(this IBrowsingContext context)
        {
            return context?.GetProvider<IScriptingProvider>() != null;
        }

        public static IScriptEngine GetJsScriptEngine(this IBrowsingContext context)
        {
            return context.GetScriptEngine(MimeTypeNames.DefaultJavaScript);
        }

        public static IScriptEngine GetScriptEngine(this IBrowsingContext context, String type)
        {
            var provider = context.GetProvider<IScriptingProvider>();
            return provider?.GetEngine(type);
        }

        #endregion

        #region Commands

        public static ICommand GetCommand(this IBrowsingContext context, String commandId)
        {
            var provider = context.GetProvider<ICommandProvider>();
            return provider?.GetCommand(commandId);
        }

        #endregion

        #region Children

        /// <summary>
        /// Creates the specified target browsing context.
        /// </summary>
        /// <param name="document">
        /// The document that originates the request.
        /// </param>
        /// <param name="target">The specified target name.</param>
        /// <returns>The new context.</returns>
        public static IBrowsingContext CreateChildFor(this IBrowsingContext context, String target)
        {
            var security = Sandboxes.None;

            if (target.Is("_blank"))
            {
                target = null;
            }

            return context.CreateChild(target, security);
        }

        /// <summary>
        /// Gets the specified target browsing context.
        /// </summary>
        /// <param name="document">
        /// The document that originates the request.
        /// </param>
        /// <param name="target">The specified target name.</param>
        /// <returns>
        /// The available context, or null, if the context does not exist yet.
        /// </returns>
        public static IBrowsingContext FindChildFor(this IBrowsingContext context, String target)
        {
            if (String.IsNullOrEmpty(target) || target.Is("_self"))
            {
                return context;
            }
            else if (target.Is("_parent"))
            {
                return context.Parent ?? context;
            }
            else if (target.Is("_top"))
            {
                return context;
            }

            return context.FindChild(target);
        }

        #endregion

        #region Downloads

        /// <summary>
        /// Checks if the context is waiting for tasks from originator of type
        /// T to finish downloading.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <returns>Enumerable of awaitable tasks.</returns>
        public static IEnumerable<Task> GetDownloads<T>(this IBrowsingContext context)
            where T : INode
        {
            var loader = context.GetService<IResourceLoader>();

            if (loader == null)
            {
                return Enumerable.Empty<Task>();
            }

            return loader.GetDownloads().Where(m => m.Source is T).Select(m => m.Task);
        }

        #endregion
    }
}