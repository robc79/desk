using Desk.Application.Dtos;
using Desk.Domain.Entities;

namespace Desk.Application.Mapping;

public static class EnumMapping
{
    public static ItemLocation MapToDomain(ItemLocationEnum location)
    {
        return location switch
        {
            ItemLocationEnum.None => ItemLocation.None,
            ItemLocationEnum.Pile => ItemLocation.Pile,
            ItemLocationEnum.Desk => ItemLocation.Desk,
            ItemLocationEnum.Tabletop => ItemLocation.Tabletop,
            _ => throw new ArgumentOutOfRangeException(nameof(location))
        };
    }
}