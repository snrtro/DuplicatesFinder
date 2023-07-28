using System.Security.Cryptography;

var folder = @"C:\Users\alexander\Desktop\testData";


Directory.GetFiles(folder)
    .Select(f =>
    {
        using var fs = new FileStream(f, FileMode.Open, FileAccess.Read);
        return new
        {
            Filename = f,
            Filehash = BitConverter.ToString(SHA1.Create().ComputeHash(fs))
        };
    })
    .GroupBy(f => f.Filehash)
    .Select(g => new { Filehash = g.Key, Files = g.Select(z => z.Filename).ToList() })
    .SelectMany(f => f.Files.Skip(1))
    .ToList()
    .ForEach(File.Delete);

Console.WriteLine("DONE!");