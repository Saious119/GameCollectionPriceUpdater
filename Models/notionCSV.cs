using CsvHelper.Configuration.Attributes;

namespace NotionGamePricingUpdater.Models;

public class notionCSV
{
    public string own { get; set; }
    public string gameName { get; set; }
    public string loosePrice { get; set; }

    public notionCSV(string _own, string _gameName, string _loosePrice)
    {
        own = _own;
        gameName = _gameName;
        loosePrice = _loosePrice;
    }
}