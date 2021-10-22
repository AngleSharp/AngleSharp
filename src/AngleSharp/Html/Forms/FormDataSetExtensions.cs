namespace AngleSharp.Html.Forms
{
    using AngleSharp.Html.Forms.Submitters;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Text;

    static class FormDataSetExtensions
    {
        public static Stream CreateBody(this FormDataSet formDataSet, String enctype, String? charset, IHtmlEncoder? htmlEncoder)
        {
            var encoding = TextEncoding.Resolve(charset);
            return formDataSet.CreateBody(enctype, encoding, htmlEncoder ?? new DefaultHtmlEncoder());
        }

        public static Stream CreateBody(this FormDataSet formDataSet, String enctype, Encoding encoding, IHtmlEncoder htmlEncoder)
        {
            if (enctype.Isi(MimeTypeNames.UrlencodedForm))
            {
                return formDataSet.AsUrlEncoded(encoding);
            }
            else if (enctype.Isi(MimeTypeNames.MultipartForm))
            {
                return formDataSet.AsMultipart(htmlEncoder, encoding);
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
