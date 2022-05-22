using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
namespace lzma
{
    public partial class Program
    {
        static async Task Main(string[] args)
        {
            #region Initializing Variables
            string encryptionKey, extension;
            byte[] encryptionKeyBytes;
            CitadelMain.Mode conversionMode;
            string defaultKey = "hkN!VEh+g=9rwxzZ";
            #endregion
            if (args.Length <= 0)
            {
                return;
            }
            else if (args[0] == "-h")
            {
                return;
            }

            if (args.Length < 2)
            {
                return;
            }
            else if (args.Length < 3)
            {
                return;
            }

            string fullPath = @args[0]; string savePath = @args[1]; string action = @args[2];

            if (!File.Exists(fullPath))
            {
                return;
            }
            else if (!Directory.Exists(savePath))
            {
                return;
            }
            switch (action)
            {
                case "-e":
                    conversionMode = CitadelMain.Mode.Transform;
                    break;
                case "-d":
                    conversionMode = CitadelMain.Mode.Restore;
                    break;
                default:
                    return;
            }
            if (args.Length < 3)
            {
                encryptionKey = defaultKey;
            }
            else if (args[3] == "")
            {
                encryptionKey = defaultKey;
            }
            else
            {
                encryptionKey = args[3];
            }

            if (args.Length < 4)
            {
                extension = "enc"; 
            }
            else
            {
                extension = @args[4];
            }
            Regex rxPattern = new Regex(@"[^\\\\]*$");
            Match rx = rxPattern.Match(fullPath); 
            string filePath = Regex.Replace(fullPath, rx.Value, ""); 
            string fileName = rx.Value;
            using (SHA256Managed SHA256 = new SHA256Managed())
            encryptionKeyBytes = SHA256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            Task<string> task;
            string newFilePath = null;
            CitadelMain ft = new CitadelMain(encryptionKeyBytes);
            if (conversionMode == CitadelMain.Mode.Transform)
            {
                task = Task.Run(() => ft.TransformFile(fullPath, savePath, extension));
            }
            else
            { 
                task = Task.Run(() => ft.RestoreFile(fullPath, savePath));
            }
            newFilePath = await task;
            Console.Write(Path.GetFileName(newFilePath));
        }
    }
}
