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

        private UserRegistrationModel model1 = new UserRegistrationModel
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

        private UserRegistrationModel model2 = new UserRegistrationModel
        {
            FirstName = "Joni",
            LastName = "Mitchell",
            City = "Calgary",
            DayOfBirth = 7,
            MonthOfBirth = 11,
            YearOfBirth = 1943,
            Password = "Big Yellow Taxi",
            SecurityQuestionsAndAnswers = new Tuple<string, string>[]
            {
                new Tuple<string, string>("Best folk performance Grammy in 1969", "Clouds"),
                new Tuple<string, string>("Your madden name", "Anderson"),
            }
        };

        [TestInitialize]
        public void Initialise()
        {
            services = new ServiceCollection().AddMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
                                              .AddSingleton(typeof(XmlSerializerMapper<>), typeof(XmlSerializerMapper<>))
                                              .AddSingleton<HashAlgorithm, SHA1CryptoServiceProvider>()
                                              .BuildServiceProvider();
        }

        [TestMethod]
        public void CanMapTheModelSynchronously()
        {
            var mapper = services.GetService<IMapper<UserRegistrationModel, User>>();

            var user = mapper.Map(model1);

            AssertUser(model1, user);
        }

        [TestMethod]
        public async Task CanMapTheModelAsynchronously()
        {
            var mapper = services.GetService<IMapper<UserRegistrationModel, User>>();

            var user = await mapper.MapAsync(model2);

            AssertUser(model2, user);
        }

        [TestMethod]
        public async Task CanMapModelCollectionAsynchronously()
        {
            var mapper = services.GetService<IMapper<UserRegistrationModel, User>>();

            var users = await mapper.MapAsync(new[] { model1, model2 });

            AssertUser(model1, users.First());
            AssertUser(model2, users.Last());
        }

        [TestMethod]
        public async Task CanMapModelCollectionInParallel()
        {
            var mapper = services.GetService<IMapper<UserRegistrationModel, User>>();

            var users = await mapper.MapParallel(new[] { model1, model2 });

            AssertUser(model1, users.First());
            AssertUser(model2, users.Last());
        }

        private void AssertUser(UserRegistrationModel model, User user)
        {
            Assert.AreEqual(model.LastName, user.Surname);
            Assert.AreEqual(model.City, user.Address.AddressLine3);
            Assert.AreEqual(model.SecurityQuestionsAndAnswers.Length, user.SecurityQuestions.Count());
            Assert.AreEqual(ExpectedHash(model.Password), user.Password.HashedPassword);
        }

        private string ExpectedHash(string input)
        {
            var hasher = services.GetService<HashAlgorithm>();
            return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
