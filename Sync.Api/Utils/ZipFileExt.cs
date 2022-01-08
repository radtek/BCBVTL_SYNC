using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sync.Api.Utils
{
    public static class ZipFileExt
    {
        public static ZipFile Read2(this ZipFile item, byte[] data)
        {
            return ZipFile.Read(new MemoryStream(data));
        }
        public static ZipFile Merge(this ZipFile item, ZipFile file)
        {
            foreach (var entry in file)
                item.AddEntry(entry.FileName, entry.Extract2Byte());
            return item;
        }
        public static byte[] Extract2Byte(this ZipEntry entry)
        {
            using (var ms = new MemoryStream())
            {
                entry.Extract(ms);
                return ms.ToArray();
            }
        }
    }
}