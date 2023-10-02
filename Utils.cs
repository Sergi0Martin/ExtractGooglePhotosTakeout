namespace ExtractGooglePhotosTakeout;

internal class Utils
{
    internal static void MoveFiles(string originFolder, string destinationFolder, string searchPattern, bool keepFolderStructure)
    {
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
            Console.WriteLine($"Destination folder created '{destinationFolder}'.");
        }

        string[] files = Directory.GetFiles(originFolder, searchPattern);

        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            string destinationPath = Path.Combine(destinationFolder, fileName);

            if (File.Exists(destinationPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"File already exist in destination: {Path.GetFileName(destinationPath)}");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }

            File.Copy(file, destinationPath);
            Console.WriteLine($"Moved: {file} -> {destinationPath}");
        }

        string[] subdirectories = Directory.GetDirectories(originFolder);

        foreach (var subdirectory in subdirectories)
        {
            string subdirectoryName = Path.GetFileName(subdirectory);
            string newOriginFolder = Path.Combine(originFolder, subdirectoryName);
            if (keepFolderStructure)
            {
                destinationFolder = Path.Combine(destinationFolder, subdirectoryName);
            }

            MoveFiles(newOriginFolder, destinationFolder, searchPattern, keepFolderStructure: keepFolderStructure);
        }
    }
}
