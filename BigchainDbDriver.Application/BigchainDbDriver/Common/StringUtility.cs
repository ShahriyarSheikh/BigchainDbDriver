using System;
using System.Collections.Generic;
using System.Text;

namespace BigchainDbDriver.Common
{
    public static class StringUtility
    {
        public static string ToHex(this byte[] buffer, bool isUppercase = false) {
            StringBuilder result = new StringBuilder(buffer.Length * 2);
            for (int i = 0; i < buffer.Length; i++)
                result.Append(buffer[i].ToString(isUppercase ? "X2" : "x2"));
            return result.ToString();
        }

        public static byte[] ToByteArray(this string str) {
            return Encoding.UTF8.GetBytes(str);
        }

    }
}
