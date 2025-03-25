using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private UserRole Role { get; set; }

        public User(int id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public int GetId() => Id;
        public string GetName() => Name;
        public UserRole GetRole() => Role;

        public void ChangeRole(UserRole newRole)
        {
            Role = newRole;
        }
    }

}
