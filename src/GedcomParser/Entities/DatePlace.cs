using System;

namespace GedcomParser.Entities
{
    public class DatePlace
    {
        public string DateAsString { get; set; }
        public DateOnly? Date { get; set; }
        public string Place { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Note { get; set; }
    }
}