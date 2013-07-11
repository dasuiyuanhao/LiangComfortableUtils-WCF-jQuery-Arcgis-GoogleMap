using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace LiangComUtils.Web.App_Code
{
    public class OperateMthods
    {
        /// <summary>d
        /// 保存日志
        /// </summary>
        /// <param name="note"></param>
        public static void SaveNote(string note)
        {
            FileStream stream = new FileStream(OperateMthods.GetLogDirectory("Common") + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.Delete | FileShare.ReadWrite);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("================================================================");
            writer.WriteLine(string.Format("Note:\t\n{0}", note));
            writer.WriteLine(string.Format("DateTime:\t{0}\r\n\r\n", DateTime.Now.ToShortTimeString()));
            stream.Flush();
            writer.Close();
            stream.Close();
            stream.Dispose();
            writer.Dispose();
        }

        /// <summary>
        /// 日志目录
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string GetLogDirectory(string category)
        {
            string baseDirectory = string.Empty;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if ((baseDirectory[baseDirectory.Length - 1] != '/') && (baseDirectory[baseDirectory.Length - 1] != '\\'))
            {
                baseDirectory = baseDirectory + @"\";
            }
            baseDirectory = string.Format(@"{0}Log\{1}\", baseDirectory, category);
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
            return baseDirectory;
        }
    }
}