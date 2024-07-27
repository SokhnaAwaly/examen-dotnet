namespace projet.net.Models
{
    public class Etudiant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime datenaissance { get; set; }
        public string lieudenaissance { get; set; }
        public string adresse { get; set; }
        public ICollection<Notes> Notes { get; set; }
    }
}
