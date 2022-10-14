 using System.ComponentModel.DataAnnotations;

namespace whenAppModel.Models
{
    public class Message
    {

        [Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime Created { get; set; }
        
        public string From { get; set; }
        public string To { get; set; }
        
        public Message()
        {
        }

        public Message(string from, string to, string content)
        {
            this.Content = content;
            this.Created = DateTime.Now;
            this.From = from;
            this.To = to;
        }

        public Message(int id, string content, DateTime created, string from, string to)
        {
            this.Id = id;
            this.Content = content;
            this.Created = created;
            this.From = from;
            this.To = to;
        }

    }
}
