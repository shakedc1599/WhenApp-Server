using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace whenAppModel.Models
{
    public class User
    {
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;  
        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Required] 
        [Key]
        [JsonPropertyName("id")]
        public string Username { get; set; }

       

        [RegularExpression("^(? !.* )(?=.*'\'d)(?=.*[A - Z]).{8,}$")]
        [Required]
        [JsonIgnore]
        public string Password { get; set; }

    }
}
