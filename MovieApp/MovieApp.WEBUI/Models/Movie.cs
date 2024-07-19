using System.ComponentModel.DataAnnotations;

namespace MovieApp.WEBUI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Film ismi 10-60 karakter arasında olmalı.")]
        [StringLength(40, MinimumLength =10)]        
        public string? Name { get; set; }

        [Required(ErrorMessage ="Fiyat girmelisiniz")]
        [Range(100,500)]
        public double? Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Image { get; set; }
        public bool IsApproved { get; set; }

        [Required(ErrorMessage ="Kategori id alanı girmelisiniz")]
        public int? CategoryId { get; set; }
    }
}
