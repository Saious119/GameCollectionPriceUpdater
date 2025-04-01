using CsvHelper.Configuration.Attributes;

namespace NotionGamePricingUpdater.Models;

public class notionCSV
{
    [Name("own")]
    public string own { get; set; }
    [Name("game-name")]
    public string gameName { get; set; }
    [Name("loose-price")]
    public string loosePrice { get; set; }
}