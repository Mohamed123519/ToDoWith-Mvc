using System.ComponentModel.DataAnnotations;

namespace TodoAPP.ViewModels
{
    public class CreateUser
    {
        public int? Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(4)]
        [MaxLength(16)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
