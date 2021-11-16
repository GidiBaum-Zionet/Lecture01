using System.Collections.Generic;
using ElementBackend.Models;

namespace ElementBackend.Services
{
    public class UserRepository
    {
        record UserData(string Name, string PasswordHash, Role Role);

        readonly Dictionary<string, UserData> _Users = new();

        public UserRepository()
        {
            
        }

        public void AddUser(string name, string password, Role role)
        {
            _Users[name] = new UserData(name, BCrypt.Net.BCrypt.HashPassword(password), role);
        }

        public Role VerifyUser(string name, string password)
        {
            if (_Users.TryGetValue(name, out var user))
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return user.Role;
                }
            }

            return Role.None;
        }

    }
}
