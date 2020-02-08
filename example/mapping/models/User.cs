using System;
using System.Collections.Generic;

namespace Example.ExplicitMapper
{
    public class User
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public Address Address { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public Password Password { get; set; }

        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
