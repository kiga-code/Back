using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kiga.domain.Entities
{
    public class UsuarioDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string facebookId { get; set; }
        [Required]
        [StringLength(50)]
        public string firstName { get; set; }
        [Required]
        [StringLength(120)]
        public string lastName { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? created_at { get; set; } = DateTime.UtcNow;

        public string token { get; set; }
    }
}