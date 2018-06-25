using System;

namespace LookingForMyFriends.Domain.Entities
{
    public class Friend
    {
        public Friend()
        {
            Id = Guid.NewGuid();
            Location = new Location();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }
    }
}
