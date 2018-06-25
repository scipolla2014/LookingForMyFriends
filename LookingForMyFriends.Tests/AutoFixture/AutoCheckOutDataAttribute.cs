using AutoFixture;
using AutoFixture.Xunit2;

namespace LookingForMyFriends.Tests.AutoFixture
{
    public class AutoCheckOutDataAttribute : AutoDataAttribute
    {
        public AutoCheckOutDataAttribute()
            : base(new Fixture()
                  .Customize(new AutoCheckOutDataCustomization()))
        {
        }
    }
}