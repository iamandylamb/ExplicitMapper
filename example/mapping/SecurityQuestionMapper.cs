using System;
using System.Security.Cryptography;
using System.Text;
using ExplicitMapper;

namespace Example.ExplicitMapper
{
    public class SecurityQuestionMapper : ClassMapper<Tuple<string, string>, SecurityQuestion>
    {
        private readonly HashAlgorithm hashAlgorithm;
        public SecurityQuestionMapper(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }
        
        protected override void Map(Tuple<string, string> source, SecurityQuestion destination)
        {
            destination.Question = source.Item1;

            destination.HashedAnswer = Convert.ToBase64String(
                                            this.hashAlgorithm.ComputeHash(
                                                Encoding.UTF8.GetBytes(source.Item2)));
            
            destination.CreatedDate = DateTime.UtcNow;
        }
    }
}
