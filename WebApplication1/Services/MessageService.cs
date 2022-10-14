using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class MessageService : IMessageService
    {
        private readonly WhenAppContext _context;

        //constructor
        public MessageService(WhenAppContext context)
        {
            _context = context;
        }

        //not a action
        public async Task<ICollection<Message>> GetAllMessages()
        {

            return await _context.Messages.ToListAsync();
        }

        public async Task<ICollection<Message>> GetAllMessages(string username)
        {
            var result = _context.Messages
                .Where(message => message.From == username || message.To == username);

            return await result.ToListAsync();
        }

        //action number 1
        public async Task<ICollection<Message>> GetMessagesBetween(string user1, string user2)
        {
            var user1sent = _context.Messages
                .Where(message => message.From == user1 && message.To == user2);

            var user2sent = _context.Messages
                .Where(message => message.From == user2 && message.To == user1);

            return await user1sent.Union(user2sent).ToListAsync();

        }

        //action number 3
        public async Task<Message?> GetMessage(int Id)
        {
            var result = await _context.Messages.FindAsync(Id);

            return result;
        }

        //action number 2
        public async Task<Message?> AddMessage(string from, string to, string content)
        {
            var contact = await _context.Contacts.FindAsync(to, from);

            if (contact == null)
                return null;

            contact.LastMessage = content;
            contact.LastMessageDate = DateTime.Now;

            var message = new Message(from, to, content);

            var m = await _context.Messages.AddAsync(message);

            await _context.SaveChangesAsync();

            return m.Entity;
        }

        //action number 4
        public async Task<bool> UpdateMessage(int id, string content)
        {
            var m = await _context.Messages.FindAsync(id);
            if (m == null)
            {
                return false;
            }

            m.Content = content;
            await _context.SaveChangesAsync();

            return true;
        }
        //action number 5
        public async Task<bool> RemoveMessage(int id)
        {
            var m = await _context.Messages.FindAsync(id);

            if (m == null)
            {
                return false;
            }

            _context.Messages.Remove(m);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
