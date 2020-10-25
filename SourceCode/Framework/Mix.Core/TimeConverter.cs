using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Mix.Core
{
    /// <summary>
    /// 时间戳 后台只返回 1562904163734
    /// </summary>
    public class TimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="JsonSerializationException">Expected date object value.</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double javaScriptTicks = 0;
            if (value is DateTime dateTime)
            {
                javaScriptTicks = ConvertDateTimeInt(dateTime);
            }
            else
            {
                if (!(value is DateTimeOffset dateTimeOffset))
                    throw new JsonSerializationException("Expected date object value.");
                javaScriptTicks = ConvertDateTimeInt(dateTimeOffset.ToUniversalTime().UtcDateTime);
            }
            writer.WriteValue(javaScriptTicks);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ConvertIntDateTime(double.Parse(reader.Value.ToString()));
        }

        /// <summary>
        /// Converts the int date time.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns></returns>
        public static DateTime ConvertIntDateTime(double milliseconds)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// 日期转换为时间戳（时间戳单位毫秒）
        /// </summary>
        /// <returns></returns>
        public static double ConvertDateTimeInt(DateTime dateTime)
        {
            if (dateTime.Year == 1)
            {
                return 0;
            }
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
            //return (dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}