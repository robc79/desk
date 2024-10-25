using System.ComponentModel;

namespace Desk.Domain.Entities;

public enum ItemStatus
{
    None = 0,
    
    [Description("On spure")]
    OnSpure = 1,

    [Description("Part assembled")]
    PartAssembled = 2,

    Assembled = 3,

    Primed = 4,

    [Description("Part painted")]
    PartPainted = 5,

    Painted = 6,

    Based = 7,

    Finished = 8
}