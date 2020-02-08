using System;
using ExplicitMapper;

namespace Example.ExplicitMapper
{
    public class UserRegistrationMapper : ClassMapper<UserRegistrationModel, User>
    {
        private IMapper<UserRegistrationModel, Address> addressMapper;
        private IMapper<UserRegistrationModel, DateTime> dateOfBirthMapper;
        private IMapper<UserRegistrationModel, Password> passwordMapper;
        private IMapper<Tuple<string, string>, SecurityQuestion> securityQuestionMapper;

        public UserRegistrationMapper(
            IMapper<UserRegistrationModel, Address> addressMapper,
            IMapper<UserRegistrationModel, DateTime> dateOfBirthMapper,
            IMapper<UserRegistrationModel, Password> passwordMapper,
            IMapper<Tuple<string, string>, SecurityQuestion> securityQuestionMapper)
        {
            this.addressMapper = addressMapper;
            this.dateOfBirthMapper = dateOfBirthMapper;
            this.passwordMapper = passwordMapper;
            this.securityQuestionMapper = securityQuestionMapper;
        }

        protected override void Map(UserRegistrationModel source, User destination)
        {
            // Mapping a field with an exact name match.
            destination.FirstName = source.FirstName;

            // Mapping a field with a name difference.
            destination.Surname = source.LastName;

            // Using a sub-class-mapping.
            destination.Address = this.addressMapper.Map(source);

            // Using a sub-struct-mapping.
            destination.DateOfBirth = this.dateOfBirthMapper.Map(source);
            
            // Using a sub-mapper that in-turn has dependencies.
            destination.Password = this.passwordMapper.Map(source);

            // Using a sub-mapper to map a collection.
            destination.SecurityQuestions = this.securityQuestionMapper.Map(source.SecurityQuestionsAndAnswers);
        }
    }
}
