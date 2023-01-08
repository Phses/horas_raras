using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorasRaras.Domain.Entities
{
    public partial class TogglTaskEntity
    {
        [JsonProperty("total_grand", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalGrand { get; set; }

        [JsonProperty("total_billable", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalBillable { get; set; }

        [JsonProperty("total_currencies", NullValueHandling = NullValueHandling.Ignore)]
        public TotalCurrency[] TotalCurrencies { get; set; }

        [JsonProperty("total_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalCount { get; set; }

        [JsonProperty("per_page", NullValueHandling = NullValueHandling.Ignore)]
        public long? PerPage { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Datum[] Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("pid", NullValueHandling = NullValueHandling.Ignore)]
        public long? Pid { get; set; }

        [JsonProperty("tid")]
        public object Tid { get; set; }

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public long? Uid { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Start { get; set; }

        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? End { get; set; }

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Updated { get; set; }

        [JsonProperty("dur", NullValueHandling = NullValueHandling.Ignore)]
        public long? Dur { get; set; }

        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }

        [JsonProperty("use_stop", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UseStop { get; set; }

        [JsonProperty("client", NullValueHandling = NullValueHandling.Ignore)]
        public string Client { get; set; }

        [JsonProperty("project", NullValueHandling = NullValueHandling.Ignore)]
        public string Project { get; set; }

        [JsonProperty("project_color", NullValueHandling = NullValueHandling.Ignore)]
        public long? ProjectColor { get; set; }

        [JsonProperty("project_hex_color", NullValueHandling = NullValueHandling.Ignore)]
        public string ProjectHexColor { get; set; }

        [JsonProperty("task")]
        public object Task { get; set; }

        [JsonProperty("billable", NullValueHandling = NullValueHandling.Ignore)]
        public long? Billable { get; set; }

        [JsonProperty("is_billable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsBillable { get; set; }

        [JsonProperty("cur", NullValueHandling = NullValueHandling.Ignore)]
        public string Cur { get; set; }

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Tags { get; set; }
    }

    public partial class TotalCurrency
    {
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public long? Amount { get; set; }
    }
}


