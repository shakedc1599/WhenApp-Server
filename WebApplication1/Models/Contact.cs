using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace whenAppModel.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactUsername = string.Empty;
            ContactNickname = string.Empty;
            Server = string.Empty;
            LastMessage = string.Empty;
            LastMessageDate = null;
            ContactOfUsername = string.Empty;
        }

        public Contact(string id, string name, string server, string user)
        {
            ContactUsername=id;
            ContactNickname=name;
            Server=server;
            LastMessage= string.Empty;
            LastMessageDate= null;
            ContactOfUsername=user;
        }

        [Key]
        [JsonPropertyName("id")]
        public string ContactUsername { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string ContactNickname { get; set; }

        [Required]
        [JsonPropertyName("server")]
        public string Server { get; set; }

        [JsonPropertyName("last")]
        public string LastMessage { get; set; }

        [JsonPropertyName("lastdate")]
        public DateTime? LastMessageDate { get; set; }

        [Key]
        //[JsonIgnore]
        public string ContactOfUsername { get; set; }
    }
}
