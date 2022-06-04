using Newtonsoft.Json;

namespace Domain.Helpers
{
    public class Error
    {
        public Error()
        {
            RequestDateTime = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        public Error(string code, string title, string? detail)
        {
            Code = code;
            Title = title;
            Detail = !string.IsNullOrEmpty(detail) ? detail : "";
            RequestDateTime = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }



        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Detail { get; set; }

        public string? RequestDateTime { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

