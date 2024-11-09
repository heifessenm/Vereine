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
        // ** Internal functions *************************************************************************************

        private string TranshelperDatumInDeutsch(string sourceDatum)
        {
            if (!string.IsNullOrEmpty(sourceDatum))
            {
                // Datum in Deutsches Format formatieren
                DateTime geburtsdatum;
                string gebDatumInDeutsch;
                if (DateTime.TryParse(sourceDatum, out geburtsdatum))
                {
                    gebDatumInDeutsch = geburtsdatum.ToString("d", germanCulture);
                    return gebDatumInDeutsch;
                }
            }
            return string.Empty;
        }

        private void GetAnzahlPersonen(RootEntry item)
        {
            User_Inputs userinputs = item.User_Inputs;
            if (!string.IsNullOrEmpty(userinputs.VornamePerson1) && !string.IsNullOrEmpty(userinputs.NachnamePerson1))
            { // Person 1 ist nicht leer 
                _anzahlPersonen++;
                _personIsVorhanden[0] = true;
            }

            if (!string.IsNullOrEmpty(userinputs.VornamePerson2) && !string.IsNullOrEmpty(userinputs.NachnamePerson2))
            { // Person 2 ist nicht leer 
                _anzahlPersonen++;
                _personIsVorhanden[1] = true;
            }

            if (!string.IsNullOrEmpty(userinputs.VornamePerson3) && !string.IsNullOrEmpty(userinputs.NachnamePerson3))
            { // Person 3 ist nicht leer 
                _anzahlPersonen++;
                _personIsVorhanden[2] = true;
            }

            if (!string.IsNullOrEmpty(userinputs.VornamePerson4) && !string.IsNullOrEmpty(userinputs.NachnamePerson4))
            { // Person 4 ist nicht leer 
                _anzahlPersonen++;
                _personIsVorhanden[3] = true;
            }


            if (!string.IsNullOrEmpty(userinputs.VornamePerson5) && !string.IsNullOrEmpty(userinputs.NachnamePerson5))
            { // Person 5 ist nicht leer 
                _anzahlPersonen++;
                _personIsVorhanden[4] = true;
            }

            Console.WriteLine("Anzahl Personen: {0} ", _anzahlPersonen);

            string angegebenePersonenMsg = "Angegebene Personen: ";
            int person = 1;
            foreach (bool personIsVorhanden in _personIsVorhanden)
            {
                if (personIsVorhanden)
                {
                    angegebenePersonenMsg = string.Format("{0}, Person {1} ", angegebenePersonenMsg, person);
                }
                person++;
            }
            Console.WriteLine(angegebenePersonenMsg);
        }

        private void TranshelperPerson(SpgVereinMember newSpgMember, string nachname, string vorname, string geburtsdatum, string geschlecht, string email)
        {
            newSpgMember.Nachname = nachname;
            newSpgMember.Vorname = vorname;
            newSpgMember.Geburtsdatum = geburtsdatum;
            newSpgMember.Geschlecht = geschlecht;
            newSpgMember.Email = email;
        }

        private void TranshelperGetPersonData(Person person, RootEntry item, out string vorname, out string nachname, out string geburtsdatum
                                                , out string geschlecht, out string email)
        {
            User_Inputs userinputs = item.User_Inputs;
            bool eMailBenoetigt = false;

            vorname = nachname = geburtsdatum = geschlecht = email = string.Empty;

            if (person == Person.Person1)
            {
                Console.WriteLine("** Einlesen von Person 1");
                vorname = userinputs.VornamePerson1;
                nachname = userinputs.NachnamePerson1;
                geburtsdatum = TranshelperDatumInDeutsch(userinputs.Geburtsdatum1);
                geschlecht = TranshelperGeschlecht(userinputs.Geschlecht1);
                eMailBenoetigt = true;
                email = TranshelperEmail(userinputs.Email1, item.Id, "1", eMailBenoetigt);
            }
            else if (person == Person.Person2)
            {
                Console.WriteLine("** Einlesen von Person 2");
                vorname = userinputs.VornamePerson2;
                nachname = userinputs.NachnamePerson2;
                geburtsdatum = TranshelperDatumInDeutsch(userinputs.Geburtsdatum2);
                geschlecht = TranshelperGeschlecht(userinputs.Geschlecht2);
                email = TranshelperEmail(userinputs.Email2, item.Id, "2");
            }
            else if (person == Person.Person3)
            {
                Console.WriteLine("** Einlesen von Person 3");
                vorname = userinputs.VornamePerson3;
                nachname = userinputs.NachnamePerson3;
                geburtsdatum = TranshelperDatumInDeutsch(userinputs.Geburtsdatum3);
                geschlecht = TranshelperGeschlecht(userinputs.Geschlecht3);
                email = TranshelperEmail(userinputs.Email3, item.Id, "3");
            }
            else if (person == Person.Person4)
            {
                Console.WriteLine("** Einlesen von Person 4");
                vorname = userinputs.VornamePerson4;
                nachname = userinputs.NachnamePerson4;
                geburtsdatum = TranshelperDatumInDeutsch(userinputs.Geburtsdatum4);
                geschlecht = TranshelperGeschlecht(userinputs.Geschlecht4);
                email = TranshelperEmail(userinputs.Email4, item.Id, "4");
            }
            else if (person == Person.Person5)
            {
                Console.WriteLine("** Einlesen von Person 5");
                vorname = userinputs.VornamePerson5;
                nachname = userinputs.NachnamePerson5;
                geburtsdatum = TranshelperDatumInDeutsch(userinputs.Geburtsdatum5);
                geschlecht = TranshelperGeschlecht(userinputs.Geschlecht5);
                email = TranshelperEmail(userinputs.Email5, item.Id, "5");
            }

            if (string.IsNullOrEmpty(vorname) || string.IsNullOrEmpty(nachname) || string.IsNullOrEmpty(geburtsdatum) || string.IsNullOrEmpty(geschlecht) )
            {
                string errorMsg = string.Format("Eine benötigte Personen Angabe fehlt. Vorname = {0}, Nachname = {1}, Geburtsdatum = {2}, Geschlecht = {3}",
                                                                 vorname, nachname, geburtsdatum, geschlecht);
                LogError(item.Id, errorMsg);
            }

            if (eMailBenoetigt && string.IsNullOrEmpty(email))
            {
                LogError(item.Id, "Die benötigte Personen Angabe E-Mail fehlt.");
            }
        }

        private void LogError(int id, string errorMsg)
        {
            Console.WriteLine(errorMsg);
            Errors.Add(new ErrorEntry() { Id = id, Message = errorMsg });
        }

        private string TranshelperGeschlecht(string sourceGeschlecht)
        {
            string targetGeschlecht = string.Empty;

            switch (sourceGeschlecht)
            {
                case "m\u00e4nnlich":
                    targetGeschlecht = "m";
                    break;
                case "weiblich":
                    targetGeschlecht = "w";
                    break;
                case "divers":
                    targetGeschlecht = string.Empty;
                    break;
            }

            return targetGeschlecht;
        }

        private string TranshelperEmail(string sourceEmail, int id, string person, bool isRequired = false)
        {
            string targetEmail = string.Empty;

            if (isRequired)
            {
                if (string.IsNullOrEmpty(sourceEmail))
                {
                    string errorMsg = string.Format("Error ! Email fehlt: Person {0}",person);
                    LogError(id, errorMsg);
                }
            }

            targetEmail = sourceEmail;

            return targetEmail;
        }

        private void TranshelperPersonenAdresse(SpgVereinMember newSpgMember, FormularEntryResponse response)
        {
            newSpgMember.Strasse = response.Strasse + " " + response.Hausnummer;
            newSpgMember.PLZ = response.PLZ;
            newSpgMember.Ort = response.Wohnort;
        }

    }
}
