using whenAppModel.Models;

namespace whenAppModel.Services
{
    public interface IContactsService
    {
        public Task<ICollection<Contact>?> GetAllContacts(string username);
        public Task<ICollection<Contact>?> GetAllContacts(User user);

        public Task<Contact?> GetContact(string contactOf, string contactUsername);

        public Task<Contact?> AddContact(string currentUser, string contactUserName, string contactNickName, string contactServer);

        public Task<bool> UpdateContact(string contactOf, string contactUsername, string newNick, string newServer);

        public Task<bool> UpdateContactLast(string contactOf, string contactUsername, string content);

        public Task<bool> DeleteContact(string contactOf, string contactUsername);

    }
}
