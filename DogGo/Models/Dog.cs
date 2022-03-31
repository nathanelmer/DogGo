using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(45, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string Breed { get; set; }

        [Display(Name = "Notes (Optional)")]
        public string Notes { get; set; }

        [Display(Name = "Image Url (Optional)")]
        public string ImageUrl { get; set; }
        public Owner Owner { get; set; }
    }
}
