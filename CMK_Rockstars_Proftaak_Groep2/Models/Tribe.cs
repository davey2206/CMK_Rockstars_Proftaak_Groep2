using System;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Tribe
    {
        [Newtonsoft.Json.JsonProperty("Id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string id { get; set; }
        public string displayName { get; set;}
        public string description { get; set;}
    }
}