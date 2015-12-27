using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace OnlineExam.App_Code
{
    public class MyTool
    {
        //截取过长的字符串，并在后面加上"...";
        public static string ToShortString(string s, int maxLength)
        {
            if (s == null) return "";
            return s.Length < maxLength ? s : s.Substring(0, maxLength) + "...";

        }
        //将搜索字符串中的多个词组转换成字符串数组
        public static string[] GetSearchStringList(string key)
        {
            string[] s = key.Split(' ').Where(m => m.Trim() != "").ToArray();
            return s;
        }
        //些方法支持汉字  
        public static string Escape(string s)
        {
            if (s == null) return null;
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(s);

            for (int i = 0; i < byteArr.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(byteArr[i + 1].ToString("X2"));//把字节转换为十六进制的字符串表现形式  

                sb.Append(byteArr[i].ToString("X2"));
            }
            return sb.ToString();

        }
        //把JavaScript的escape()转换过去的字符串解释回来  
        //些方法支持汉字  
        public static string UnEscape(string s)
        {
            if (s == null) return null;
            string str = s.Remove(0, 2);//删除最前面两个＂%u＂  
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串＂%u＂分隔  
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
            {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16);  //把十六进制形式的字串符串转换为二进制字节  
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr); //把字节转为unicode编码  
            return str;

        }

        //public static string Escape(string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char c in str)
        //    {
        //        sb.Append((Char.IsLetterOrDigit(c)
        //        || c == '-' || c == '_' || c == ' '
        //        || c == '/' || c == '.') ? c.ToString() : Uri.HexEscape(c));
        //    }
        //    return sb.ToString();
        //}

        ////UnEscape
        ////unescape() 函数可对通过 escape() 编码的字符串进行解码。
        //public static string UnEscape(string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int len = str.Length;
        //    int i = 0;
        //    while (i != len)
        //    {
        //        if (Uri.IsHexEncoding(str, i))
        //            sb.Append(Uri.HexUnescape(str, ref i));
        //        else
        //            sb.Append(str[i++]);
        //    }
        //    return sb.ToString();
        //}


    }

}