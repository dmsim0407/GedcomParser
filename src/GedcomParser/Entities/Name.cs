namespace GedcomParser.Entities
{
    public class Name
    {
        public string GivenNames { get; set; }
        public string Surname { get; set; }

        public string FullName
        {
            get {
                return GivenNames + " " + Surname;
            }
        }
    }
}