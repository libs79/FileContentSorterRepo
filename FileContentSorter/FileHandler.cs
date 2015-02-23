using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SortFileContents
{
    public class FileHandler : IDisposable
    {
        public string SortTheFile(string[] args)
        {
            string errMsg = "";

            if (args == null || args.Length == 0)
            {
                return @"No Parameters Specified.";
            }
            if (args.Length > 1)
            {
                return @"More than one parameter specified.";
            }

            try
            {
                var sourceFilePath = args[0];
                if (!File.Exists(sourceFilePath))
                {
                    return @"Specified parameter is not a valid file path.";
                }

                string newFilePath;
                if (sourceFilePath.Contains("."))
                {
                    var tmpArr = sourceFilePath.Split('.');
                    newFilePath = sourceFilePath.Substring(0, sourceFilePath.Length - (tmpArr[tmpArr.Length - 1].Length + 1));
                    newFilePath += "-sorted." + tmpArr[tmpArr.Length - 1];
                }
                else
                {
                    newFilePath = sourceFilePath + "-sorted";
                }

                var lines = File.ReadAllLines(sourceFilePath);
                var linqData = lines.Where(line => line.Contains(",")).Select(
                    line => new { LName = line.Split(',').First().Trim(), FName = line.Split(',').Skip(1).First().Trim() }).Where(x => x.FName.Length > 0 || x.LName.Length > 0).OrderBy(x => x.LName).ThenBy(x => x.FName);

                using (var sw = new StreamWriter(newFilePath, false))
                {
                    sw.AutoFlush = true;
                    foreach (var item in linqData)
                    {
                        sw.WriteLine(item.LName + " - " + item.FName);
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                errMsg = "ERROR: " + ex.Message;
            }
            return errMsg;
        }
        public void Dispose()
        {
        }
    }
}
