using System;

namespace ExplicitMapper.Example
{
    public class UserRegistrationMapper : ClassMapper<UserRegistrationModel, User>
    {
        private IMapper<UserRegistrationModel, Address> addressMapper;

        public UserRegistrationMapper( IMapper<UserRegistrationModel, Address> addressMapper)
        {
            this.addressMapper = addressMapper;
        }

        protected override void Map(UserRegistrationModel source, User destination)
        {
            // Mapping a field with an exact name match.
            destination.FirstName = source.FirstName;

            // Mapping a field with a name difference.
            destination.Surname = source.LastName;

            // Using a sub-mapping.
            destination.Address = this.addressMapper.Map(source);

            // Map multiple fields to a single field.
            destination.DateOfBirth = new DateTime(source.YearOfBirth, source.MonthOfBirth, source.DayOfBirth);
        }
    }
}