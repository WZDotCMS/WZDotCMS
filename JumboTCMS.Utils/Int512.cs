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
    public struct Int512
    {
        public static readonly int Size;
        public Int256 Lo;
        public Int256 Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x40];
            Array.Copy(this.Lo.GetBytes(), 0, destinationArray, 0, 0x20);
            Array.Copy(this.Hi.GetBytes(), 0, destinationArray, 0x20, 0x20);
            return destinationArray;
        }

        public static Int512 ToInt512(byte[] buf, int startIndex)
        {
            Int512 num;
            num.Lo = Int256.ToInt256(buf, startIndex);
            num.Hi = Int256.ToInt256(buf, startIndex + 0x20);
            return num;
        }

        static Int512()
        {
            Size = 0x40;
        }
    }
}

