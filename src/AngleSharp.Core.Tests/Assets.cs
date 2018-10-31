using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.Core.Tests
{
    internal static class Assets
    {
        private static readonly Encoding Windows1252 = Encoding.GetEncoding("ISO-8859-1");

        public static string nbc => GetManifestResourceString("Pages.nbc.html", Encoding.UTF8);

        public static string selectors { get; } = GetManifestResourceString("Pages.selectors.html", Windows1252);

        public static string books => GetManifestResourceString("Pages.books.xml", Windows1252);

        public static string food => GetManifestResourceString("Pages.food.xml", Encoding.UTF8);

        public static string quirksmode => GetManifestResourceString("Pages.quirksmode.html", Windows1252);

        public static byte[] amazonenc => GetManifestResourceBytes("Resources.amazonenc_fr.bin");

        public static byte[] big5 => GetManifestResourceBytes("Resources.big5_com.bin");

        public static byte[] htmlcodetutorial => GetManifestResourceBytes("Resources.htmlcodetutorial_com.bin");

        public static byte[] IrishCentral => GetManifestResourceBytes("Resources.IrishCentral_com.bin");

        public static string w3c_selectors => GetManifestResourceString("Resources.w3cselectors_org.bin", Windows1252);

        public static byte[] gb18030 => GetManifestResourceBytes("Resources.gb18030_com.bin");

        public static byte[] gb2312 => GetManifestResourceBytes("Resources.gb2312_com.bin");

        public static byte[] iso_8859_1 => GetManifestResourceBytes("Resources.iso88591_it.bin");

        public static byte[] item_jd => GetManifestResourceBytes("Resources.item_jd_com.bin");

        public static byte[] real_shift_jit => GetManifestResourceBytes("Resources.realshiftjit_net.bin");

        public static byte[] shift_jis => GetManifestResourceBytes("Resources.shiftjis_jp.bin");

        public static byte[] trade_500 => GetManifestResourceBytes("Resources.trade500_com.bin");

        public static byte[] windows_1251 => GetManifestResourceBytes("Resources.windows1251_ru.bin");

        public static byte[] windows_1252 => GetManifestResourceBytes("Resources.windows1252_com.bin");

        public static byte[] www_baidu => GetManifestResourceBytes("Resources.wwwbaidu_com.bin");

        public static byte[] utf_8 => GetManifestResourceBytes("Resources.utf8_edu.bin");

        public static byte[] longscript => GetManifestResourceBytes("Resources.longscript.bin");

        public static string GetManifestResourceString(string name, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var fullName = typeof(Assets).Namespace + "." + name;
            using (var stream = typeof(Assets).Assembly.GetManifestResourceStream(fullName))
            {
                if (stream == null) throw new ArgumentException($"Unable to load Resource {fullName}. Check test project", nameof(name));
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static byte[] GetManifestResourceBytes(string name)
        {
            var fullName = typeof(Assets).Namespace + "." + name;
            using (var stream = typeof(Assets).Assembly.GetManifestResourceStream(fullName))
            {
                if (stream == null) throw new ArgumentException($"Unable to load Resource {fullName}. Check test project", nameof(name));
                using (var memStream = new MemoryStream())
                {
                    stream.CopyTo(memStream);
                    return memStream.ToArray();
                }
            }
        }
    }
}
