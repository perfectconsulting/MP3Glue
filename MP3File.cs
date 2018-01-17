using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Lame;

namespace MP3Glue
{
    class MP3File
    {
        string file = String.Empty;
        public FileInfo info = null;
        public WaveFormat format = null;

        public MP3File(string file)
        {
            if(!String.IsNullOrEmpty(file))
                if (File.Exists(file))
                {
                    info = new FileInfo(file);

                    using (Mp3FileReader reader = new Mp3FileReader(file))
                    {
                        this.file = file;
                        this.format = reader.WaveFormat;
                        reader.Close();
                    }
                }
        }



    }
}
