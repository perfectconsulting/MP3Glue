using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NAudio.Wave;
using NAudio.Lame;

namespace Helper
{
    static public class String
    {
        public static string ppByteSize(long n)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (n >= 1024 && ++order < sizes.Length)
                n = n / 1024;

            return (string.Format("{0:0.##}{1}", n, sizes[order]));
        }

        public static string Random(int n)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < n; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string Clip(string source, int length)
        {
            if (source.Length > length)
                source = source.Substring(0, length);
            return source;
        }

        public static string Normalise(string text)
        {
            const string filter="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ !\"£$%^&*()_+-{}:@,.;'.,/?\\#~";
            string pad = string.Empty;
            foreach(char c in text.ToArray<char>())
            {
                if (filter.IndexOf(c) >= 0)
                    pad += c;
            }

            return (pad);
        }
    }

    static public class Files
    {
        public static long FileSize(string path)
        {
            try
            {
                if(!File.Exists(path))
                    return 0;

                FileInfo info = new FileInfo(path);
                return info.Length;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }
    }

    static public class Lists
    {
        public static List<List<T>> SplitList<T>(IEnumerable<T> values, int groupSize, int? maxCount = null)
        {
            List<List<T>> result = new List<List<T>>();
            // Quick and special scenario
            if (values.Count() <= groupSize)
            {
                result.Add(values.ToList());
            }
            else
            {
                List<T> valueList = values.ToList();
                int startIndex = 0;
                int count = valueList.Count;
                int elementCount = 0;

                while (startIndex < count && (!maxCount.HasValue || (maxCount.HasValue && startIndex < maxCount)))
                {
                    elementCount = (startIndex + groupSize > count) ? count - startIndex : groupSize;
                    result.Add(valueList.GetRange(startIndex, elementCount));
                    startIndex += elementCount;
                }
            }


            return result;
        }
    }

    static public class Threading
    {
        public static bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }
    }

    static public class Imaging
    {
        public static MemoryStream ImageToMemoryStream(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, format);
            return ms;
        }
    }

    static public class Audio
    {
        public static string ppWaveFormat(WaveFormat format)
        {
            return(string.Format("{0}bits {1}KHz {2} channels", format.BitsPerSample, format.SampleRate/1000, format.Channels));
        }
 
        public static WaveFormat GetWavFormat(string file)
        {
            if (!File.Exists(file))
                return null;

            using (WaveFileReader reader = new WaveFileReader(file))
            {
                WaveFormat wavForamt = reader.WaveFormat;
                reader.Close();
                return wavForamt;
            }
        }

        public static bool ChangeWavFormat(string wavFile, WaveFormat wavFormat)
        {
            try
            {
                string tempFile = Path.GetTempFileName() + ".wav";
                File.Move(wavFile, tempFile);

                using (var reader = new WaveFileReader(tempFile))
                using (var conversionStream = new WaveFormatConversionStream(wavFormat, reader))
                {
                    WaveFileWriter.CreateWaveFile(wavFile, conversionStream);
                    conversionStream.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
             
        public static bool ConvertMp3ToWav(string mp3File, string wavFile)
        {
            try
            {
                using (Mp3FileReader mp3 = new Mp3FileReader(mp3File))
                {
                    using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                    {
                        WaveFileWriter.CreateWaveFile(wavFile, pcm);
                        pcm.Close();
                    }
                    mp3.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ConvertWavToMP3(string wavFile, string mp3file, Action<object,long,long,bool> handler)
        {
            using (WaveFileReader rdr = new WaveFileReader(wavFile))
            using (LameMP3FileWriter wtr = new LameMP3FileWriter(mp3file, Helper.Audio.GetWavFormat(wavFile), LAMEPreset.VBR_90))
            {

                wtr.OnProgress += new NAudio.Lame.ProgressHandler( handler);
                rdr.CopyTo(wtr);
                rdr.Close();
                wtr.Close();
            }
            return true;
        }

    }
}
