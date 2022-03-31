using System;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Tribe
    {
        [Newtonsoft.Json.JsonProperty("Id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string Name {get; set;}
    }
}