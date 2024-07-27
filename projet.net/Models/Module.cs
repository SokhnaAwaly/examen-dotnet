namespace projet.net.Models
{
    public class Module
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Notes> Notes { get; set; }

    }
}
