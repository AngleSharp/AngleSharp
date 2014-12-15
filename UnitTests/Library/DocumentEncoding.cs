namespace UnitTests.Library
{
    using AngleSharp;
    using NUnit.Framework;
    using System.Globalization;

    [TestFixture]
    public class DocumentEncodingTests
    {
        [Test]
        public void BaiduEncodingTransformationEnToUtf8VsZhToUtf8()
        {
            var configEnUs = new Configuration { Culture = new CultureInfo("en-US") };
            var configZhCn = new Configuration { Culture = new CultureInfo("zh-CN") };
            var titleWithEnUs = DocumentBuilder.Html(Helper.StreamFromBytes(Assets.www_baidu), configEnUs).Title;
            var titleWithZhCn = DocumentBuilder.Html(Helper.StreamFromBytes(Assets.www_baidu), configZhCn).Title;

            Assert.AreEqual("百度一下，你就知道", titleWithEnUs);
            Assert.AreEqual(titleWithEnUs, titleWithZhCn);
        }

        [Test]
        public void JdEncodingDisplayCharacters()
        {
            var doc = DocumentBuilder.Html(Helper.StreamFromBytes(Assets.item_jd));

            Assert.AreEqual("《金庸作品集（21-25）：天龙八部（套装全5册）（朗声旧版）》(金庸)【摘要 书评 试读】- 京东图书", doc.Title);
        }

        [Test]
        public void TradeEncodingDisplayCharactersFromUtf8()
        {
            var config = new Configuration { Culture = CultureInfo.GetCultureInfo("ar") };
            TradeEncodingDisplayCharactersFrom(config);
        }

        [Test]
        public void TradeEncodingDisplayCharactersFromWindows1252()
        {
            var config = new Configuration { Culture = CultureInfo.GetCultureInfo("de-de") };
            TradeEncodingDisplayCharactersFrom(config);
        }

        [Test]
        public void TradeEncodingDisplayCharactersFromLatin5()
        {
            var config = new Configuration { Culture = CultureInfo.GetCultureInfo("be") };
            TradeEncodingDisplayCharactersFrom(config);
        }

        [Test]
        public void TradeEncodingDisplayCharactersFromGB18030()
        {
            var config = new Configuration { Culture = CultureInfo.GetCultureInfo("zh-cn") };
            TradeEncodingDisplayCharactersFrom(config);
        }

        [Test]
        public void TradeEncodingDisplayCharactersFromBig5()
        {
            var config = new Configuration { Culture = CultureInfo.GetCultureInfo("zh-tw") };
            TradeEncodingDisplayCharactersFrom(config);
        }

        static void TradeEncodingDisplayCharactersFrom(Configuration configuration)
        {
            var doc = DocumentBuilder.Html(Helper.StreamFromBytes(Assets.trade_500), configuration);
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
    }
}
