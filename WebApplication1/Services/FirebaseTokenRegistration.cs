using FirebaseAdmin.Messaging;
using whenAppModel.Models;
using WhenUp;

namespace WhenAppApi.Services
{
    public class FirebaseTokenRegistration
    {
        private readonly Dictionary<string, string> _tokens = new Dictionary<string, string>();
        private readonly WhenAppContext _context;

        public FirebaseTokenRegistration(WhenAppContext context)
        {
            _context = context;
        }
        public void AddToken(string username, string token)
        {
            foreach(var _token in _tokens)
            {
                if (_token.Value.Equals(token, StringComparison.CurrentCultureIgnoreCase))
                {
                    _tokens.Remove(_token.Key);
                }
            }
            _tokens[username] = token;
        }

        public bool GetToken(string username, out string? token)
        {
            return _tokens.TryGetValue(username, out token);
        }

        public async void SendMessage(string from, string to, string content)
        {
            GetToken(to, out var token);
            if (token == null)
            {
                return;
            }
            var message = _context.Messages.Where(message => message.From == from && message.To == to && message.Content == content).OrderByDescending(message => message.Created).FirstOrDefault();
            if (message == null)
            {
                // error
                return;
            }
            var notifyMessage = new FirebaseAdmin.Messaging.Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = content,
                    Title = "new message from " + from,
                },
                Data = new Dictionary<string, string>()
                {
                    {"type", "message" },
                    {"id", message.Id.ToString() },
                    {"contact", from },
                    {"content", message.Content },
                    {"created", message.Created.ToString("yyyy-MM-ddThh:mm:ss.fffffffzzz") },
                    {"sent", "false" }
                },
            };
            var result = await FirebaseMessaging.DefaultInstance.SendAsync(notifyMessage);
        }

        public async void SendContact(string to, Contact contact)
        {
            GetToken(to, out var token);
            if (token == null)
            {
                return;
            }
            var notifyMessage = new FirebaseAdmin.Messaging.Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = contact.ContactNickname,
                    Title = "new contact",
                },
                Data = new Dictionary<string, string>()
                {
                    {"type", "contact" },
                    {"id", contact.ContactUsername },
                    {"name", contact.ContactNickname },
                    { "server", contact.Server },
                },
            };
            var result = await FirebaseMessaging.DefaultInstance.SendAsync(notifyMessage);
        }
    }
}
