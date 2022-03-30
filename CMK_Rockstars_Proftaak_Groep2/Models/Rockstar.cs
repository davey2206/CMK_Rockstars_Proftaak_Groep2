using System;

namespace CMK_Rockstars_Proftaak_Groep2.Models
{
    public class Rockstar
    {
        [Newtonsoft.Json.JsonProperty("Id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty("Image", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Image { get; set; }
        public string Description { get; set; }
        [Newtonsoft.Json.JsonProperty("Role", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Role { get; set; }
    }
}