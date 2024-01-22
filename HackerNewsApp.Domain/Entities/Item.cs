using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackerNewsApp.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public bool? Deleted { get; set; }
        public string Type { get; set; }
        public string By { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset? Time {  get; set; }
        public string Text { get; set; }
        public bool? Dead { get; set; }
        public int? Parent { get; set; }
        public int? Poll { get; set; }
        public int[]? Kids { get; set; }
        public string Url { get; set; }
        public int? Score { get; set; }
        public string Title { get; set; }
        public int[]? Parts { get; set; }
        public int? Descendants { get; set; }
    }

    public class UnixDateTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException($"Unexpected token type: {reader.TokenType}");
            }

            var unixTime = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeSeconds(unixTime);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(new DateTimeOffset(value.UtcDateTime).ToUnixTimeSeconds());
        }
    }
}
