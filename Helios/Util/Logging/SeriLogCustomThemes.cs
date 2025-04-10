using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;

public class SeriLogCustomThemes
{
    public static readonly SystemConsoleTheme Theme = new SystemConsoleTheme(new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
    {
        [ConsoleThemeStyle.TertiaryText] = new SystemConsoleThemeStyle
        {
            Foreground = ConsoleColor.White,
        },
        [ConsoleThemeStyle.SecondaryText] = new SystemConsoleThemeStyle
        {
            Foreground = ConsoleColor.DarkGray,
        },
        [ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle
        {
            Foreground = ConsoleColor.Cyan,
        },
        [ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle
        {
            Foreground = ConsoleColor.Red,
        },
        [ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle
        {
            Foreground = ConsoleColor.DarkYellow,
        },
        [ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle
        {
            Foreground = ConsoleColor.Yellow,
        }
    });
}
