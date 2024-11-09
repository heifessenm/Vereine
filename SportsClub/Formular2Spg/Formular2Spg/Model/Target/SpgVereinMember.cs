using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular2Spg.Model.Target
{
    public class SpgVereinMember
    {
        public int MitgliedID { get; set; }
        public string Verknüpfung { get; set; }
        public string Anrede { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Strasse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Geburtsdatum { get; set; }
        public string Geschlecht { get; set; }
        public string Eintritt_Datum { get; set; }
        public string Zahlart { get; set; }
        public string Zahler { get; set; }
        public string DTA_1 { get; set; }
        public string Einmalbetrag_1 { get; set; }
        public string Email { get; set; }
        public string IBAN_Nr { get; set; }
        public string Sepa_Datum_Mandats_Ref { get; set; }
        public string Sepa_kz_ausfuehrung { get; set; }
        public string Sepa_Mandats_Ref { get; set; }
        public string Abteilung_1 { get; set; }
        public string Beitragsart_1 { get; set; }
        public string Zahlweise_1 { get; set; }
        public string Beitrag_1 { get; set; }
    }

}
