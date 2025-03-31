using CsvHelper.Configuration.Attributes;

namespace NotionGamePricingUpdater.Models;

public class priceCharting
{
    public string id { get; set; }
    public string consoleName { get; set; }
    public string productName { get; set; }
    public string loosePrice { get; set; }
}