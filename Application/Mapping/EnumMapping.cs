using Desk.Application.Dtos;
using Desk.Domain.Entities;

namespace Desk.Application.Mapping;

public static class EnumMapping
{
    public static ItemLocation MapToDomain(ItemLocationEnum location)
    {
        return location switch
        {
            ItemLocationEnum.Pile => ItemLocation.Pile,
            ItemLocationEnum.Desk => ItemLocation.Desk,
            ItemLocationEnum.Tabletop => ItemLocation.Tabletop,
            _ => throw new ArgumentOutOfRangeException(nameof(location))
        };
    }

    public static ItemStatus MapToDomain(ItemStatusEnum status)
    {
        return status switch
        {
            ItemStatusEnum.Assembled => ItemStatus.Assembled,
            ItemStatusEnum.Based => ItemStatus.Based,
            ItemStatusEnum.Finished => ItemStatus.Finished,
            ItemStatusEnum.None => ItemStatus.None,
            ItemStatusEnum.OnSpure => ItemStatus.OnSpure,
            ItemStatusEnum.Painted => ItemStatus.Painted,
            ItemStatusEnum.PartAssembled => ItemStatus.PartAssembled,
            ItemStatusEnum.PartPainted => ItemStatus.PartPainted,
            ItemStatusEnum.Primed => ItemStatus.Primed,
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };
    }
}