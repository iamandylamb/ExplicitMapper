using System;

namespace ExplicitMapper.Example
{
    public class User
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public Address Address { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}