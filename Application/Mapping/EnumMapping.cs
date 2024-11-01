using System.Security.Cryptography;
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

    public static ItemLocationEnum MapFromDomain(ItemLocation location)
    {
        return location switch
        {
            ItemLocation.Pile => ItemLocationEnum.Pile,
            ItemLocation.Desk => ItemLocationEnum.Desk,
            ItemLocation.Tabletop => ItemLocationEnum.Tabletop,
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
    
    public static ItemStatusEnum MapFromDomain(ItemStatus status)
    {
        return status switch
        {
            ItemStatus.Assembled => ItemStatusEnum.Assembled,
            ItemStatus.Based => ItemStatusEnum.Based,
            ItemStatus.Finished => ItemStatusEnum.Finished,
            ItemStatus.None => ItemStatusEnum.None,
            ItemStatus.OnSpure => ItemStatusEnum.OnSpure,
            ItemStatus.Painted => ItemStatusEnum.Painted,
            ItemStatus.PartAssembled => ItemStatusEnum.PartAssembled,
            ItemStatus.PartPainted => ItemStatusEnum.PartPainted,
            ItemStatus.Primed => ItemStatusEnum.Primed,
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };
    }
}