using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TMD.WebBase.WebApi
{
    /// <summary>
    /// Json.NET formatter
    /// </summary>
    public class JsonNetFormatter : MediaTypeFormatter
    {
        #region Private

        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        private readonly Encoding encoding = Encoding.UTF8;
 
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonNetFormatter(JsonSerializerSettings jsonSerializerSettings)
        {
            // Fill out the mediatype and encoding we support
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        #endregion

        #region Public

        /// <summary>
        /// Override
        /// </summary>
        public override bool CanReadType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Overrride
        /// </summary>
        public override bool CanWriteType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Override
        /// </summary>
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            // Create a serializer
            JsonSerializer serializer = JsonSerializer.Create(jsonSerializerSettings);

            // Create task reading the content
            return Task.Factory.StartNew(() =>
            {
                using (StreamReader streamReader = new StreamReader(readStream, encoding))
                {
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                    {
                        return serializer.Deserialize(jsonTextReader, type);
                    }
                }
            });
        }

        /// <summary>
        /// Override
        /// </summary>
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            // Create a serializer
            JsonSerializer serializer = JsonSerializer.Create(jsonSerializerSettings);

            // Create task writing the serialized content
            return Task.Factory.StartNew(() =>
            {
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(writeStream, encoding)) { CloseOutput = false })
                {
                    serializer.Serialize(jsonTextWriter, value);
                    jsonTextWriter.Flush();
                }
            });
        }

        #endregion
    }
}
