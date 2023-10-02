namespace ExtractGooglePhotosTakeout;

public sealed class ConsoleParameters
{
    public bool IsValid => OriginValidation.Invoke(Origin) && DestinationValidation.Invoke(Destination);

    public bool Help { get; private set; }

    public string Origin { get; private set; }

    public string Destination { get; private set; }

    public string[] SearchPatterns { get; private set; }

    public ConsoleParameters() { }

    public ConsoleParameters Build(string[] args)
    {
        if (args.Length == 0)
        {
            PrintHelp();
            return this;
        }

        var help = GetParameter("-h", args);
        var origin = GetParameter("-originPath", args);
        var destination = GetParameter("-destinationPath", args);
        var searchPatterns = GetParameterArray("-searchPatterns", args);

        var parameters = new ConsoleParameters
        {
            Help = help.Exist,
            Origin = origin.Exist ? origin.Value : "",
            Destination = destination.Exist ? destination.Value : "",
            SearchPatterns = searchPatterns.Exist ? searchPatterns.Value : new[] { @"*.jpg", @"*.mp4" },
        };

        return parameters;
    }

    public static void PrintHelp()
    {
        Console.WriteLine();

        Console.WriteLine("Usage:");
        Console.WriteLine("ExtractGooglePhotosTakeout.exe <parameters>");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Parameters:");
        Console.ResetColor();

        Console.WriteLine("\t" + @"[-h]");
        Console.WriteLine("\t" + @"[-originPath D:\path\to\google\takeout\folder\]");
        Console.WriteLine("\t" + @"[-destinationPath D:\path\to\destination\folder\]");
        Console.WriteLine("\t" + @"[-searchPatterns *.jpg,*.mp4]");

        Console.WriteLine();
    }

    private Func<string, bool> OriginValidation = path =>
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine($"Origin path does not exist. [{path}]");
            return false;
        }

        return true;
    };

    private Func<string, bool> DestinationValidation = path =>
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine($"Destination path does not exist. [{path}]");
            return false;
        }

        return true;
    };

    private (string? Value, bool Exist) GetParameter(string parameter, string[] args)
    {
        var index = Array.IndexOf(args, parameter);
        if (index < 0)
        {
            return (null, false);
        }

        return (args[index + 1], true);
    }

    private (string[] Value, bool Exist) GetParameterArray(string parameter, string[] args)
    {
        var index = Array.IndexOf(args, parameter);
        if (index < 0)
        {
            return (null, false);
        }

        var values = args[index + 1].Split(",");

        return (values, true);
    }
}
