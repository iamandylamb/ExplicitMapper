using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExplicitMapper.Example
{
    [TestClass]
    public class UserRegistrationExample
    {
        // The mapper would usually be provided by the DI system.
        private IMapper<UserRegistrationModel, User> mapper = new UserRegistrationMapper(new AddressMapper());

        [TestMethod]
        public void Example()
        {
            // This could be database entities.
            var expected = new User 
            { 
                FirstName = "Bob",
                Surname = "Smith",
                Address = new Address 
                {
                    AddressLine1 = "12 Patterson Lane",
                    AddressLine2 = "Bridge of Allan",
                    AddressLine3 = "Stirling",
                    AddressLine4 = string.Empty,
                    PostalCode = "FK9 2DE"
                },
                DateOfBirth = new DateTime(1982, 09, 12)
            };

            // This could be the data from a flat form coming from a website.
            var source = new UserRegistrationModel
            {
                FirstName = "Bob",
                LastName = "Smith",
                AddressLine1 = "12 Patterson Lane",
                AddressLine2 = "Bridge of Allan",
                City = "Stirling",
                Postcode = "FK9 2DE",
                DayOfBirth = 12,
                MonthOfBirth = 09,
                YearOfBirth = 1982
            };

            // Mapping the form submission data to the database entities.
            var actual = mapper.Map(source);

            var serializer = new XmlSerializerMapper<User>();

            Assert.AreEqual(
                serializer.Map(expected), 
                serializer.Map(actual));
        }
    }
}