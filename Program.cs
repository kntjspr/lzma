using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace lzma {
    public partial class Program {
        static async Task Main(string[] args) {
            #
            region Initializing Variables

            string encryptionKey, extension, fullPath, savePath, action;
            byte[] encryptionKeyBytes;
            CitadelMain.Mode conversionMode;
            string defaultKey = @ "LLp53sQt%BDPmKn?"; //Hard coded default key, will be used unless specified in args!
            // filePath and fileName used to split fullPath (Currently not used. Initially created to implement predetermined filenames.)
            #
            endregion


            int pointer = args.Length - 1;

            if (pointer < 0 || args[0] == "-h") {
                return;
            }

            fullPath = @args[0];
            savePath = @args[1];
            action = @args[2];

            if (!File.Exists(fullPath)) {
                return;
            }

            if (!Directory.Exists(savePath)) {
                Console.Write($ "Argument {savePath} doesn't exist. \nUse -h for help.");
                return;
            }

            switch (action) {
                case "-e":
                    conversionMode = CitadelMain.Mode.Transform;
                    break;
                case "-d":
                    conversionMode = CitadelMain.Mode.Restore;
                    break;
                default:
                    return;
            }

            encryptionKey = (pointer < 3 || args[3] == "") ? defaultKey : args[3];
            extension = (pointer < 4) ? "enc" : @args[4];
            /*
            Regex rxPattern = new Regex(@"[^\\\\]*$"); // Regex pattern to use
            Match rx = rxPattern.Match(fullPath); //Match fileName pattern to fullpath and put it into a variable named fileName
            string filePath = Regex.Replace(fullPath, rx.Value, ""); //Remove fileName value and name it to a var named filePath
            string fileName = rx.Value;
            */

            using(SHA256Managed SHA256 = new SHA256Managed())
            encryptionKeyBytes = SHA256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            Task < string > task;
            string newFilePath = null;

            CitadelMain ft = new CitadelMain(encryptionKeyBytes);
            if (conversionMode == CitadelMain.Mode.Transform) {
                task = Task.Run(() => ft.TransformFile(fullPath, savePath, extension));
            } else { // (conversionMode == CitadelMain.Mode.Restore)
                task = Task.Run(() => ft.RestoreFile(fullPath, savePath));

            }
            newFilePath = await task;



        }


    }

}
