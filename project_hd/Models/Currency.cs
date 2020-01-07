using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace project_hd.Models
{
    [Serializable]
    public class Currency
    {
        public Currency() { }

        public static Currency CreateNew(dynamic obj)
        {
            var cur = new Currency();
            cur.Bpi = obj.bpi;
            cur.Disclaimer = obj.disclaimer;
            cur.Time = obj.time;

            return cur;
        }

        [BsonId]
        public string CurrencyId { get; set; }

        [JsonProperty]
        public dynamic Time { get; set; }
        [JsonProperty]
        public string Disclaimer { get; set; }
        [JsonProperty]
        public dynamic Bpi { get; set; }
    }
}
