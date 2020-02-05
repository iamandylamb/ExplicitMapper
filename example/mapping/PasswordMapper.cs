using System;
using System.Security.Cryptography;
using System.Text;
using ExplicitMapper;

namespace Example.ExplicitMapper
{
    public class PasswordMapper : ClassMapper<UserRegistrationModel, Password>
    {
        private readonly HashAlgorithm hashAlgorithm;
        public PasswordMapper(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgoritm = hashAlgorithm;
        }
        
        protected override void Map(UserRegistrationModel source, Password destination)
        {
            destination.HashedPassword = this.hashAlgorithm.ComputeHash(
                                            Encoding.UTF8.GetBytes(source.Password));
            
            destination.CreatedDate = DateTime.UtcNow;
        }
    }
}
