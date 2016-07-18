namespace AngleSharp.Extensions
{
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.IO;
    using System.Text;

    static class FormDataSetExtensions
    {
        public static Stream CreateBody(this FormDataSet formDataSet, String enctype, String charset)
        {
            var encoding = TextEncoding.Resolve(charset);
            return formDataSet.CreateBody(enctype, encoding);
        }

        public static Stream CreateBody(this FormDataSet formDataSet, String enctype, Encoding encoding)
        {
            if (enctype.Isi(MimeTypeNames.UrlencodedForm))
            {
                return formDataSet.AsUrlEncoded(encoding);
            }
            else if (enctype.Isi(MimeTypeNames.MultipartForm))
            {
                return formDataSet.AsMultipart(encoding);
            }
            else if (enctype.Isi(MimeTypeNames.Plain))
            {
                return formDataSet.AsPlaintext(encoding);
            }
            else if (enctype.Isi(MimeTypeNames.ApplicationJson))
            {
                return formDataSet.AsJson();
            }

            return MemoryStream.Null;
        }
    }
}
