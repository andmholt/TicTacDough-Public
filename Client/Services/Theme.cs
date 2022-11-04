using MudBlazor;

public static class Theme {
    private static MudTheme theme = new MudTheme() {
        Palette = new Palette() {
            Primary = Colors.Orange.Default,
            Secondary = Colors.Blue.Default,
        },
    };

    private static int spacing = 5;

    public static MudTheme getTheme() {
        return theme;
    }

}