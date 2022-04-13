using System;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace TarefasBackEnd
{
    public static class SecretCompartilhada
    {
        public static string Secret
        {
            get
            {
                string path = Path.Combine(Environment.CurrentDirectory, @"Secret_Key.txt").ToString();
                var CurrentDirectory = Environment.CurrentDirectory;

                using (StreamReader streamReader = File.OpenText(path))
                {
                    return streamReader.ReadLine();
                }
            }
        }
    }
}
