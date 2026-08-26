using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace super_simple_ticketing_system.Models
{
    [Table("TicketTypes")]
    public class TicketType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int TicketTypeId { get; set; }
        public required string IssueType { get; set; }
        public required bool IsActive { get; set; }
    }
}
