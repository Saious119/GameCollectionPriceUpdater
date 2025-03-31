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
            if (!File.Exists("priceCharting.csv"))
            {
                Console.WriteLine("Notion Game Pricing updater: price charting file not found.");
                return;
            }

            if (!File.Exists("notion.csv"))
            {
                Console.WriteLine("Notion Game Pricing updater: notion file not found.");
                return;
            }
            Console.WriteLine("Reading...");
            IEnumerable<priceCharting> priceChartings;
            IEnumerable<notionCSV> notions;
            using (var reader = new StreamReader("priceCharting.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                priceChartings = csv.GetRecords<priceCharting>().ToList();
            }

            using (var reader = new StreamReader("notion.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                notions = csv.GetRecords<notionCSV>().ToList();
            }
            Console.WriteLine("Both Files Read.");
            Console.WriteLine("Updating Prices and filtering data...");
            priceChartings = priceChartings.Where(x => x.consoleName == consoleName);
            List<notionCSV> output = new();
            foreach (var rec in notions)
            {
                try
                {
                    var newNotionPrice = priceChartings.Where(x => x.productName == rec.gameName).FirstOrDefault();
                    var newNotionObj = new notionCSV(rec.own, rec.gameName, newNotionPrice.loosePrice);
                    output.Add(newNotionObj);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Writing output to CSV file...");
            using (StreamWriter streamWriter = new StreamWriter("output.csv",true))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(output);
            }
            Console.WriteLine("Data written to CSV successfully.");
        }
    }
}

