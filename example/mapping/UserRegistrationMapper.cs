using System;
using ExplicitMapper;

namespace Example.ExplicitMapper
{
    public class UserRegistrationMapper : ClassMapper<UserRegistrationModel, User>
    {
        private IMapper<UserRegistrationModel, Address> addressMapper;
        private IMapper<UserRegistrationModel, DateTime> dateOfBirthMapper;

        public UserRegistrationMapper(
            IMapper<UserRegistrationModel, Address> addressMapper,
            IMapper<UserRegistrationModel, DateTime> dateOfBirthMapper)
        {
            this.addressMapper = addressMapper;
            this.dateOfBirthMapper = dateOfBirthMapper;
        }

        protected override void Map(UserRegistrationModel source, User destination)
        {
            // Mapping a field with an exact name match.
            destination.FirstName = source.FirstName;

            // Mapping a field with a name difference.
            destination.Surname = source.LastName;

            // Using a sub-class-mapping.
            destination.Address = this.addressMapper.Map(source);

            // Use a sub-struct-mapping.
            destination.DateOfBirth = this.dateOfBirthMapper.Map(source);
        }
    }
}
