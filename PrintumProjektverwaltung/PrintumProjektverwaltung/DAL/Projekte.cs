//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrintumProjektverwaltung.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Projekte
    {
        public int Projektnummer { get; set; }
        public string Projektname { get; set; }
        public string OrdnersturkturJSON { get; set; }
        public string BestellungenJSON { get; set; }
        public string MailsJSON { get; set; }
        public Nullable<System.DateTime> Projektbeginn { get; set; }
        public Nullable<System.DateTime> Produktionsbeginn { get; set; }
        public Nullable<System.DateTime> Inbetriebname { get; set; }
        public bool aktiv { get; set; }
        public string RootOrdner { get; set; }
    }
}
