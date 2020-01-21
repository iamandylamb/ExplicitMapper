using System;
using ExplicitMapper;

namespace Example.ExplicitMapper
{
    public class DateOfBirthMapper : StructMapper<UserRegistrationModel, DateTime>
    {
        protected override void Map(UserRegistrationModel source, out DateTime destination)
        {
            destination = new DateTime(source.YearOfBirth, source.MonthOfBirth, source.DayOfBirth);
        }
    }
}
