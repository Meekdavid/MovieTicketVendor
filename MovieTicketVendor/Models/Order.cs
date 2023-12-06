using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketVendor.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public int OrderItemId { get; set; }
        [ForeignKey("OrderItemId")]
        public OrderItem OrderItem { get; set; }
    }
}
