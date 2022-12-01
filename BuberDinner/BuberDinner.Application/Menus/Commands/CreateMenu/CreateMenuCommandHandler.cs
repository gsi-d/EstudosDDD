using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.ValueObjects;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public record CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    public Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        //Create Menu
        var menu = Menu.Create(
            hostId : HostId.Create(request.HostId), 
            name: request.Name, 
            description: request.Description, 
            sections: request.Sections.ConvertAll(section => MenuSection.Create(
                section.Name, 
                section.Description, 
                section.Items.ConvertAll(item => MenuItem.Create(
                    item.Name, 
                    item.Description)))));
        return default!;
    }
}
