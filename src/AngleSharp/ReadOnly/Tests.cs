namespace AngleSharp.ReadOnly;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common;

public class Tests
{
    public class MemStrConv : JsonConverter<StringOrMemory>
    {
        public override StringOrMemory Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => new StringOrMemory(reader.GetString() ?? "");

        public override void Write(
            Utf8JsonWriter writer,
            StringOrMemory value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.Memory.Span);
    }

    public static void Run()
    {

    }
}