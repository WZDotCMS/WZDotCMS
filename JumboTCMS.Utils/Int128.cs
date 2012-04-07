/*
 * 程序中文名称: 将博内容管理系统通用版
 * 
 * 程序英文名称: JumboTCMS
 * 
 * 程序版本: 5.2.X
 * 
 * 程序编写: 随风缘 (定制开发请联系：jumbot114#126.com,不接受免费的技术答疑,请见谅)
 * 
 * 官方网站: http://www.jumbotcms.net/
 * 
 * 商业服务: http://www.jumbotcms.net/about/service.html
 * 
 */

using System;
using System.Runtime.InteropServices;
namespace JumboTCMS.Utils
{


    [Serializable, StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Int128
    {
        public static readonly int Size;
        public long Lo;
        public long Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x10];
            Array.Copy(BitConverter.GetBytes(this.Lo), 0, destinationArray, 0, 8);
            Array.Copy(BitConverter.GetBytes(this.Hi), 0, destinationArray, 8, 8);
            return destinationArray;
        }

        public static Int128 ToInt128(byte[] buf, int startIndex)
        {
            Int128 num;
            num.Lo = BitConverter.ToInt64(buf, startIndex);
            num.Hi = BitConverter.ToInt64(buf, startIndex + 8);
            return num;
        }

        static Int128()
        {
            Size = 0x10;
        }
    }
}

