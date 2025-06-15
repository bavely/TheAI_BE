using THEAI_BE.Models;

namespace THEAI_BE.GraphQL.Types
{
    public class ThreadsType : ObjectType<Threads>
    {
        protected override void Configure(IObjectTypeDescriptor<Threads> descriptor)
        {
            descriptor.Field(t => t.Id);
            descriptor.Field(t => t.Title);
            descriptor.Field(t => t.CreatedAt);
            descriptor.Field(t => t.UserId);

            // Navigation properties
            descriptor.Field(t => t.User).Type<UserType>();
            descriptor.Field(t => t.Messages).Type<ListType<MessageType>>();
        }
    }
}