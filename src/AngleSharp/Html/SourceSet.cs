namespace AngleSharp.Html
{
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a useful helper for dealing with source sets.
    /// </summary>
    public sealed class SourceSet
    {
        private static readonly String FullWidth = "100vw";
        private static readonly Regex SizeParser = CreateRegex();

        private static Regex CreateRegex()
        {
            var regexString = @"(\([^)]+\))?\s*(.+)";

            try
            {
                return new Regex(regexString, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
            }
            catch
            {
                // See issue #256
                return new Regex(regexString, RegexOptions.ECMAScript);
            }
        }

        private readonly IDocument _document;

        /// <summary>
        /// Creates a new SourceSet representation within the given document.
        /// </summary>
        /// <param name="document">The hosting document.</param>
        public SourceSet(IDocument document)
        {
            _document = document;
        }

        /// <summary>
        /// Parses the given srcset attribute into an enumeration of candidates.
        /// </summary>
        /// <param name="srcset">The value of the srcset attribute.</param>
        /// <returns>The iterator yielding the various candidates.</returns>
        public static IEnumerable<ImageCandidate> Parse(String srcset)
        {
            var sources = srcset.Trim().SplitSpaces();

            for (var i = 0; i < sources.Length; i++)
            {
                var url = sources[i];
                var descriptor = default(String);

                if (url.Length != 0)
                {
                    if (url[url.Length - 1] == Symbols.Comma)
                    {
                        url = url.Remove(url.Length - 1);
                        descriptor = String.Empty;
                    }
                    else if (++i < sources.Length)
                    {
                        descriptor = sources[i];
                        var descpos = descriptor.IndexOf(Symbols.Comma);

                        if (descpos != -1)
                        {
                            sources[i] = descriptor.Substring(descpos + 1);
                            descriptor = descriptor.Substring(0, descpos);
                            --i;
                        }
                    }

                    yield return new ImageCandidate
                    {
                        Url = url,
                        Descriptor = descriptor
                    };
                }
            }
        }

        private static MediaSize ParseSize(String sourceSizeStr)
        {
            var match = SizeParser.Match(sourceSizeStr);

            return new MediaSize
            {
                Media = match.Success && match.Groups[1].Success ? match.Groups[1].Value : String.Empty,
                Length = match.Success && match.Groups[2].Success ? match.Groups[2].Value : String.Empty
            };
        }

        private Double ParseDescriptor(String descriptor, String sizesattr = null)
        {
            var sizes = sizesattr ?? FullWidth;
            var sizeDescriptor = descriptor.Trim();
            var widthInCssPixels = GetWidthFromSourceSize(sizes);
            var resCandidate = 1.0;
            var splitDescriptor = sizeDescriptor.Split(' ');

            for (var i = splitDescriptor.Length - 1; i >= 0; i--)
            {
                var curr = splitDescriptor[i];
                var lastchar = curr.Length > 0 ? curr[curr.Length - 1] : Symbols.Null;

                if ((lastchar == 'h' || lastchar == 'w') && curr.Length > 2 && curr[curr.Length] == 'v')
                {
                    resCandidate = curr.Substring(0, curr.Length - 2).ToInteger(0) / widthInCssPixels;
                }
                else if (lastchar == 'x' && curr.Length > 0)
                {
                    resCandidate = curr.Substring(0, curr.Length - 1).ToDouble(1.0);
                }
            }

            return resCandidate;
        }

        private Double GetWidthFromLength(String length)
        {
            var value = default(Length);

            if (Length.TryParse(length, out value))
            {
                //TODO Compute Value from RenderDevice
            }

            return 0.0;
        }

        private Double GetWidthFromSourceSize(String sourceSizes)
        {
            var sizes = sourceSizes.Trim().Split(Symbols.Comma);

            for (var i = 0; i < sizes.Length; i++)
            {
                var size = sizes[i];
                var parsedSize = ParseSize(size);
                var length = parsedSize.Length;
                var media = parsedSize.Media;

                if (!String.IsNullOrEmpty(length))
                {
                    if (String.IsNullOrEmpty(media) || _document.DefaultView.MatchMedia(media).IsMatched)
                    {
                        return GetWidthFromLength(length);
                    }
                }
            }

            return GetWidthFromLength(FullWidth);
        }

        /// <summary>
        /// Gets the promising candidates from the given srcset using the provided sizes.
        /// </summary>
        /// <param name="srcset">The value of the srcset attribute.</param>
        /// <param name="sizes">The value of the sizes attribute.</param>
        /// <returns>An iterator of the different URLs yielding matching images.</returns>
        public IEnumerable<String> GetCandidates(String srcset, String sizes)
        {
            if (!String.IsNullOrEmpty(srcset))
            {
                //Resolution = ParseDescriptor(candidate.Descriptor, sizes)
                foreach (var candidate in Parse(srcset))
                {
                    yield return candidate.Url;
                }
            }
        }

        sealed class MediaSize
        {
            public String Media { get; set; }

            public String Length { get; set; }
        }

        /// <summary>
        /// Represents a srcset image candidate.
        /// </summary>
        public sealed class ImageCandidate
        {
            /// <summary>
            /// The URL of the given image.
            /// </summary>
            public String Url { get; set; }

            /// <summary>
            /// The descriptor of the given image.
            /// </summary>
            public String Descriptor { get; set; }
        }
    }
}
