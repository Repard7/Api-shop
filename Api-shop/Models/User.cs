using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiUseSqlLiteItog.Models
{
    public class User
    {
        [Key]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Fio { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        public string UserToken { get; set; } = "";

        public int IsAdmin { get; set; } = 0;
    }
}
