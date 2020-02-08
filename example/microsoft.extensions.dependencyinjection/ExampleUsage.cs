using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using ExplicitMapper;
using Example.ExplicitMapper;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace Example.Microsoft.Extensions.DependencyInjection
{
    [TestClass]
    public class ExampleUsage
    {
        private IServiceProvider services;

        private string hashedPassword;

        private UserRegistrationModel model = new UserRegistrationModel
        {
            FirstName = "Bob",
            LastName = "Dylan",
            City = "Minnesota",
            DayOfBirth = 24,
            MonthOfBirth = 5,
            YearOfBirth = 1941,
            Password = "Blowin' in the Wind",
            SecurityQuestionsAndAnswers = new Tuple<string, string>[]
            {
                new Tuple<string, string>("Year you won a Nobel prize", "2016"),
                new Tuple<string, string>("Year of your debut album", "1962"),
            }
        };

        [TestInitialize]
        public void Initialise()
        {
            services = new ServiceCollection().AddMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
                                              .AddSingleton(typeof(XmlSerializerMapper<>), typeof(XmlSerializerMapper<>))
                                              .AddSingleton<HashAlgorithm, SHA1CryptoServiceProvider>()
                                              .BuildServiceProvider();

            var hasher = services.GetService<HashAlgorithm>();
            hashedPassword = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
        }

        [TestMethod]
        public void CanMapTheModel()
        {
            var mapper = services.GetService<IMapper<UserRegistrationModel, User>>();

            var user = mapper.Map(model);

            AssertUser(user);
        }

        [TestMethod]
        public async Task CanAsynchronouslyMapTheModel()
        {
            var mapper = services.GetService<IMapper<UserRegistrationModel, User>>();

            var user = await mapper.MapAsync(model);

            AssertUser(user);
        }

        private void AssertUser(User user)
        {
            Assert.AreEqual(model.LastName, user.Surname);
            Assert.AreEqual(model.City, user.Address.AddressLine3);
            Assert.AreEqual(model.SecurityQuestionsAndAnswers.Length, user.SecurityQuestions.Count());
            Assert.AreEqual(hashedPassword, user.Password.HashedPassword);
        }
    }
}
