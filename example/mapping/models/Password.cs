using System;

namespace Example.ExplicitMapper
{
    public class Password
    {
        public byte[] HashedPassword { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
