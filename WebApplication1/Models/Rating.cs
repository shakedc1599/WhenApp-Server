using System.ComponentModel.DataAnnotations;

namespace whenAppModel.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
