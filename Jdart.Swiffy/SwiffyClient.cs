using Jdart.Swiffy.Helpers;
using Jdart.Swiffy.RequestModels;
using Jdart.Swiffy.ResponseModels;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Jdart.Swiffy
{
    public class SwiffyClient
    {
        private const string GOOGLE_API_URL = "https://www.googleapis.com/rpc?key=AIzaSyCC_WIu0oVvLtQGzv4-g7oaWNoc-u8JpEI";

        public SwiffyOptions Options { get; }

        private readonly Lazy<DataContractJsonSerializer> _requestSerializer = new Lazy<DataContractJsonSerializer>(() => new DataContractJsonSerializer(typeof(GoogleApiRequest)));
        private readonly Lazy<DataContractJsonSerializer> _responseSerializer = new Lazy<DataContractJsonSerializer>(() => new DataContractJsonSerializer(typeof(GoogleApiResponse)));

        public SwiffyClient(SwiffyOptions options = null)
        {
            Options = options ?? new SwiffyOptions
            {
                MillisecondsTimeout = 5000
            };
        }

        public string ConvertToHtml5(byte[] swf)
        {
            return ConvertToHtml5Async(swf).Result;
        }

        public async Task<string> ConvertToHtml5Async(byte[] swf)
        {
            if (swf == null) throw new ArgumentNullException(nameof(swf));

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Host", "www.googleapis.com");
                httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                httpClient.Timeout = TimeSpan.FromMilliseconds(Options.MillisecondsTimeout);

                var base64Data = Convert.ToBase64String(swf)
                                        .Replace('+', '-')
                                        .Replace('/', '_');

                var requestModel = new GoogleApiRequest
                {
                    ApiVersion = "v1",
                    Method = "swiffy.convertToHtml",
                    Params = new SwiffyParams
                    {
                        Client = "Swiffy Flash Extension",
                        Input = base64Data
                    }
                };

                using (var streamContent = new PushStreamContent(stream => Task.Run(() => _requestSerializer.Value.WriteObject(stream, requestModel))))
                {
                    using (var response = await httpClient.PostAsync(GOOGLE_API_URL, streamContent).ConfigureAwait(false))
                    {
                        var responseJson = (GoogleApiResponse)_responseSerializer.Value.ReadObject(await response.Content.ReadAsStreamAsync().ConfigureAwait(false));

                        if(responseJson.Error != null)
                        {
                            throw new Exception($"Conversion error. {responseJson.Error.Message}.");
                        }

                        if (responseJson.Result?.Response?.Status == "SUCCESS")
                        {
                            var base64 = responseJson.Result.Response.Output.Replace('-', '+')
                                                                            .Replace('_', '/');

                            base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4), '=');

                            var gzip = Convert.FromBase64String(base64);

                            using (var memoryStream = new MemoryStream(gzip))
                            {
                                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                                {
                                    using (var streamReader = new StreamReader(gzipStream))
                                    {
                                        return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception($"Status error. Статус: {responseJson.Result?.Response?.Status}");
                        }
                    }
                }
            }
        }
    }
}
