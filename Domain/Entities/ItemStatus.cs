using System.ComponentModel;

namespace Desk.Domain.Entities;

public enum ItemStatus
{
    None = 0,
    
    OnSpure = 1,

    PartAssembled = 2,

    Assembled = 3,

    Primed = 4,

    PartPainted = 5,

    Painted = 6,

    Based = 7,

    Finished = 8
}