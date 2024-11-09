using Formular2Spg.Model.Target;
using System.Text;

namespace Formular2Spg.Services
{

    public class SpgVereinCsvWriter
    {
        public void WriteToCsv(List<SpgVereinMember> spgVereinMembers, string filePath)
        {
            var csv = new StringBuilder();
            csv.AppendLine("MitgliedID;Verknüpfung;Anrede;Vorname;Nachname;Strasse;PLZ;Ort;Geburtsdatum;Geschlecht;Eintritt_Datum;Zahlart;Zahler;DTA_1;Einmalbetrag_1;Email;IBAN_Nr;Sepa_Datum_Mandats_Ref;Sepa_kz_ausfuehrung;Sepa_Mandats_Ref;Abteilung_1;Beitragsart_1;Zahlweise_1;Beitrag_1");

            foreach (var member in spgVereinMembers)
            {
                var line = $"{member.MitgliedID};{member.Verknüpfung};{member.Anrede};{member.Vorname};{member.Nachname};{member.Strasse};{member.PLZ};{member.Ort};{member.Geburtsdatum};{member.Geschlecht};{member.Eintritt_Datum};{member.Zahlart};{member.Zahler};{member.DTA_1};{member.Einmalbetrag_1};{member.Email};{member.IBAN_Nr};{member.Sepa_Datum_Mandats_Ref};{member.Sepa_kz_ausfuehrung};{member.Sepa_Mandats_Ref};{member.Abteilung_1};{member.Beitragsart_1};{member.Zahlweise_1};{member.Beitrag_1}";
                csv.AppendLine(line);
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }

}
