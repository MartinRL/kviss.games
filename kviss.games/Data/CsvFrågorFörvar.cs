using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using kviss.games.Spel;
using kviss.games.Spel.MerEllerMindre;

namespace kviss.games.Data
{
    public class CsvFrågorFörvar : IFrågorFörvar
    {
        private readonly List<Fråga> frågor;

        public CsvFrågorFörvar()
        {
            frågor = [];
            DeserialiseraFrågorFrånCsv("Frågor.csv");
        }

        private void DeserialiseraFrågorFrånCsv(string filväg)
        {
            using StreamReader reader = new(filväg);
            using CsvReader csv = new(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            });

            frågor.AddRange(csv.GetRecords<Fråga>());
        }

        public ISet<Fråga> HämtaTioSlumpmässigt()
        {
            Random random = new();
            return frågor.OrderBy(x => random.Next()).Take(10).ToHashSet();
        }
    }
}
