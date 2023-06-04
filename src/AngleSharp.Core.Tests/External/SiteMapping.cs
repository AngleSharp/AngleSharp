namespace AngleSharp.Core.Tests.External
{
    using System;
    using System.IO;
    using System.Xml;

    sealed class SiteMapping
    {
        private readonly String _fileName;
        private readonly XmlDocument _xml;

        public SiteMapping(String fileName)
        {
            _fileName = fileName;
            _xml = new XmlDocument();
            var content = File.Exists(fileName) ? File.ReadAllText(_fileName) : "<entries></entries>";
            _xml.LoadXml(content);
        }

        public String this[String url]
        {
            get
            {
                var element = _xml.SelectSingleNode($"//entry[@url='{url}']");
                return element != null ? element.InnerText : null;
            }
        }

        public Boolean Contains(String url)
        {
            return this[url] != null;
        }

        public void Add(String url, String fileName)
        {
            if (Contains(url) == false)
            {
                var element = _xml.CreateElement("entry");
                element.SetAttribute("url", url);
                element.InnerText = fileName;
                _xml.DocumentElement.AppendChild(element);

                using var writer = File.CreateText(_fileName);
                _xml.Save(writer);
            }
        }
    }
}
