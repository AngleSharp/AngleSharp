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

            Assert.AreEqual(titleWithEnUs, titleWithZhCn);
        }

        [Test]
        public void JdEncodingDisplayCharacters()
        {
            var doc = DocumentBuilder.Html(Helper.StreamFromBytes(Assets.item_jd));

            Assert.AreEqual("《金庸作品集（21-25）：天龙八部（套装全5册）（朗声旧版）》(金庸)【摘要 书评 试读】- 京东图书", doc.Title);
        }

        [Test]
        public void TradeEncodingDisplayCharacters()
        {
            var doc = DocumentBuilder.Html(Helper.StreamFromBytes(Assets.trade_500));

            Assert.AreEqual("【单场胜负过关】投注|合买|代购_体彩单场胜负过关游戏_500彩票网", doc.Title);

            var tr = doc.QuerySelector("tr[mid=375][ordernum=375]");
            Assert.AreEqual("足球", tr.GetAttribute("matchtype"));
            Assert.AreEqual("足球-欧罗巴", tr.GetAttribute("lg"));
            Assert.AreEqual("布鲁日", tr.GetAttribute("homesxname"));
        }
    }
}
