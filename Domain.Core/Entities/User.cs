using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public LLMParameters DefaultLLMParameters { get; set; }

        public User()
        {
            Name = string.Empty;
            Password = string.Empty;
            DefaultLLMParameters = new LLMParameters();
        }
        public User(string name, string password) : this()
        {
            Name = name;
            Password = password;
        }
    }
}
