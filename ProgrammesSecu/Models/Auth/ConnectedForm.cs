using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammesSecu.Models.Auth
{
    public class ConnectedForm
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

        public string Dimin { get; set; }
    }
}
