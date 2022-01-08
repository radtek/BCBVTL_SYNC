using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Sync.Web.Utils
{
    public static class StringUtils
    {
        public static void Prepend(this FileStream file, string value)
        {
            PrependString(value, file);
        }

        public static string ReadNLineOfFile(string filePath, int limit)
        {
            List<string> listContent = File.ReadLines(filePath, Encoding.UTF8).Take(limit).ToList();
            return String.Join("\n", listContent);
        }
        private static void PrependString(string value, FileStream file)
        {
            var buffer = new byte[file.Length];

            while (file.Read(buffer, 0, buffer.Length) != 0)
            {
            }

            if (!file.CanWrite)
                throw new ArgumentException("The specified file cannot be written.", "file");

            file.Position = 0;
            var data = Encoding.UTF8.GetBytes(value);
            file.SetLength(buffer.Length + data.Length);
            file.Write(data, 0, data.Length);
            file.Write(buffer, 0, buffer.Length);
        }
    }
}