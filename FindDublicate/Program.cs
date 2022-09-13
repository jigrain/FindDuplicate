using System.Security.Cryptography;


string FullPath = "D:/Models material/Substance Painter Alpha";


static List<string> GetAllFilesInDirectory(string targetDirectory, List<string> list = null)
{

    if (list == null)
    {
        list = new List<string>();
    }


    string[] fileEntries = Directory.GetFiles(targetDirectory);

    foreach (string fileName in fileEntries)
    {
        list.Add(fileName);
    }

    string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);

    foreach (string subdirectory in subdirectoryEntries)
        GetAllFilesInDirectory(subdirectory, list);

    return list;
}

List<string> AllFile = GetAllFilesInDirectory(FullPath);
Console.WriteLine($"Find {AllFile.Count} files");

var watch = new System.Diagnostics.Stopwatch();
watch.Start();






watch.Stop();
Console.WriteLine($"Execution Time: {watch} s");



