using System;

class Program
{
    static void Main()
    {
        Console.Title = "Organizador de Arquivos";

        File.AppendAllText(
            "log.txt",
            $"Executando em: {DateTime.Now}\n"
        );

        FileOrganizer();
    }
    static void FileOrganizer()
    {
        string downloadPath = Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");

        Console.WriteLine("Organizing files....");

        foreach (string file in Directory.GetFiles(downloadPath))
        {
            string extension = Path.GetExtension(file).ToLower();

            string destinationFolder =
                GetFolderByExtension(extension, downloadPath);

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string fileName = Path.GetFileName(file);

            string destinationPath =
                Path.Combine(destinationFolder, fileName);

            int contador = 1;

            while (File.Exists(destinationPath))
            {
                string nomeSemExtensao =
                    Path.GetFileNameWithoutExtension(fileName);

                string extensao =
                    Path.GetExtension(fileName);

                destinationPath = Path.Combine(
                    destinationFolder,
                    $"{nomeSemExtensao}({contador}){extensao}"
                );

                contador++;
            }

            File.Move(file, destinationPath);

            Console.WriteLine(
                $"{fileName} -> {destinationPath}"
            );
        }

        Console.WriteLine("Done!");
    }
    static string GetFolderByExtension(string extension,string originalPath)
    {
        return extension switch
        {
            ".pdf" => Path.Combine(originalPath, "PDF"),

            ".jpg" or ".png" or ".jpeg" or ".gif" or ".webp"
                => Path.Combine(originalPath, "Imagens"),

            ".mp4" or ".mkv" or ".avi"
                => Path.Combine(originalPath, "Vídeos"),

            ".mp3" or ".wav" or ".flac"
                => Path.Combine(originalPath, "Músicas"),

            ".zip" or ".rar" or ".7z"
                => Path.Combine(originalPath, "Compactados"),

            ".doc" or ".docx" or ".txt"
                => Path.Combine(originalPath, "Documentos"),

            ".xls" or ".xlsx" or ".csv"
                => Path.Combine(originalPath, "Planilhas"),

            ".exe" or ".msi"
                => Path.Combine(originalPath, "Programas"),

            _ => Path.Combine(originalPath, "Resto")
        };
    }
}