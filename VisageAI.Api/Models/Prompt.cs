using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisageAI.Api.Models
{
    public class Prompt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 👈 Esto le dice a EF que el ID es autoincremental
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]  // puedes ajustar según necesidad
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Column(TypeName = "LONGTEXT")]  // 👈 ideal para base64 o URLs largas
        public string Image { get; set; }
    }
}
