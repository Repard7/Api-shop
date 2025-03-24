using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiUseSqlLiteItog.Models
{
    public class User
    {
        [Key]
        public string? Email { get; set; }

        public string? Fio { get; set; }

        [Required]
        public string? Password { get; set; }

        public string UserToken { get; set; } = "";

        public int IsAdmin { get; set; } = 0;
    }
}
