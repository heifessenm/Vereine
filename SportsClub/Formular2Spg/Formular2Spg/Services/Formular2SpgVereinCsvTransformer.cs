using Formular2Spg.Helper;
using Formular2Spg.Model.Source;
using Formular2Spg.Model.Target;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular2Spg.Services
{

    public partial class Formular2SpgVereinCsvTransformer
    {
        public List<SpgVereinMemberEntry> SpgVereinMemberEntries { get; private set; } = new List<SpgVereinMemberEntry>();
        public List<ErrorEntry> Errors { get; private set; } = new List<ErrorEntry>();

        // German culture info
        CultureInfo germanCulture = new CultureInfo("de-DE");

        #region Enums
        enum Person { Person1, Person2, Person3, Person4, Person5 };
        enum ErwartetePersonen { Unbekannt, Eine, Zwei, Mehrere }
        #endregion

        #region private
        private ErwartetePersonen _erwartetePersonen = ErwartetePersonen.Unbekannt;
        private int _anzahlPersonen = 0;
        private List<bool> _personIsVorhanden = new List<bool> { false, false, false, false, false };
        #endregion

        public Formular2SpgVereinCsvTransformer()
        {
          
        }

        public void Transfer(List<RootEntry> formularData, List<SpgVereinMember> spgVereinMembers)
        {
            foreach (var item in formularData)
            {
                // Reset 
                _anzahlPersonen = 0;
                _personIsVorhanden = new List<bool> { false, false, false, false, false };


                SpgVereinMember newSpgMember = new SpgVereinMember();

                FormularEntryResponse entry = item.Response;
                Console.WriteLine("***************************************************************************");
                Console.WriteLine($"ID: {item.Id}");

                // WebSeite 1
                TransferBeitrittsdatum(item, newSpgMember);
                TransferAbteilung(item, newSpgMember);

                // Ermittelt  tasaechliche Anzahl von angegebenen Personen
                GetAnzahlPersonen(item);

                // Setzt auch erwartete Anzahl von Personen (Eine, Zwei, mehrere)
                TransferMitgliedsbeitrag(Person.Person1, item, newSpgMember);
  

                // WebSeite 2
                if (_personIsVorhanden[0])
                {
                    TransferPerson(Person.Person1, item, newSpgMember);
                    Console.WriteLine($"Person 1: Name, Vorname: {newSpgMember.Vorname}, {newSpgMember.Nachname}");
                }
                else { LogError(item.Id, "Erste Person muss eingegeben werden"); }

                // WebSeite 3 (nur bei mehreren Personen [Familie, Ehepaare} 
                if (_erwartetePersonen == ErwartetePersonen.Zwei || _erwartetePersonen == ErwartetePersonen.Mehrere)
                {
                    if (_personIsVorhanden[1])
                    {
                        TransferPerson(Person.Person2, item, newSpgMember);
                        Console.WriteLine($"Person 2: Name, Vorname: {newSpgMember.Vorname}, {newSpgMember.Nachname}");
                    }
                    else { LogError (item.Id, "Zweite Person muss eingegeben werden"); }
                }

                if ( _erwartetePersonen == ErwartetePersonen.Mehrere)
                {
                    if (_personIsVorhanden[2])
                    {
                        TransferPerson(Person.Person3, item, newSpgMember);
                        Console.WriteLine($"Person 3: Name, Vorname: {newSpgMember.Vorname}, {newSpgMember.Nachname}");
                    }

                    if (_personIsVorhanden[3])
                    {
                        TransferPerson(Person.Person4, item, newSpgMember);
                        Console.WriteLine($"Person 4: Name, Vorname: {newSpgMember.Vorname}, {newSpgMember.Nachname}");
                    }

                    if (_personIsVorhanden[4])
                    {
                        TransferPerson(Person.Person5, item, newSpgMember);
                        Console.WriteLine($"Person 5: Name, Vorname: {newSpgMember.Vorname}, {newSpgMember.Nachname}");
                    }
                }

                // WebSeite 6
                TransferZahlungsdetails(item, newSpgMember);

                SpgVereinMemberEntries.Add(new SpgVereinMemberEntry() { Valid = true, SpgVereinMember = newSpgMember });
            }
        }
    }
}
