using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeTest.Services.dll
{
    public class TaxRequest : RequestBase
    {
        public TaxRequest()
        {
            FromCountry = string.Empty;
            FromZip = string.Empty;
            FromState = string.Empty;
            FromCity = string.Empty;
            FromStreet = string.Empty;
            ToCountry = string.Empty;
            ToZip = string.Empty;
            ToState = string.Empty;
            ToCity = string.Empty;
            ToStreet = string.Empty;
            Amount = 0.0;
            Shipping = 0.0;
            CustomerId = string.Empty;
            ExemptionType = "non_exempt";//after testing...this HAS to be populated with something. Wasn't stated in their api notes.
            NexusAddresses = new List<NexAddress>();
            LineItems = new List<TaxLineItem>();
        }

        [JsonProperty("from_country")]
        public string FromCountry { get; set; }

        [JsonProperty("from_zip")]
        public string FromZip { get; set; }

        [JsonProperty("from_state")]
        public string FromState { get; set; }

        [JsonProperty("from_city")]
        public string FromCity { get; set; }

        [JsonProperty("from_street")]
        public string FromStreet { get; set; }

        [JsonProperty("to_country")]
        public string ToCountry { get; set; }

        [JsonProperty("to_zip")]
        public string ToZip { get; set; }

        [JsonProperty("to_state")]
        public string ToState { get; set; }

        [JsonProperty("to_city")]
        public string ToCity { get; set; }

        [JsonProperty("to_street")]
        public string ToStreet { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("shipping")]
        public double Shipping { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonProperty("nexus_addresses")]
        public List<NexAddress> NexusAddresses { get; set; }

        [JsonProperty("line_items")]
        public List<TaxLineItem> LineItems { get; set; }
    }

    public class NexAddress
    {
        public NexAddress()
        {
            Id = string.Empty;
            Country = string.Empty;
            Zip = string.Empty;
            State = string.Empty;
            City = string.Empty;
            Street = string.Empty;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }

    public class TaxLineItem
    {
        public TaxLineItem()
        {
            Id = string.Empty;
            Quantity = 0;
            ProductTaxCode = string.Empty;
            UnitPrice = 0.0;
            Discount = 0.0;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("product_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonProperty("unit_price")]
        public double UnitPrice { get; set; }

        [JsonProperty("discount")]
        public double Discount { get; set; }
    }
}
