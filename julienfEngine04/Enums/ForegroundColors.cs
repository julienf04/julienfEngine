using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    enum ForegroundColors
    {
        Black = 0,
        DarkGray = DllImporter.FOREGROUND_INTENSITY,
        DarkRed = DllImporter.FOREGROUND_RED,
        DarkGreen = DllImporter.FOREGROUND_GREEN,
        DarkBlue = DllImporter.FOREGROUND_BLUE,
        DarkYellow = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_GREEN,
        DarkViolet = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_BLUE,
        DarkLightblue = DllImporter.FOREGROUND_GREEN | DllImporter.FOREGROUND_BLUE,
        Gray = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_GREEN | DllImporter.FOREGROUND_BLUE,
        Red = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_INTENSITY,
        Green = DllImporter.FOREGROUND_GREEN | DllImporter.FOREGROUND_INTENSITY,
        Blue = DllImporter.FOREGROUND_BLUE | DllImporter.FOREGROUND_INTENSITY,
        Yellow = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_GREEN | DllImporter.FOREGROUND_INTENSITY,
        Violet = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_BLUE | DllImporter.FOREGROUND_INTENSITY,
        Lightblue = DllImporter.FOREGROUND_GREEN | DllImporter.FOREGROUND_BLUE | DllImporter.FOREGROUND_INTENSITY,
        White = DllImporter.FOREGROUND_RED | DllImporter.FOREGROUND_GREEN | DllImporter.FOREGROUND_BLUE | DllImporter.FOREGROUND_INTENSITY
    }
}
