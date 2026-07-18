using System.Diagnostics;

namespace SlowFetch;

internal static class Program
{
    private static int Main()
    {
        string[] truck =
        [
            " /‾‾|‾‾‾‾‾‾| delivery",
            "|‾‾‾       | truck   ",
            " |‾|‾‾‾‾|‾|  :D      ",
            "  ‾      ‾           "
        ];

        string[] guy =
        [
            "|O|",
            " | ",
            @"/ \"
        ];
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ProcessStartInfo processStartInfo = new("fastfetch", string.Empty)
        {
            RedirectStandardOutput = true,
        };

        Process? process = Process.Start(processStartInfo);

        if (process is null)
        {
            Console.WriteLine("You need to install fastfetch (ironic)");
            return 1;
        }

        string fetchOutput = process.StandardOutput.ReadToEnd();
        string[] fetchLines = fetchOutput.Split('\n');
        
        string longestLine = "";
        foreach (string line in fetchLines)
            if (line.Length > longestLine.Length)
                longestLine = line;

        int guyX = longestLine.Length + 5;
        int guyY = fetchLines.Length / 2;
        const int waitTime = 0;

        int truckX = guyX + 10;
        
        Console.Clear();

        DrawArt(truck, truckX, guyY);

        for (int y = 0; y < fetchLines.Length; y++)
        {
            for (int x = 0; x < fetchLines[y].Length; x++)
            {
                if (char.IsWhiteSpace(fetchLines[y][x])) continue;
                
                DrawArt(guy, guyX, guyY);
                Thread.Sleep(waitTime);
                DrawCharAtPos(fetchLines[y][x], guyX + 1, guyY - 1);
                Thread.Sleep(waitTime);
                EraseArt(guy, guyX, guyY);
                EraseChar(guyX + 1, guyY - 1);
                DrawArt(guy, x, y + 2);
                DrawCharAtPos(fetchLines[y][x], x + 1, y + 1);
                Thread.Sleep(waitTime);
                EraseArt(guy, x, y + 2);
                DrawArt(guy, guyX, guyY);
            }
        }
        
        EraseArt(guy, guyX, guyY);
        EraseArt(truck, truckX, guyY);

        return 0;
    }

    private static void DrawArt(string[] art, int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        for (int y = 0; y < art.Length; y++)
        {
            for (int x = 0; x < art[y].Length; x++)
            {
                Console.Write(art[y][x]);
                if (x == art[y].Length - 1)
                    Console.SetCursorPosition(xPos, yPos + y + 1);
            }
        }
    }

    private static void DrawCharAtPos(char c, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(c);
    } 
    
    private static void EraseArt(string[] art, int xPos, int yPos)
    {
        for (int y = 0; y < art.Length; y++)
        {
            Console.SetCursorPosition(xPos, yPos + y);
            Console.Write(new string(' ', art[y].Length));
        }
    }

    private static void EraseChar(int xPos, int yPos)
    {
        Console.SetCursorPosition(xPos, yPos);
        Console.Write(' ');
    }
}