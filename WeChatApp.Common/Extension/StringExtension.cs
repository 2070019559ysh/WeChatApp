using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeChatApp.Common.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// SHA1加密，返回SHA1加密后的字符串
        /// </summary>
        /// <param name="intput">要进行加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA1Encrypt(this string input)
        {
            byte[] strRes = Encoding.Default.GetBytes(input);
            HashAlgorithm mySHA = new SHA1CryptoServiceProvider();
            strRes = mySHA.ComputeHash(strRes);
            StringBuilder enText = new StringBuilder();
            foreach (byte byteIn in strRes)
            {
                enText.AppendFormat("{0:x2}", byteIn);
            }
            return enText.ToString();
        }
    }
}
