using System;
using whenAppModel.Models;

namespace whenAppModel.Services
{
    public interface IMessageService
    {

        public Task<ICollection<Message>> GetAllMessages();
        public Task<ICollection<Message>> GetAllMessages(string username);
        public Task<ICollection<Message>> GetMessagesBetween(string user1, string user2);
        public Task<Message?> GetMessage(int Id);
        public Task<Message?> AddMessage(string from, string to, string content);
        public Task<bool> UpdateMessage(int id, string content);
        public Task<bool> RemoveMessage(int id);
    }
}
