using System.Collections.Generic;
using System.IO;

namespace FileLib
{
    /// <summary>
    /// Чтение текстового файла
    /// </summary>
    public class TxtHandler : FileHandler
    {
        public TxtHandler(string path) : base(path) { }

        public override void ReadFile(out List<string> lines)
        {
            lines = new List<string>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if(!string.IsNullOrEmpty(line))
                        lines.Add(line);
                }
                reader.Close();
            }
        }
    }
}
