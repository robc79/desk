using System.ComponentModel.DataAnnotations;

namespace Desk.Application.Dtos;

public enum ItemStatusEnum
{
    None = 0,
    
    [Display(Name="On spure")]
    OnSpure = 1,

    [Display(Name="Part assembled")]
    PartAssembled = 2,

    Assembled = 3,

    Primed = 4,

    [Display(Name="Part painted")]
    PartPainted = 5,

    Painted = 6,

    Based = 7,

    Finished = 8
}