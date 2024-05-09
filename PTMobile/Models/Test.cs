using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    public class Test
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Image")]
        public Uri Image { get; set; }

    }
}
