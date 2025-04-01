using CsvHelper.Configuration.Attributes;

namespace NotionGamePricingUpdater.Models;

public class priceCharting
{
    [Name("id")]
    public string id { get; set; }
    [Name("console-name")]
    public string consoleName { get; set; }
    [Name("product-name")]
    public string productName { get; set; }
    [Name("loose-price")]
    public string loosePrice { get; set; }
}