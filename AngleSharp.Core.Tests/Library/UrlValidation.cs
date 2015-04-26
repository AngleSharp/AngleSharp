namespace AngleSharp.Core.Tests.Library
{
    using System;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using NUnit.Framework;

    /// <summary>
    /// Tests automatically generated from:
    /// https://w3c-test.org/url/a-element.html
    /// </summary>
    [TestFixture]
	public class UrlValidationTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
		public void DocumentUrlTest1()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example	.
org");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest2()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://user:pass@foo:21/bar;par?b#c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("21", anchor.Port);
			Assert.AreEqual("/bar;par", anchor.PathName);
			Assert.AreEqual("?b", anchor.Search);
			Assert.AreEqual("#c", anchor.Hash);
			Assert.AreEqual("http://user:pass@foo:21/bar;par?b#c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest3()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:foo.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/foo.com", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/foo.com", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest4()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"	   :foo.com   
");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:foo.com", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:foo.com", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest5()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @" foo.com  ");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/foo.com", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/foo.com", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest6()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"a:	 foo.com");
			Assert.AreEqual("a:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual(" foo.com", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("a: foo.com", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest7()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:21/ b ? d # e ");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("21", anchor.Port);
			Assert.AreEqual("/%20b%20", anchor.PathName);
			Assert.AreEqual("?%20d%20", anchor.Search);
			Assert.AreEqual("# e", anchor.Hash);
			Assert.AreEqual("http://f:21/%20b%20?%20d%20# e", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest8()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://f/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest9()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:0/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("0", anchor.Port);
			Assert.AreEqual("/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://f:0/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest10()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:00000000000000/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("0", anchor.Port);
			Assert.AreEqual("/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://f:0/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest11()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:00000000000000000000080/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://f/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest14()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:
/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://f/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest16()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://f:999999/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("f", anchor.HostName);
			Assert.AreEqual("999999", anchor.Port);
			Assert.AreEqual("/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://f:999999/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest18()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest19()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"  	");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest20()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":foo.com/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:foo.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:foo.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest21()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":foo.com\");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:foo.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:foo.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest22()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest23()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":a");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:a", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:a", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest24()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest25()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":\");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest26()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":#");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:#", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest27()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"#");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar#", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest28()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"#/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("#/", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar#/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest29()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"#\");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual(@"#\", anchor.Hash);
			Assert.AreEqual(@"http://example.org/foo/bar#\", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest30()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"#;?");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("#;?", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar#;?", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest31()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"?");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar?", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest32()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest33()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @":23");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:23", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:23", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest34()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/:23");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/:23", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/:23", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest35()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"::");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/::", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/::", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest36()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"::23");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/::23", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/::23", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest37()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"foo://");
			Assert.AreEqual("foo:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("//", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("foo://", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest38()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://a:b@c:29/d");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("c", anchor.HostName);
			Assert.AreEqual("29", anchor.Port);
			Assert.AreEqual("/d", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:b@c:29/d", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest39()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http::@c:29");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/:@c:29", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/:@c:29", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest40()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://&a:foo(b]c@d:2/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("d", anchor.HostName);
			Assert.AreEqual("2", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://&a:foo(b]c@d:2/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest41()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://::@c@d:2");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("d", anchor.HostName);
			Assert.AreEqual("2", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://:%3A%40c@d:2/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest42()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo.com:b@d/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("d", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo.com:b@d/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest43()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo.com/\@");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("//@", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo.com//@", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest44()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:\\foo.com\");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest45()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:\\a\b:c\d@foo.com\");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("a", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/b:c/d@foo.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a/b:c/d@foo.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest46()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"foo:/");
			Assert.AreEqual("foo:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("foo:/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest47()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"foo:/bar.com/");
			Assert.AreEqual("foo:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/bar.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("foo:/bar.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest48()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"foo://///////");
			Assert.AreEqual("foo:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/////////", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("foo://///////", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest49()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"foo://///////bar.com/");
			Assert.AreEqual("foo:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/////////bar.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("foo://///////bar.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest50()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"foo:////://///");
			Assert.AreEqual("foo:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("////://///", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("foo:////://///", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest51()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"c:/foo");
			Assert.AreEqual("c:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("c:/foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest52()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"//foo/bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest53()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo/path;a??e#f#g");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/path;a", anchor.PathName);
			Assert.AreEqual("??e", anchor.Search);
			Assert.AreEqual("#f#g", anchor.Hash);
			Assert.AreEqual("http://foo/path;a??e#f#g", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest54()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo/abcd?efgh?ijkl");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/abcd", anchor.PathName);
			Assert.AreEqual("?efgh?ijkl", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo/abcd?efgh?ijkl", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest55()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo/abcd#foo?bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/abcd", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("#foo?bar", anchor.Hash);
			Assert.AreEqual("http://foo/abcd#foo?bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest56()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"[61:24:74]:98");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/[61:24:74]:98", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/[61:24:74]:98", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest57()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:[61:27]/:foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/[61:27]/:foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/[61:27]/:foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest62()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://[2001::1]");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("[2001::1]", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://[2001::1]/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest63()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://[2001::1]:80");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("[2001::1]", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://[2001::1]/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest64()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:/example.com/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest65()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftp:/example.com/");
			Assert.AreEqual("ftp:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftp://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest66()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"https:/example.com/");
			Assert.AreEqual("https:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("https://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest67()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"madeupscheme:/example.com/");
			Assert.AreEqual("madeupscheme:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("madeupscheme:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest68()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file:/example.com/");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest69()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftps:/example.com/");
			Assert.AreEqual("ftps:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftps:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest70()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"gopher:/example.com/");
			Assert.AreEqual("gopher:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("gopher://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest71()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws:/example.com/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest72()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss:/example.com/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest73()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"data:/example.com/");
			Assert.AreEqual("data:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("data:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest74()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"javascript:/example.com/");
			Assert.AreEqual("javascript:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("javascript:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest75()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"mailto:/example.com/");
			Assert.AreEqual("mailto:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("mailto:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest76()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:example.com/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest77()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftp:example.com/");
			Assert.AreEqual("ftp:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftp://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest78()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"https:example.com/");
			Assert.AreEqual("https:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("https://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest79()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"madeupscheme:example.com/");
			Assert.AreEqual("madeupscheme:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("madeupscheme:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest80()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftps:example.com/");
			Assert.AreEqual("ftps:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftps:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest81()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"gopher:example.com/");
			Assert.AreEqual("gopher:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("gopher://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest82()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws:example.com/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest83()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss:example.com/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest84()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"data:example.com/");
			Assert.AreEqual("data:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("data:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest85()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"javascript:example.com/");
			Assert.AreEqual("javascript:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("javascript:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest86()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"mailto:example.com/");
			Assert.AreEqual("mailto:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("mailto:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest87()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/a/b/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/a/b/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/a/b/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest88()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/a/ /c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/a/%20/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/a/%20/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest89()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/a%2fc");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/a%2fc", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/a%2fc", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest90()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/a/%2f/c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/a/%2f/c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.org/a/%2f/c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest91()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"#β");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.org", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("#β", anchor.Hash);
			Assert.AreEqual("http://example.org/foo/bar#β", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest92()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://example.org/foo/bar";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"data:text/html,test#test");
			Assert.AreEqual("data:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("text/html,test", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("#test", anchor.Hash);
			Assert.AreEqual("data:text/html,test#test", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest93()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file:c:\foo\bar.html");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/c:/foo/bar.html", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///c:/foo/bar.html", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest94()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"  File:c|////foo\bar.html");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/c:////foo/bar.html", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///c:////foo/bar.html", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest95()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"C|/foo/bar");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/C:/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///C:/foo/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest96()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/C|\foo\bar");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/C:/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///C:/foo/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest97()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"//C|/foo/bar");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/C:/foo/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///C:/foo/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest98()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"//server/file");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("server", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/file", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://server/file", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest99()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"\\server\file");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("server", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/file", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://server/file", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest100()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/\server/file");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("server", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/file", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://server/file", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest101()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file:///foo/bar.txt");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///foo/bar.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest102()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file:///home/me");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/home/me", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///home/me", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest103()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"//");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest104()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"///");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest105()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"///test");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///test", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest106()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file://test");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("test", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://test/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest107()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file://localhost");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("localhost", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://localhost/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest108()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file://localhost/");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("localhost", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://localhost/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest109()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file://localhost/test");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("localhost", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file://localhost/test", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest110()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"test");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/tmp/mock/test", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///tmp/mock/test", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest111()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"file:///tmp/mock/path";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file:test");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/tmp/mock/test", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///tmp/mock/test", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest112()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/././foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest113()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/./.foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/.foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/.foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest114()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/.");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest115()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/./");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest116()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/bar/..");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest117()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/bar/../");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest118()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/..bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/..bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/..bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest119()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/bar/../ton");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/ton", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/ton", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest120()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/bar/../ton/../../a");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/a", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/a", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest121()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/../../..");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest122()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/../../../ton");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/ton", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/ton", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest123()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/%2e");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest124()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/%2e%2");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/%2e%2", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/%2e%2", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest125()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/%2e./%2e%2e/.%2e/%2e.bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%2e.bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%2e.bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest126()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com////../..");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("//", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com//", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest127()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/bar//../..");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest128()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo/bar//..");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/bar/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo/bar/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest129()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest130()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/%20foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%20foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%20foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest131()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo%");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest132()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo%2");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%2", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%2", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest133()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo%2zbar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%2zbar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%2zbar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest134()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo%2Â©zbar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%2%C3%82%C2%A9zbar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%2%C3%82%C2%A9zbar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest135()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo%41%7a");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%41%7a", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%41%7a", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest136()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo	%91");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%C2%91%91", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%C2%91%91", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest137()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo%00%51");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%00%51", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foo%00%51", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest138()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/(%28:%3A%29)");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/(%28:%3A%29)", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/(%28:%3A%29)", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest139()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/%3A%3a%3C%3c");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%3A%3a%3C%3c", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%3A%3a%3C%3c", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest140()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/foo	bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foobar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/foobar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest141()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com\\foo\\bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("//foo//bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com//foo//bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest142()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/%7Ffp3%3Eju%3Dduvgw%3Dd");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%7Ffp3%3Eju%3Dduvgw%3Dd", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%7Ffp3%3Eju%3Dduvgw%3Dd", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest143()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/@asdf%40");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/@asdf%40", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/@asdf%40", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest144()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/你好你好");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%E4%BD%A0%E5%A5%BD%E4%BD%A0%E5%A5%BD", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%E4%BD%A0%E5%A5%BD%E4%BD%A0%E5%A5%BD", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest145()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/‥/foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%E2%80%A5/foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%E2%80%A5/foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest146()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/﻿/foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%EF%BB%BF/foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%EF%BB%BF/foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest147()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://example.com/‮/foo/‭/bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%E2%80%AE/foo/%E2%80%AD/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/%E2%80%AE/foo/%E2%80%AD/bar", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest148()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www.google.com/foo?bar=baz#");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.google.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo", anchor.PathName);
			Assert.AreEqual("?bar=baz", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.google.com/foo?bar=baz#", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest149()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www.google.com/foo?bar=baz# »");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.google.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo", anchor.PathName);
			Assert.AreEqual("?bar=baz", anchor.Search);
			Assert.AreEqual("# »", anchor.Hash);
			Assert.AreEqual("http://www.google.com/foo?bar=baz# »", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest150()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"data:test# »");
			Assert.AreEqual("data:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("test", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("# »", anchor.Hash);
			Assert.AreEqual("data:test# »", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest152()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www.google.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.google.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.google.com/", anchor.Href);
		}
	
		//TODO [Test]
		public void DocumentUrlTest153()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://192.0x00A80001");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("192.168.0.1", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://192.168.0.1/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest154()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www/foo%2Ehtml");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo%2Ehtml", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www/foo%2Ehtml", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest155()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www/foo/%2E/html");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo/html", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www/foo/html", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest157()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://%25DOMAIN:foobar@foodomain.com/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foodomain.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://%25DOMAIN:foobar@foodomain.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest158()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:\\www.google.com\foo");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.google.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/foo", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.google.com/foo", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest159()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo:80/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest160()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo:81/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("81", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://foo:81/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest161()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"httpa://foo:80/");
			Assert.AreEqual("httpa:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("//foo:80/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("httpa://foo:80/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest163()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"https://foo:443/");
			Assert.AreEqual("https:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("https://foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest164()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"https://foo:80/");
			Assert.AreEqual("https:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("80", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("https://foo:80/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest165()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftp://foo:21/");
			Assert.AreEqual("ftp:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftp://foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest166()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftp://foo:80/");
			Assert.AreEqual("ftp:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("80", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftp://foo:80/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest167()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"gopher://foo:70/");
			Assert.AreEqual("gopher:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("gopher://foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest168()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"gopher://foo:443/");
			Assert.AreEqual("gopher:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("443", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("gopher://foo:443/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest169()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws://foo:80/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest170()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws://foo:81/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("81", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://foo:81/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest171()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws://foo:443/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("443", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://foo:443/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest172()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws://foo:815/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("815", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://foo:815/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest173()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss://foo:80/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("80", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://foo:80/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest174()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss://foo:81/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("81", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://foo:81/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest175()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss://foo:443/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://foo/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest176()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss://foo:815/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("foo", anchor.HostName);
			Assert.AreEqual("815", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://foo:815/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest177()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:/example.com/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest178()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftp:/example.com/");
			Assert.AreEqual("ftp:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftp://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest179()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"https:/example.com/");
			Assert.AreEqual("https:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("https://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest180()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"madeupscheme:/example.com/");
			Assert.AreEqual("madeupscheme:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("madeupscheme:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest181()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"file:/example.com/");
			Assert.AreEqual("file:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("file:///example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest182()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftps:/example.com/");
			Assert.AreEqual("ftps:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftps:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest183()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"gopher:/example.com/");
			Assert.AreEqual("gopher:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("gopher://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest184()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws:/example.com/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest185()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss:/example.com/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest186()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"data:/example.com/");
			Assert.AreEqual("data:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("data:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest187()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"javascript:/example.com/");
			Assert.AreEqual("javascript:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("javascript:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest188()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"mailto:/example.com/");
			Assert.AreEqual("mailto:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("mailto:/example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest189()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:example.com/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest190()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftp:example.com/");
			Assert.AreEqual("ftp:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftp://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest191()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"https:example.com/");
			Assert.AreEqual("https:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("https://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest192()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"madeupscheme:example.com/");
			Assert.AreEqual("madeupscheme:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("madeupscheme:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest193()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ftps:example.com/");
			Assert.AreEqual("ftps:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ftps:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest194()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"gopher:example.com/");
			Assert.AreEqual("gopher:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("gopher://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest195()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"ws:example.com/");
			Assert.AreEqual("ws:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("ws://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest196()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"wss:example.com/");
			Assert.AreEqual("wss:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("wss://example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest197()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"data:example.com/");
			Assert.AreEqual("data:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("data:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest198()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"javascript:example.com/");
			Assert.AreEqual("javascript:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("javascript:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlTest199()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"mailto:example.com/");
			Assert.AreEqual("mailto:", anchor.Protocol);
			Assert.AreEqual("", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("example.com/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("mailto:example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldDropEverythingBeforeTheAtSignAndIgnoreMissingSlashes()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldDropEverythingBeforeTheAtSignAndIgnoreMissingSlash()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:/@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldDropPartBeforeTheAtSign()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldAddMissingSlashesToPasswordUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:a:b@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:b@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldAddMissingSlashToPasswordUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:/a:b@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:b@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandleUserAndPasswordInUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://a:b@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:b@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldDropIllegalPartUntilAtLetter()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://@pple.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("pple.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://pple.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandlePasswordProtectedUrlWithoutSlashes()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http::b@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://:b@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandlePasswordProtectedUrlWithSingleSlash()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:/:b@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://:b@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandlePasswordProtectedUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://:b@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://:b@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldIntegrateTheMissingSlashes()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:a:@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldIntegrateTheSecondSlash()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http:/a:@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldChangeToTheUsernameAndPasswordUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://a:@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://a:@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldSplitAtTheAtLetter()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www.@pple.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("pple.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.@pple.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldChangeTheWholeUrlWithUsernameAndPassword()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"about:blank";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://:@www.example.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://:@www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldSetTheAbsolutePath()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldUseTheAbsolutePath()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"/test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldLeaveTheCurrentPathUnmodified()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @".");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldGoOneDirectoryUp()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"..");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldUseCurrentDirectory()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldStayInCurrentDirectory()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"./test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldBeAbleToGoOneDirectorUp()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"../test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldBeAbleToGoOneDirectorUpAndDown()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"../aaa/test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/aaa/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/aaa/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldBeAbleToGoTwoDirectoriesUp()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"../../test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldTransformJapaneseLetter()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"中/test.txt");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/%E4%B8%AD/test.txt", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example.com/%E4%B8%AD/test.txt", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandleValidAbsoluteUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www.example2.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example2.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example2.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandleSchemeRelativeUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://www.example.com/test";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"//www.example2.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.example2.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.example2.com/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldLowerCase()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://ExAmPlE.CoM");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://example.com/", anchor.Href);
		}

        [Test]
        public void DocumentUrlShouldDropForbiddenHiddenSpace()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://GOO​⁠﻿goo.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("googoo.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://googoo.com/", anchor.Href);
		}

        [Test]
        public void DocumentUrlShouldTransformBigDot()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://www.foo。bar.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("www.foo.bar.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://www.foo.bar.com/", anchor.Href);
		}

        [Test]
        public void DocumentUrlShouldTransformAlternativeGAndOLetters()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://Ｇｏ.com");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("go.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://go.com/", anchor.Href);
		}

        //TODO [Test]
        public void DocumentUrlTest253()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://你好你好");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("xn--6qqa088eba", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://xn--6qqa088eba/", anchor.Href);
		}

        [Test]
        public void DocumentUrlShouldCorrectlyTransformPercentEncodedHostname()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://%30%78%63%30%2e%30%32%35%30.01");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("0xc0.0250.01", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://0xc0.0250.01/", anchor.Href);
		}

        [Test]
        public void DocumentUrlShouldTransformPercentUrl()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://%30%78%63%30%2e%30%32%35%30.01%2e");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("0xc0.0250.01.", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://0xc0.0250.01./", anchor.Href);
		}

        [Test]
        public void DocumentUrlShouldConvertSpecialNumbers()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://０Ｘｃ０．０２５０．０１");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("0xc0.0250.01", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
			Assert.AreEqual("http://0xc0.0250.01/", anchor.Href);
		}
	
		[Test]
		public void DocumentUrlShouldHandleUnicodeInPassword()
		{
			var document = Html("<base id=base>");
			var element = document.GetElementById("base") as HtmlBaseElement;
			Assert.IsNotNull(element);
			element.Href = @"http://other.com/";
			var anchor = document.CreateElement<IHtmlAnchorElement>();
			anchor.SetAttribute("href", @"http://foo:💩@example.com/bar");
			Assert.AreEqual("http:", anchor.Protocol);
			Assert.AreEqual("example.com", anchor.HostName);
			Assert.AreEqual("", anchor.Port);
			Assert.AreEqual("/bar", anchor.PathName);
			Assert.AreEqual("", anchor.Search);
			Assert.AreEqual("", anchor.Hash);
            Assert.AreEqual("http://foo:%F0%9F%92%A9@example.com/bar", anchor.Href);
		}
	}
}