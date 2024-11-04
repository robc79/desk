using Desk.Application.Dtos;

namespace Desk.WebUI.ViewModels;

public class ItemSummaryViewModel
{
    public SummaryItemDto Dto { get; set; }

    public bool LinkToLocation { get; set; }

    public ItemSummaryViewModel(SummaryItemDto dto, bool linkToLocation)
    {
        Dto = dto;
        LinkToLocation = linkToLocation;
    }
}