using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAutoEvent.API
{
    public class CustomPaths
    {
        public static string MainFolder => Path.Combine(LabApi.Loader.Features.Paths.PathManager.LabApi.ToString(), "AutoEvent");
        public static string TranslateFolder => Path.Combine(MainFolder, "Translates");
        public static string MusicFolder => Path.Combine(MainFolder, "Music");
        public static void InitAll()
        {
            if(!Directory.Exists(MusicFolder))
            {
                Directory.CreateDirectory(MusicFolder);
            }
            if(!Directory.Exists(MusicFolder))
            {
                Directory.CreateDirectory(MusicFolder);
            }
            if(!Directory.Exists(TranslateFolder))
            {
                Directory.CreateDirectory(TranslateFolder);
            }
        }
    }
}
