namespace AngleSharp.Core.Tests.External
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    static class MemoryStreamExtensions
    {
        public static Int32 ReadInt(this MemoryStream ms)
        {
            var buffer = new Byte[4];
            ms.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static String ReadString(this MemoryStream ms)
        {
            var length = ReadInt(ms);
            var buffer = new Byte[length];
            ms.Read(buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }

        public static Dictionary<String, String> ReadDictionary(this MemoryStream ms)
        {
            var dictionary = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            var headers = ms.ReadInt();

            for (int i = 0; i < headers; i++)
            {
                var key = ms.ReadString();
                var val = ms.ReadString();
                dictionary.Add(key, val);
            }

            return dictionary;
        }

        public static void Write(this MemoryStream ms, Int32 value)
        {
            ms.Write(BitConverter.GetBytes(value));
        }

        public static void Write(this MemoryStream ms, Byte[] buffer)
        {
            ms.Write(buffer, 0, buffer.Length);
        }

        public static void Write(this MemoryStream ms, String content)
        {
            var buffer = Encoding.UTF8.GetBytes(content);
            Write(ms, BitConverter.GetBytes(buffer.Length));
            Write(ms, buffer);
        }

        public static void Write(this MemoryStream ms, IDictionary<String, String> dict)
        {
            ms.Write(dict.Count);

            foreach (var pair in dict)
            {
                ms.Write(pair.Key);
                ms.Write(pair.Value);
            }
        }
    }
}
