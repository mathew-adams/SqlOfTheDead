using MudBlazor;

namespace SqlOfTheDead.Client.Layout;

public static class UI
{
    public static DialogOptions DialogFullWidth = new DialogOptions()
    {
        CloseButton = false,
        MaxWidth = MaxWidth.ExtraExtraLarge,
        FullWidth = true,
        BackdropClick = false,
        CloseOnEscapeKey = false,
        Position = DialogPosition.TopCenter
    };
}
