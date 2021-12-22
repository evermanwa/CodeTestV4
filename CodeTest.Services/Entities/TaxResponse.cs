using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeTest.Services.dll
{
    public class TaxResponse : ResponseBase
    {
        [JsonProperty("tax")]
        public TaxResponseData TaxData { get; set; }

        public TaxResponse()
        {
            TaxData = new TaxResponseData();
        }
    }

    public class TaxResponseData
    {
        public TaxResponseData()
        {
            OrderTotalAmount = 0.0;
            Shipping = 0.0;
            TaxableAmount = 0.0;
            AmountToCollect = 0.0;
            Rate = 0.0;
            HasNexus = false;//could change this to something else if needed
            FreightTaxable = false;
            TaxSource = string.Empty;
            ExemptionType = string.Empty;
            Jurisdictions = new Jurisdictions();
            TaxBreakdown = new Breakdown();
        }

        [JsonProperty("order_total_amount")]
        public double OrderTotalAmount { get; set; }

        [JsonProperty("shipping")]
        public double Shipping { get; set; }

        [JsonProperty("taxable_amount")]
        public double TaxableAmount { get; set; }

        [JsonProperty("amount_to_collect")]
        public double AmountToCollect { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("tax_source")]
        public string TaxSource { get; set; }

        [JsonProperty("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonProperty("jurisdictions")]
        public Jurisdictions Jurisdictions { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown TaxBreakdown { get; set; }
    }

    public class Jurisdictions
    {
        public Jurisdictions()
        {
            Country = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            City = string.Empty;
        }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }

    public class Breakdown
    {
        public Breakdown()
        {
            TaxableAmount = 0.0;
            TaxCollectable = 0.0;
            CombinedTaxRate = 0.0;
            StateTaxableAmount = 0.0;
            StateTaxRate = 0.0;
            StateTaxCollectable = 0.0;
            CountyTaxableAmount = 0.0;
            CountyTaxRate = 0.0;
            CountyTaxCollectable = 0.0;
            CityTaxableAmount = 0.0;
            CityTaxRate = 0.0;
            CityTaxCollectable = 0.0;
            SpecialDistrictTaxableAmount = 0.0;
            SpecialTaxRate = 0.0;
            SpecialDistrictTaxCollectable = 0.0;
            LineItems = new List<LineItem>();
        }

        [JsonProperty("taxable_amount")]
        public double TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonProperty("state_tax_rate")]
        public double StateTaxRate { get; set; }

        [JsonProperty("state_tax_collectable")]
        public double StateTaxCollectable { get; set; }

        [JsonProperty("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonProperty("county_tax_collectable")]
        public double CountyTaxCollectable { get; set; }

        [JsonProperty("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonProperty("city_tax_collectable")]
        public double CityTaxCollectable { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public double SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonProperty("special_district_tax_collectable")]
        public double SpecialDistrictTaxCollectable { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }
    }

    public class LineItem
    {
        public LineItem()
        {
            Id = string.Empty;
            TaxableAmount = 0.0;
            TaxCollectable = 0.0;
            CombinedTaxRate = 0.0;
            StateTaxableAmount = 0.0;
            StateSalesTaxRate = 0.0;
            StateAmount = 0.0;
            CountyTaxableAmount = 0.0;
            CountyTaxRate = 0.0;
            CountyAmount = 0.0;
            CityTaxableAmount = 0.0;
            CityTaxRate = 0.0;
            CityAmount = 0.0;
            SpecialDistrictTaxableAmount = 0.0;
            SpecialTaxRate = 0.0;
            SpecialDistrictAmount = 0.0;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("taxable_amount")]
        public double TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonProperty("state_sales_tax_rate")]
        public double StateSalesTaxRate { get; set; }

        [JsonProperty("state_amount")]
        public double StateAmount { get; set; }

        [JsonProperty("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonProperty("county_amount")]
        public double CountyAmount { get; set; }

        [JsonProperty("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonProperty("city_amount")]
        public double CityAmount { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public double SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonProperty("special_district_amount")]
        public double SpecialDistrictAmount { get; set; }
    }
}