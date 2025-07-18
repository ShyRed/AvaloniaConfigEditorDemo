using System;

namespace AvaloniaConfigEditorDemo.Util;

public static class PlatformUtil
{
    public static bool IsWindows11()
    {
        // Windows 11 starts at build 22000
        var version = Environment.OSVersion.Version;
        return OperatingSystem.IsWindows() && version.Build >= 22000;
    }
}