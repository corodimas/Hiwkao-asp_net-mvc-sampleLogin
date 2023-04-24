using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleLogin.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string StoreName { get; set; }

        [Required]
        public string Menu1 { get; set; }

        [Required]
        [Range(1,5)]
        public int Amount1 { get; set; }
        public string Menu2 { get; set; }
        [Range(1, 5)]
        public int Amount2 { get; set; }

        public string Menu3 { get; set; }
        [Range(1, 5)]
        public int Amount3 { get; set; }

        public string Menu4 { get; set; }

        [Range(1, 5)]
        public int Amount4 { get; set; }

        public string Menu5 { get; set; }
        [Range(1, 5)]
        public int Amount5 { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string Status { get; set; }

        public string UserId { get; set; }

        public string RiderId { get; set; }
    }
}
