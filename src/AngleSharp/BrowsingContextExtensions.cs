namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Dom.Events;
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Media;
    using AngleSharp.Scripting;
    using AngleSharp.Text;
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
        /// <param name="url">The optional base URL of the document. By default "http://localhost/".</param>
        /// <param name="cancellation">The cancellation token (optional)</param>
        /// <returns>The new, yet empty, document.</returns>
        public static Task<IDocument> OpenNewAsync(this IBrowsingContext context, String url = null, CancellationToken cancellation = default) =>
            context.OpenAsync(m => m.Address(url ?? "http://localhost/"), cancellation);

        /// <summary>
        /// Opens a new document created from the response asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel = default)
        {
            response = response ?? throw new ArgumentNullException(nameof(response));
            context = context ?? BrowsingContext.New();
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
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, DocumentRequest request, CancellationToken cancel = default)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            context = context ?? BrowsingContext.New();
            return context.NavigateToAsync(request, cancel);
        }

        /// <summary>
        /// Opens a new document loaded from the provided url asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url, CancellationToken cancel = default)
        {
            url = url ?? throw new ArgumentNullException(nameof(url));
            return context.OpenAsync(DocumentRequest.Get(url, referer: context?.Active?.DocumentUri), cancel);
        }

        /// <summary>
        /// Opens a new document loaded from a virtual response that can be
        /// filled via the provided callback.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request, CancellationToken cancel = default)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            using (var response = VirtualResponse.Create(request))
            {
                return await context.OpenAsync(response, cancel).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Opens a new document loaded from the provided address asynchronously
        /// in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="address">The address to load.</param>
        /// <param name="cancellation">The cancellation token (optional)</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, String address, CancellationToken cancellation = default)
        {
            address = address ?? throw new ArgumentNullException(nameof(address));
            return context.OpenAsync(Url.Create(address), cancellation);
        }

        #endregion

        #region Navigate

        /// <summary>
        /// Plan to navigate to an action using the specified method with the given
        /// entity body of the mime type.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#plan-to-navigate
        /// </summary>
        /// <param name="context">The browsing context.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel"></param>
        /// <returns>A task that will eventually result in a new document.</returns>
        internal static Task<IDocument> NavigateToAsync(this IBrowsingContext context, DocumentRequest request, CancellationToken cancel = default)
        {
            var handler = context.GetNavigationHandler(request.Target);
            return handler?.NavigateAsync(request, cancel) ?? Task.FromResult<IDocument>(null);
        }

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

        /// <summary>
        /// Gets the navigation handler that supports the provided protocol.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to navigate to.</param>
        /// <returns>The found navigation handler, if any.</returns>
        public static INavigationHandler GetNavigationHandler(this IBrowsingContext context, Url url) =>
            context.GetServices<INavigationHandler>().FirstOrDefault(m => m.SupportsProtocol(url.Scheme));

        #endregion

        #region Encoding

        /// <summary>
        /// Gets the default encoding to use as initial guess.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The encoding from the provider or UTF-8.</returns>
        public static Encoding GetDefaultEncoding(this IBrowsingContext context)
        {
            var provider = context.GetProvider<IEncodingProvider>();
            var locale = context.GetLanguage();
            return provider?.Suggest(locale) ?? Encoding.UTF8;
        }

        #endregion

        #region Languages

        /// <summary>
        /// Gets the culture info associated with the current context.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The culture info assigned to the context.</returns>
        public static CultureInfo GetCulture(this IBrowsingContext context) => context.GetService<CultureInfo>() ?? CultureInfo.CurrentUICulture;

        /// <summary>
        /// Gets the culture from the language string (or the current culture).
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="language">The ISO culture name.</param>
        /// <returns>
        /// The culture info representing the language or the current culture.
        /// </returns>
        public static CultureInfo GetCultureFrom(this IBrowsingContext context, String language)
        {
            try
            {
                return new CultureInfo(language);
            }
            catch (CultureNotFoundException ex)
            {
                context.TrackError(ex);
                return context.GetCulture();
            }
        }

        /// <summary>
        /// Gets the language of the current context.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The ISO name of the culture.</returns>
        public static String GetLanguage(this IBrowsingContext context) => context.GetCulture().Name;

        #endregion

        #region Services

        /// <summary>
        /// Gets a factory service instance. Exactly one has to be available.
        /// </summary>
        /// <typeparam name="TFactory">The type of the factory service.</typeparam>
        /// <param name="context">The current context.</param>
        /// <returns>The factory instance.</returns>
        public static TFactory GetFactory<TFactory>(this IBrowsingContext context)
            where TFactory : class => context.GetServices<TFactory>().Single();

        /// <summary>
        /// Gets a provider service instance. At most one has to be available.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider service.</typeparam>
        /// <param name="context">The current context.</param>
        /// <returns>The provider instance or null.</returns>
        public static TProvider GetProvider<TProvider>(this IBrowsingContext context)
            where TProvider : class => context.GetServices<TProvider>().SingleOrDefault();

        /// <summary>
        /// Gets a resource service. Multiple resource services may be registered, so
        /// the one that matches the given mime-type will be returned, if any.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource service.</typeparam>
        /// <param name="context">The current context.</param>
        /// <param name="type">The mime-type of the resource.</param>
        /// <returns>The service instance or null.</returns>
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

            return default;
        }

        #endregion

        #region Cookies

        /// <summary>
        /// Gets the cookie for the given URL, if any.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="url">The URL of the cookie.</param>
        /// <returns>The cookie or the empty string.</returns>
        public static String GetCookie(this IBrowsingContext context, Url url)
        {
            var provider = context.GetProvider<ICookieProvider>();
            return provider?.GetCookie(url) ?? String.Empty;
        }

        /// <summary>
        /// Sets the cookie for the given URL.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="url">The URL of the cookie.</param>
        /// <param name="value">The cookie value to set.</param>
        public static void SetCookie(this IBrowsingContext context, Url url, String value)
        {
            var provider = context.GetProvider<ICookieProvider>();
            provider?.SetCookie(url, value);
        }

        #endregion

        #region Spell Check

        /// <summary>
        /// Gets the spell check service for the given language, if any.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="language">The language of the spellchecker.</param>
        /// <returns>The spell check service, if any.</returns>
        public static ISpellCheckService GetSpellCheck(this IBrowsingContext context, String language)
        {
            var substitute = default(ISpellCheckService);
            var services = context.GetServices<ISpellCheckService>();
            var culture = context.GetCultureFrom(language);
            var twoLetters = culture.TwoLetterISOLanguageName;

            foreach (var service in services)
            {
                var otherCulture = service.Culture;

                if (otherCulture != null)
                {
                    var otherTwoLetters = otherCulture.TwoLetterISOLanguageName;

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

        /// <summary>
        /// Tries to get the CSS styling service, if available.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The CSS styling service if any.</returns>
        public static IStylingService GetCssStyling(this IBrowsingContext context) => context.GetStyling(MimeTypeNames.Css);

        /// <summary>
        /// Tries to get the styling service for the given mime-type.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="type">The type of the style engine.</param>
        /// <returns>The styling service if any.</returns>
        public static IStylingService GetStyling(this IBrowsingContext context, String type)
        {
            var services = context.GetServices<IStylingService>();

            foreach (var service in services)
            {
                if (service.SupportsType(type))
                {
                    return service;
                }
            }

            return default;
        }

        #endregion

        #region Parsing Scripts

        /// <summary>
        /// Gets if the context allows scripting or not.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>True if a scripting provider is available, otherwise false.</returns>
        public static Boolean IsScripting(this IBrowsingContext context) => context.GetServices<IScriptingService>().Any();

        /// <summary>
        /// Tries to get the JavaScript service, if available.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <returns>The JavaScript scripting service, if any.</returns>
        public static IScriptingService GetJsScripting(this IBrowsingContext context) => context.GetScripting(MimeTypeNames.DefaultJavaScript);

        /// <summary>
        /// Tries to get the scripting service for the given mime-type.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="type">The type of the scripting language.</param>
        /// <returns>The scripting service, if any.</returns>
        public static IScriptingService GetScripting(this IBrowsingContext context, String type)
        {
            var services = context.GetServices<IScriptingService>();

            foreach (var service in services)
            {
                if (service.SupportsType(type))
                {
                    return service;
                }
            }

            return default;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Tries to get the command with the given name.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="commandId">The command to get.</param>
        /// <returns>The command if any.</returns>
        public static ICommand GetCommand(this IBrowsingContext context, String commandId)
        {
            var provider = context.GetProvider<ICommandProvider>();
            return provider?.GetCommand(commandId);
        }

        #endregion

        #region Events

        /// <summary>
        /// Notifies the context of an exception that was handled internally.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="ex">The exception to notify.</param>
        public static void TrackError(this IBrowsingContext context, Exception ex)
        {
            var ev = new TrackEvent("error", ex);
            context.Fire(ev);
        }

        /// <summary>
        /// Fires an interactive event at the given context.
        /// </summary>
        /// <typeparam name="T">The type of interactivity payload.</typeparam>
        /// <param name="context">The current context.</param>
        /// <param name="eventName">The name of the event to fire.</param>
        /// <param name="data">The data to transport.</param>
        /// <returns>The task with the response to the event.</returns>
        public static Task InteractAsync<T>(this IBrowsingContext context, String eventName, T data)
        {
            var ev = new InteractivityEvent<T>(eventName, data);
            context.Fire(ev);
            return ev.Result ?? Task.FromResult(false);
        }

        #endregion

        #region Children

        /// <summary>
        /// Resolves the given target context.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="target">The desired target frame.</param>
        /// <returns>The target context.</returns>
        public static IBrowsingContext ResolveTargetContext(this IBrowsingContext context, String target)
        {
            var createBrowsingContext = false;
            var targetBrowsingContext = context;
            //var replace = owner.ReadyState != DocumentReadyState.Complete;

            if (!String.IsNullOrEmpty(target))
            {
                targetBrowsingContext = context.FindChildFor(target);
                createBrowsingContext = targetBrowsingContext == null;
            }

            if (createBrowsingContext)
            {
                targetBrowsingContext = context.CreateChildFor(target);
                //replace = true;
            }

            return targetBrowsingContext;
        }

        /// <summary>
        /// Creates the specified target browsing context.
        /// </summary>
        /// <param name="context">The current context.</param>
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
        /// <param name="context">The current context.</param>
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