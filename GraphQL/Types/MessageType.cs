using THEAI_BE.Models;

namespace THEAI_BE.GraphQL.Types
{
    public class MessageType : ObjectType<Messages>
    {

        protected override void Configure(IObjectTypeDescriptor<Messages> descriptor)
        {
            descriptor.Field(m => m.Id);
            descriptor.Field(m => m.Content);
            descriptor.Field(m => m.CreatedAt);
            descriptor.Field(m => m.Role);
            descriptor.Field(m => m.UserId);
            // descriptor.Field(m => m.ThreadId);

            // Navigation properties
            descriptor.Field(m => m.User).Type<UserType>();
            descriptor.Field(m => m.Threads).Type<ThreadsType>();
        }


    }

}