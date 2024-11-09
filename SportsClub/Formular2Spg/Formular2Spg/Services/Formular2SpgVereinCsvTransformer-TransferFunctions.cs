using Formular2Spg.Model.Source;
using Formular2Spg.Model.Target;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Formular2Spg.Services
{
     public partial class Formular2SpgVereinCsvTransformer
    {
        private void TransferBeitrittsdatum(RootEntry item, SpgVereinMember newSpgMember)
        {
            string betrittsDatum = TranshelperDatumInDeutsch(item.Response.Beitrittsdatum);
            if (string.IsNullOrEmpty(betrittsDatum)) 
            {
                betrittsDatum = DateTime.Now.ToString("d", germanCulture);
                //throw new Exception("Error ! Beitrittsdatum ist leer");
            }
            
            newSpgMember.Eintritt_Datum = betrittsDatum;
        }

        private void TransferAbteilung(RootEntry item, SpgVereinMember newSpgMember)
        {
            switch (item.Response.Abteilung)
            {
                case "Turnen":
                    newSpgMember.Abteilung_1 = "10";
                    break;
                case "Handball":
                    newSpgMember.Abteilung_1 = "20";
                    break;
                case "Leichtathletik":
                    newSpgMember.Abteilung_1 = "30";
                    break;
                default:
                    Console.WriteLine("Unbekannte Abteilung");
                    throw new ArgumentOutOfRangeException("Unbekannte Abteilung");
            }
        }

        private void TransferMitgliedsbeitrag(Person person, RootEntry item, SpgVereinMember newSpgMember)
        {
            if (person == Person.Person1)
            {
                string mitgliedsBeitragsType = item.Response.Mitgliedsbeitrag;

                if (mitgliedsBeitragsType.StartsWith("Erwachsene")) { newSpgMember.Beitragsart_1 = "01"; _erwartetePersonen = ErwartetePersonen.Eine; }
                else if (mitgliedsBeitragsType.StartsWith("Jugendliche unter 18 Jahren")) { newSpgMember.Beitragsart_1 = "02"; _erwartetePersonen = ErwartetePersonen.Eine; }
                else if (mitgliedsBeitragsType.StartsWith("Familienbeitrag")) { newSpgMember.Beitragsart_1 = "03"; _erwartetePersonen = ErwartetePersonen.Mehrere; }
                else if (mitgliedsBeitragsType.StartsWith("Ehepaare")) { newSpgMember.Beitragsart_1 = "04"; _erwartetePersonen = ErwartetePersonen.Zwei; }
                else if (mitgliedsBeitragsType.StartsWith("Azubis")) { newSpgMember.Beitragsart_1 = "05"; _erwartetePersonen = ErwartetePersonen.Eine; }  // Azubis, Schueler, Studenten ueber 18 Jahre 
                else if (mitgliedsBeitragsType.StartsWith("Rentner")) { newSpgMember.Beitragsart_1 = "06"; _erwartetePersonen = ErwartetePersonen.Eine; }  
                else if (mitgliedsBeitragsType.StartsWith("Rentnerehepaare")) { newSpgMember.Beitragsart_1 = "07"; _erwartetePersonen = ErwartetePersonen.Eine; }
            }
        }

        private void TransferPerson(Person person, RootEntry item, SpgVereinMember newSpgMember)
        {
            FormularEntryResponse response = item.Response;

            if (person == Person.Person1)
            {
                string pVorname, pNachname, pGeburtsDatum, pGeschlecht, pEmail;
                TranshelperGetPersonData(person, item, out pVorname, out pNachname, out pGeburtsDatum, out pGeschlecht, out pEmail);

                // Adresse
                TranshelperPersonenAdresse(newSpgMember, response);
            }
            else if (person == Person.Person2 || person == Person.Person3 || person == Person.Person4 || person == Person.Person5)
            {
                string pVorname, pNachname, pGeburtsDatum, pGeschlecht, pEmail;
                TranshelperGetPersonData(person, item, out pVorname, out pNachname, out pGeburtsDatum, out pGeschlecht, out pEmail);

                TranshelperPerson(newSpgMember, pNachname, pVorname, pGeburtsDatum, pGeschlecht, pEmail);
                TranshelperGeschlecht(pGeschlecht);
                TranshelperEmail(pEmail, item.Id, "1", true);

                // Adresse
                TranshelperPersonenAdresse(newSpgMember, response);
            }
        }
        private void TransferEmail(RootEntry item, SpgVereinMember newSpgMember)
        {
            DateTime beitritt;
            if (DateTime.TryParse(item.Response.Beitrittsdatum, out beitritt))
            {
                newSpgMember.Eintritt_Datum = beitritt.ToString("d", germanCulture);
            }
        }

        private void TransferZahlungsdetails(RootEntry item, SpgVereinMember newSpgMember)
        {
            FormularEntryResponse response = item.Response;

            newSpgMember.Zahlart = "s";   // "s" = SEPA
            newSpgMember.Zahlweise_1 = "jährlich";   // "s" = SEPA

            newSpgMember.Zahler = response.NameZahlungspflichtiger;
            newSpgMember.IBAN_Nr = response.IBAN.Replace(" ","");
            newSpgMember.Sepa_Datum_Mandats_Ref = response.HeutigesDatum;
        }
    }
}
