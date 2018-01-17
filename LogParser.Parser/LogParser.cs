using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Parser
{
    public class LogParser
    {
        public void Parse(string mode, string filePath)
        {
            using (var fs = File.Open(filePath, FileMode.Open))
            {
                using (var file = MemoryMappedFile.CreateFromFile(fs, "log", 0, MemoryMappedFileAccess.Read, new MemoryMappedFileSecurity(), HandleInheritability.None, true))
                {
                    int offset = 0;
                    int size = 20;

                    using (var readStream = file.CreateViewStream(offset, size))
                    {
                        var b = new byte[] {};
                        readStream.ReadAsync(b, offset, size);
                    }
                    //using (var reader = new StreamReader(fs))
                    //{
                    //    StringBuilder sb = new StringBuilder();
                    //    while (!reader.EndOfStream)
                    //    {
                    //        sb.AppendLine(reader.ReadLine());
                    //    }
                    //}
                }
            }
        }
    }
}
