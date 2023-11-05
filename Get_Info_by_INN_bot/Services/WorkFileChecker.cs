using Get_Info_by_INN_bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Get_Info_by_INN_bot.Services
{
    internal class WorkFileChecker
    {
        public readonly string BasePath;
        public WorkFileChecker()
        {
            BasePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
        }
        internal void CheckTheWorkFiles(string folder, string file)
        {
            try
            {
                if (!Directory.Exists(BasePath + folder))
                {
                    Directory.CreateDirectory(BasePath + folder);
                }
                try
                {
                    FileInfo fileInfo = new FileInfo(Path.Combine(BasePath, folder, file));
                    if (!fileInfo.Exists)
                    {
                        using (FileStream fs = fileInfo.OpenWrite())
                        {
                            JsonSerializer.Serialize(fs, new ConfigModel());
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            catch { throw; }

        }
    }
}
