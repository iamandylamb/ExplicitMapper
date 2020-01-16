namespace ExplicitMapper.Example
{
    public class AddressMapper : ClassMapper<UserRegistrationModel, Address>
    {
        protected override void Map(UserRegistrationModel source, Address destination)
        {
            destination.AddressLine1 = source.AddressLine1;
            
            destination.AddressLine2 = source.AddressLine2;
            
            destination.AddressLine3 = source.City;

            destination.AddressLine4 = string.Empty;

            destination.PostalCode = source.Postcode;
        }
    }
}