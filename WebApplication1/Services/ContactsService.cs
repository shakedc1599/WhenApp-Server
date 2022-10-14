using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class ContactsService : IContactsService
    {
        private readonly WhenAppContext _context;


        public ContactsService(WhenAppContext Context)
        {
            _context = Context;
        }

        //Function that return all the user contacts.
        public async Task<ICollection<Contact>?> GetAllContacts(string username)
        {
            var contacts = await _context.Contacts.Where(contact => contact.ContactOfUsername == username).ToListAsync();

            return contacts;

        }

        public async Task<ICollection<Contact>?> GetAllContacts(User user)
        {
            if (user == null)
            {
                return null;
            }
            return await _context.Contacts.Where(contact => contact.ContactOfUsername == user.Username).ToListAsync();

        }

        //Function that return the user in contact format(id, name, server, last, lastdate)
        public async Task<Contact?> GetContact(string contactOf, string contactUsername)
        {
            var contacts = await _context.Contacts.FindAsync(contactUsername, contactOf);

            return contacts;
        }

        //TO-DO: Function to add new contact
        public async Task<Contact?> AddContact(string contactOf, string contactUsername, string contactNick, string contactServer)
        {
            if (contactOf == null || contactNick == null || contactServer == null || contactUsername == null)
            {
                return null;
            }

            var isExists = await _context.Contacts.FindAsync(contactUsername, contactOf) != null;
            if (isExists)
            {
                return null;
            }

            Contact contact = new Contact(contactUsername, contactNick, contactServer, contactOf);
            var c = await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return c.Entity;
        }

        //Function that update contact
        public async Task<bool> UpdateContact(string contactOf, string contactUsername, string newNick, string newServer)
        {
            var m = await _context.Contacts.FindAsync(contactUsername, contactOf);

            if (m == null)
            {
                return false;
            }

            m.ContactNickname = newNick;
            m.Server = newServer;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateContactLast(string contactOf, string contactUsername, string content)
        {
            var contact = await _context.Contacts.FindAsync(contactUsername, contactOf);

            if (contact == null)
                return false;

            contact.LastMessage = content;
            contact.LastMessageDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        //Function that delete contact.
        public async Task<bool> DeleteContact(string contactOf, string contactUsername)
        {
            var m = await _context.Contacts.FindAsync(contactUsername, contactOf);

            if (m == null)
            {
                return false;
            }

            _context.Contacts.Remove(m);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
