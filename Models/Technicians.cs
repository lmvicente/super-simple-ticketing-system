using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace super_simple_ticketing_system.Models
{
    [Table("Technicians")]
    public class Technicians
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int TechId { get; set; }
        public required string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
    }
}
