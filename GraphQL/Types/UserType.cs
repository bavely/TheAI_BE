using THEAI_BE.Models;

namespace THEAI_BE.GraphQL.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
       descriptor.Field(u => u.Id);
        descriptor.Field(u => u.Email);
        descriptor.Field(u => u.Name);
        descriptor.Field(u => u.Auth0Id);
            descriptor.Field(u => u.CreatedAt);
        }
    }
}