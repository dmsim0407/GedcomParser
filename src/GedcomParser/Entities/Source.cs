namespace GedcomParser.Entities
{
    public class Source
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AbbreviatedTitle { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DatePlace Published { get; set; }
    }    
}