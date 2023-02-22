﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngleSharp.Benchmarks
{
    public sealed class UrlTests
    {
        readonly Boolean _buffer;
        readonly String _extension;

        public UrlTests(String extension, Boolean withBuffer = true)
        {
            Tests = new List<UrlTest>();
            _buffer = withBuffer;
            _extension = extension;
        }

        public List<UrlTest> Tests { get; }

        public async Task<UrlTests> Include(params String[] urls)
        {
            var tasks = new Task[urls.Length];

            for (int i = 0; i < urls.Length; i++)
            {
                tasks[i] = Include(urls[i]);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return this;
        }

        public async Task<UrlTests> Include(String url)
        {
            var test = await UrlTest.For(url, _extension, _buffer).ConfigureAwait(false);
            Tests.Add(test);
            return this;
        }
    }
}
