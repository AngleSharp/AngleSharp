namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.Globalization;
    using System.Linq;
    using AngleSharp;
    using AngleSharp.Dom.Html;
    using NUnit.Framework;

    [TestFixture]
    public class DocumentEncodingTests
    {
        [Test]
        public void BaiduEncodingTransformationEnToUtf8VsZhToUtf8()
        {
            var configEnUs = Configuration.Default.SetCulture("en-US").WithLocaleBasedEncoding();
            var configZhCn = Configuration.Default.SetCulture("zh-CN").WithLocaleBasedEncoding();
            var titleWithEnUs = Helper.StreamFromBytes(Assets.www_baidu).ToHtmlDocument(configEnUs).Title;
            var titleWithZhCn = Helper.StreamFromBytes(Assets.www_baidu).ToHtmlDocument(configZhCn).Title;

            Assert.AreEqual("百度一下，你就知道", titleWithEnUs);
            Assert.AreEqual(titleWithEnUs, titleWithZhCn);
        }

        [Test]
        public void JdEncodingDisplayCharacters()
        {
            var configEnUs = Configuration.Default.SetCulture("en-US").WithLocaleBasedEncoding();
            var configZhCn = Configuration.Default.SetCulture("zh-CN").WithLocaleBasedEncoding();
            var titleWithEnUs = Helper.StreamFromBytes(Assets.item_jd).ToHtmlDocument(configEnUs).Title;
            var titleWithZhCn = Helper.StreamFromBytes(Assets.item_jd).ToHtmlDocument(configZhCn).Title;

            Assert.AreEqual("《金庸作品集（21-25）：天龙八部（套装全5册）（朗声旧版）》(金庸)【摘要 书评 试读】- 京东图书", titleWithEnUs);
            Assert.AreEqual(titleWithEnUs, titleWithZhCn);
        }

        [Test]
        public void TradeEncodingDisplayCharactersFromWindows1252()
        {
            var config = Configuration.Default.SetCulture("de-de").WithLocaleBasedEncoding();
            var doc = Helper.StreamFromBytes(Assets.trade_500).ToHtmlDocument(config);
            var tr = doc.QuerySelector("tr[mid=375][ordernum=375]");
            var ct = doc.QuerySelector(".countdown_time[title][style]");
            var ni = doc.QuerySelector("*[fid=443861][homesxname]");

            Assert.AreEqual("【单场胜负过关】投注|合买|代购_体彩单场胜负过关游戏_500彩票网", doc.Title);
            Assert.AreEqual("足球", tr.GetAttribute("matchtype"));
            Assert.AreEqual("足球-欧罗巴", tr.GetAttribute("lg"));
            Assert.AreEqual("布鲁日", tr.GetAttribute("homesxname"));
            Assert.AreEqual("剩余时间", ct.GetAttribute("title"));
            Assert.AreEqual("克卢日", ni.GetAttribute("homesxname"));
        }

        [Test]
        public void TradeEncodingVariousChecks()
        {
            var source = Helper.StreamFromBytes(Assets.trade_500);
            var document = source.ToHtmlDocument();

            var bet_content = document.GetElementById("bet_content");
            Assert.IsNotNull(bet_content);
            var el = bet_content.GetElementsByTagName("table").Where(x => x.ClassName.Contains("bet_table"));

            foreach (var e in el)
            {
                var t = (IHtmlTableElement)e;

                foreach (var tr in t.Rows)
                {
                    var awaysxname = tr.GetAttribute("awaysxname");
                    var homesxname = tr.GetAttribute("homesxname");

                    Assert.IsFalse(String.IsNullOrEmpty(homesxname), "Home team name should exist.");
                    Assert.IsFalse(homesxname.Contains("?"), "Invalid home team name: " + homesxname);
                    Assert.IsFalse(String.IsNullOrEmpty(awaysxname), "Away team name should exist.");
                    Assert.IsFalse(awaysxname.Contains("?"), "Invalid away team name: " + awaysxname);
                }
            }
        }

        [Test]
        public void EncodingCheckUtf8TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.utf_8);
            var doc = source.ToHtmlDocument();
            var text = "  ¥ · £ · € · $ · ¢ · ₡ · ₢ · ₣ · ₤ · ₥ · ₦ · ₧ · ₨ · ₩ · ₪ · ₫ · ₭ · ₮ · ₯ · ₹";
            Assert.AreEqual(text, doc.QuerySelector("big > big").TextContent);
        }

        [Test]
        public void EncodingCheckWindows1252TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.windows_1252);
            var doc = source.ToHtmlDocument();
            var text = "“Hello Double” vs.  ‘Hello single’ vs. it’s";
            Assert.AreEqual(text, doc.QuerySelectorAll("tr > td")[7].TextContent);
        }

        [Test]
        public void EncodingCheckWindows1251TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.windows_1251);
            var doc = source.ToHtmlDocument();
            var text = "Преимущества";
            Assert.AreEqual(text, doc.QuerySelector("h2").TextContent);
        }

        [Test]
        public void EncodingCheckBomSaysUTF8ButMetaSaysShiftJisTestPage()
        {
            var source = Helper.StreamFromBytes(Assets.shift_jis);
            var doc = source.ToHtmlDocument();
            var text = "九州大学言語学研究室";
            Assert.AreEqual(text, doc.QuerySelector("h1").TextContent);
        }

        [Test]
        public void EncodingCheckRealShiftJisTestPage()
        {
            var source = Helper.StreamFromBytes(Assets.real_shift_jit);
            var doc = source.ToHtmlDocument();
            var title = "２ちゃんねる掲示板へようこそ";
            Assert.AreEqual(title, doc.Title);
            var text = "検索";
            Assert.AreEqual(text, doc.QuerySelector(".form_button").GetAttribute("alt"));
        }

        [Test]
        public void EncodingCheckGb2312TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.gb2312);
            var doc = source.ToHtmlDocument();
            var text = "汉字";
            Assert.AreEqual(text, doc.QuerySelector("td > span").TextContent);
        }

        [Test]
        public void EncodingCheckGb18030TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.gb18030);
            var doc = source.ToHtmlDocument();
            var text = "hello，欢迎回来！";
            Assert.AreEqual(text, doc.QuerySelector(".pptitle > b").TextContent);
        }

        [Test]
        public void EncodingCheckBig5TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.big5);
            var doc = source.ToHtmlDocument();
            var text = "歷史新聞";
            Assert.AreEqual(text, doc.QuerySelector("#area_sort_3 a").TextContent);
        }

        [Test]
        public void EncodingCheckIso88591TestPage()
        {
            var source = Helper.StreamFromBytes(Assets.iso_8859_1);
            var doc = source.ToHtmlDocument();
            var text = "Apri un blog è gratis";
            Assert.AreEqual(text, doc.QuerySelectorAll(".label")[3].TextContent);
        }
    }
}
