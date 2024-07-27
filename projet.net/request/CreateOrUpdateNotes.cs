using projet.net.Models;

namespace projet.net.request
{
    public class CreateOrUpdateNotes
    {
        public Guid id { get; set; }
        public decimal Notedevoir { get; set; }
        public decimal Noteexamen { get; set; }
        public Guid EtudiantId { get; set; }
        public required Etudiant Etudiant { get; set; }
        public Guid ModuleId { get; set; }
        public required Module Module { get; set; }
        public decimal Moyenne { get; internal set; }
    }
}
