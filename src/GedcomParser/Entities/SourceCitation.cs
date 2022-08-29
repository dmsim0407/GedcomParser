namespace GedcomParser.Entities
{
    public class SourceCitation
    {
        public Person Person { get; set; }
        public Source Source { get; set; }
        public string EventType { get; set; }
        public string Page { get; set; }
        public string Date { get; set; }
        public string Note { get; set; }

    }
}
