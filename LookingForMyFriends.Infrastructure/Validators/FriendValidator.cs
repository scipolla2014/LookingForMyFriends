using FluentValidation;
using LookingForMyFriends.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LookingForMyFriends.Infrastructure.Validators
{
    public class FriendValidator : AbstractValidator<Friend>
    {
        public FriendValidator(List<Friend> friends)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Informe o nome do seu amigo")
                .NotEmpty().WithMessage("Informe o nome do seu amigo");

            RuleFor(x => x.Location)
                .NotNull().WithMessage("Infome a localização do seu amigo.");

            RuleFor(x => x.Location)
                .Must(x => AlreadyFriendInTheSameLocation(friends, x))
                .WithMessage("Já existe um amigo nessa mesma localização.");
        }

        private bool AlreadyFriendInTheSameLocation(List<Friend> friends, Location location)
        {
            if (location == null) return true;

             var friend = friends.FirstOrDefault(x =>
                 x.Location.Latitude == location.Latitude && x.Location.Longitude == location.Longitude);

            return friend == null;
        }
    }
}
