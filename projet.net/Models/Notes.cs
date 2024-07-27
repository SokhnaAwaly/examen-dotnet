namespace projet.net.Models
{
    public class Notes
    {
        public Guid id { get; set; }
        public decimal Notedevoir { get; set; }
        public decimal Noteexamen { get; set; }
        public Guid EtudiantId { get; set; }
        public Etudiant Etudiant { get; set; }
        public Guid ModuleId { get; set; }
        public Module Module { get; set; }
        public decimal Moyenne { get; internal set; }

        
    }
}
