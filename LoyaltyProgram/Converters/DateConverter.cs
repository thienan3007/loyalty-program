using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoyaltyProgram.Converters
{
    public class DateConverter : JsonConverter<DateTime>
    {
        private string formatString = "yyyy/MM/dd";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            return DateTime.ParseExact(s, formatString, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatString));
        }
    }
}
