using System.Collections.Generic;
using System.Security.Principal;
using Formular2Spg.Model.Source;
using Formular2Spg.Model.Target;
using Formular2Spg.Services;
using Newtonsoft.Json;

namespace Formular2Spg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Formular2Spg Programm");

            if (args.Length != 2)
            {
                Console.WriteLine("Hilfe: Formular2Spg <JSON Datei> <output csv file>");
                return;
            }

            string jsonFilePath = args[0];
            string outputSpgVereinCsvFile = args[1];

            
            List<SpgVereinMember>  spgVereinMembers = new List<SpgVereinMember> ();

            string jsonString = File.ReadAllText(jsonFilePath);
            List<RootEntry> formularData = JsonConvert.DeserializeObject<List<RootEntry>>(jsonString); 
            // List<RootEntry> formularData = JsonSerializer.Deserialize<List<RootEntry>>(jsonString);

            if (formularData != null)
            {
                Formular2SpgVereinCsvTransformer formular2SpgVereinCsv = new Formular2SpgVereinCsvTransformer();
                formular2SpgVereinCsv.Transfer(formularData, spgVereinMembers);

                // Write SpgVerein Csv
                SpgVereinCsvWriter spgVereinCsvWriter = new SpgVereinCsvWriter();
                spgVereinCsvWriter.WriteToCsv(spgVereinMembers, outputSpgVereinCsvFile);
            }
            else
            {
                Console.WriteLine("Fehler ! Konnte Quell-Datei (JSON) nicht einlesen.");
            }
        }
    }
}
