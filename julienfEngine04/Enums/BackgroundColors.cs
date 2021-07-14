using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    enum BackgroundColors
    {
        Black = 0,
        DarkGray = DllImporter.BACKGROUND_INTENSITY,
        DarkRed = DllImporter.BACKGROUND_RED,
        DarkGreen = DllImporter.BACKGROUND_GREEN,
        DarkBlue = DllImporter.BACKGROUND_BLUE,
        DarkYellow = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_GREEN,
        DarkPink = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_BLUE,
        DarkLightblue = DllImporter.BACKGROUND_GREEN | DllImporter.BACKGROUND_BLUE,
        Gray = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_GREEN | DllImporter.BACKGROUND_BLUE,
        Red = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_INTENSITY,
        Green = DllImporter.BACKGROUND_GREEN | DllImporter.BACKGROUND_INTENSITY,
        Blue = DllImporter.BACKGROUND_BLUE | DllImporter.BACKGROUND_INTENSITY,
        Yellow = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_GREEN | DllImporter.BACKGROUND_INTENSITY,
        Pink = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_BLUE | DllImporter.BACKGROUND_INTENSITY,
        Lightblue = DllImporter.BACKGROUND_GREEN | DllImporter.BACKGROUND_BLUE | DllImporter.BACKGROUND_INTENSITY,
        White = DllImporter.BACKGROUND_RED | DllImporter.BACKGROUND_GREEN | DllImporter.BACKGROUND_BLUE | DllImporter.BACKGROUND_INTENSITY
    }
}
