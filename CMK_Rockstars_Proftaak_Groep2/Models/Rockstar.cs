using System;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Rockstar
    {
        [Newtonsoft.Json.JsonProperty("Id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string id { get; set; }
        public string displayName { get; set; }
        [Newtonsoft.Json.JsonProperty("Image", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Image { get; set; }
        public string Role { get; set; }
    }
}