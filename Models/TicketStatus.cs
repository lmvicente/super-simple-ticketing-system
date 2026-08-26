using System.ComponentModel.DataAnnotations.Schema;

namespace super_simple_ticketing_system.Models
{
    [Table("TicketStatus")]
    public class TicketStatus
    {
        public required int TicketStatusId { get; set; }
        public required string StatusName { get; set; }
    }
}
