namespace projet.net.request
{
    public class CreateOrUpdateEtudiant
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime datenaissance { get; set; }
        public required string lieudenaissance { get; set; }
        public required string adresse { get; set; }
    }
}
