using Newtonsoft.Json;

namespace Neofilia.BOT.Helpers
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("questioncsvpath")]
        public string QuestionCsvPath { get; private set; }
        [JsonProperty("couponcsvpath")]
        public string CouponCsvPath { get; private set; }
        [JsonProperty("barcsvpath")]
        public string BarCsvPath { get; private set; }
    }
}
