using System;

namespace Example.ExplicitMapper
{
    public class SecurityQuestion
    {
        public string Question { get; set; }

        public string HashedAnswer { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}