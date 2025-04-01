using System.Globalization;
using CsvHelper;
using NotionGamePricingUpdater.Models;

namespace NotionGamePricingUpdater
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("What Console should we filter for?");
            var consoleName = Console.ReadLine();
            Console.WriteLine("Starting...");
            string priceChartingPath = "Z:\\Code\\GameCollectionPriceUpdater\\priceCharting.csv";
            string notionPath = "Z:\\Code\\GameCollectionPriceUpdater\\notion.csv";
            string outputPath = "Z:\\Code\\GameCollectionPriceUpdater\\output.csv";
            if (!File.Exists(priceChartingPath))
            {
                Console.WriteLine("Notion Game Pricing updater: price charting file not found.");
                return;
            }

            if (!File.Exists(notionPath))
            {
                Console.WriteLine("Notion Game Pricing updater: notion file not found.");
                return;
            }
            Console.WriteLine("Reading...");
            IEnumerable<priceCharting> priceChartings;
            IEnumerable<notionCSV> notions;
            using (var reader = new StreamReader(priceChartingPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                priceChartings = csv.GetRecords<priceCharting>().ToList();
            }

            using (var reader = new StreamReader(notionPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                notions = csv.GetRecords<notionCSV>().ToList();
            }
            Console.WriteLine("Both Files Read.");
            Console.WriteLine("Updating Prices and filtering data...");
            var priceChartingsOnConsole = priceChartings.Where(x => x.consoleName == consoleName).ToList();
            List<notionCSV> output = new();
            foreach (var rec in notions)
            {
                try
                {
                    var newNotionPrice = priceChartingsOnConsole.Where(x => x.productName == rec.gameName).FirstOrDefault();
                    Console.WriteLine(rec.gameName);
                    var newNotionObj = new notionCSV();
                    newNotionObj.own = rec.own;
                    newNotionObj.gameName = rec.gameName;
                    newNotionObj.loosePrice = newNotionPrice.loosePrice;
                    output.Add(newNotionObj);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Writing output to CSV file...");
            using (StreamWriter streamWriter = new StreamWriter(outputPath, true))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(output);
            }
            Console.WriteLine("Data written to CSV successfully.");
        }
    }
}

