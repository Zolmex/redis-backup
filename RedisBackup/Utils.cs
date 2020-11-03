using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RedisBackup
{
    public static class Utils
    {
        public static string ToSafe(this string str)
        {
            var invalid = Path.GetInvalidFileNameChars();
            foreach (char c in invalid.Where(c => str.Contains(c)))
                str = str.Replace(c, '-');
            return str;
        }
    }
}
