using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Formular2Spg.Model.Source
{
    public class FormularEntryResponse
    {
        public string FluentFormEmbbedPostId { get; set; }
        public string FluentformNonce { get; set; }
        public string WpHttpReferer { get; set; }
        public string Abteilung { get; set; }
        public string AndereSchonMitglied { get; set; }
        public string Beitrittsdatum { get; set; }
        public string BereitsMitgliedName { get; set; }
        public List<string> Checkbox { get; set; }
        public string Email1 { get; set; }
        public string EmailErziehungsberechtigterA1 { get; set; }
        public string EmailErziehungsberechtigterB1 { get; set; }
        public string Geburtsdatum1 { get; set; }
        public string Geschlecht1 { get; set; }
        public string Hausnummer { get; set; }
        public string HeutigesDatum { get; set; }
        public string IBAN { get; set; }
        public string InputMask { get; set; }
        public string InputMask1 { get; set; }
        public string LeistungspruefungenAnVeranstalterWeitergebbar { get; set; }
        public string Mitgliedsbeitrag { get; set; }
        public string NachnameErziehungsberechtigterA { get; set; }
        public string NachnameErziehungsberechtigterB { get; set; }
        public string NachnamePerson1 { get; set; }
        public string NameZahlungspflichtiger { get; set; }
        public string PLZ { get; set; }
        public string Strasse { get; set; }
        public string TeilnehmerergebnisseVeroeffentlichbar { get; set; }
        public string VornameErziehungsberechtigterB { get; set; }
        public string VornamePerson1 { get; set; }
        public string VornamePersonErziehungsberechtigterA { get; set; }
        public string Wohnort { get; set; }
        public List<string> Zahlungsart { get; set; }
    }
}
