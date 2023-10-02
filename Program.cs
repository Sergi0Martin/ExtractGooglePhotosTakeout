using ExtractGooglePhotosTakeout;

var parameters = new ConsoleParameters();
parameters = parameters.Build(args);

if (parameters.Help)
{
    ConsoleParameters.PrintHelp();
}

if (!parameters.IsValid)
{
    return;
}

var originFolderPath = parameters.Origin;
var destinationPath = parameters.Destination;

var searchPatterns = parameters.SearchPatterns;
foreach (var searchPattern in searchPatterns)
{
    Utils.MoveFiles(originFolderPath, destinationPath, searchPattern, keepFolderStructure: false);
}
