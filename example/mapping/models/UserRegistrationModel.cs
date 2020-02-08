using System;

namespace Example.ExplicitMapper
{
    public class UserRegistrationModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public int DayOfBirth { get; set; }

        public int MonthOfBirth { get; set; }

        public int YearOfBirth { get; set; }
        
        public string Password { get; set; }

        public Tuple<string, string>[] SecurityQuestionsAndAnswers { get; set; }
    }
}
