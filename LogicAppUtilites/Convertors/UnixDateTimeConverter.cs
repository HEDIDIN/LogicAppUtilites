using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LogicAppUtilites.Convertors
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime UnixStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                var totalSeconds = (long) ((DateTime) value - UnixStartTime).TotalSeconds;
                writer.WriteValue(totalSeconds);
            }
            else
            {
                throw new ArgumentException(nameof(value));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception("Invalid token. Expected integer");
            }

            double totalSeconds;

            try
            {
                totalSeconds = Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new Exception("Invalid double value.");
            }

            return UnixStartTime.AddSeconds(totalSeconds);
        }
    }
}