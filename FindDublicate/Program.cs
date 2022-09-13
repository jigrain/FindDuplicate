using System.Security.Cryptography;

namespace FindDublicate
{
    public static class TestClass
    {
        static void Main(string[] args)
        {
            string FullPath = "D:/Video/WingFox - Creating Realistic Hair in Blender 2.8 for Cycles Render by Maria Anikina (2020) [RUS + ENG Sub]";


            static List<string> GetAllFilesInDirectory(string targetDirectory, List<string> list = null)
            {

                if (list == null)
                {
                    list = new List<string>();
                }


                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);

                foreach (string subdirectory in subdirectoryEntries)
                {
                    list.Add(subdirectory);
                    GetAllFilesInDirectory(subdirectory, list);
                }

                if (list.Count == 0)
                {
                    list.Add($"{targetDirectory}");
                }


                return list;
            }

            List<string> AllFolder = GetAllFilesInDirectory(FullPath);
            Console.WriteLine($"Find {AllFolder.Count} Folder");



            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();


            List<FileDetails> finalDetails = new List<FileDetails>();
            List<string> Duplicate = new List<string>();
            finalDetails.Clear();

            foreach (string file in AllFolder)
            {
                var fileLists = Directory.GetFiles(file);
                foreach (var item in fileLists)
                {
                    using (var fs = new FileStream(item, FileMode.Open, FileAccess.Read))
                    {
                        finalDetails.Add(new FileDetails()
                        {
                            FileName = item,
                            FileHash = BitConverter.ToString(SHA1.Create().ComputeHash(fs)),
                        });
                    }
                }
            }
                var similarList = finalDetails.GroupBy(f => f.FileHash)
                    .Select(g => new { FileHash = g.Key, Files = g.Select(z => z.FileName).ToList() });

                Duplicate.AddRange(similarList.SelectMany(f => f.Files.Skip(1)).ToList());


                watch.Stop();

                Console.WriteLine("Total duplicate files - {0}", Duplicate.Count);

                Console.WriteLine("Duplicate list - \n");
                foreach (var item in Duplicate)
                {
                    Console.WriteLine(item + "\n");
                    FileInfo fi = new FileInfo(item);
                }

                TimeSpan timeSpan = watch.Elapsed;
                Console.WriteLine($"Execution Time: {timeSpan.Seconds} s and {timeSpan.Milliseconds} milliseconds");
            
        }
    }
}



