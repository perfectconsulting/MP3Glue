using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;
using NAudio.Lame;
using System.Diagnostics;
using System.Drawing;
using ID3;
using ID3.ID3v2Frames.BinaryFrames;
using System.Threading;


namespace MP3Glue
{
    public class AudioEngine
    {

        private double aproxFileSize;

        private List<Job> jobs = null;
        private Thread background = null;
        private IStatus status = null;
 
        public void ProcessJobs(List<Job> jobs, IStatus status)
        {
            this.jobs = jobs;
            this.status = status;
            background = new Thread(_ProcessJobs);
            background.Priority = ThreadPriority.Highest;
            background.Start();
        }

        public void Abort()
        {
            if (background != null)
                background.Abort();
        }

        private void _ProcessJobs()
        {
            foreach (Job job in jobs)
            {
                switch (job.type)
                {
                    case JobType.Convertion:
                        job.conversion.Process(this.status);
                        break;

                    case JobType.Join:
                        job.join.Process(this.status);
                        break;

                    case JobType.Tag:
                        job.tag.Process(this.status);
                        break;

                    case JobType.None:
                        break;
                }
            }

            status.Finished();
        }

        /*
        public class MP3Struct
        {
            public string OutputFile;
            public string Title;
            public string Artist;
            public string Album;
            public string Genre;
            public Byte Track;
            public Image Artwork; 
        }

        public MP3Struct mp3Info;
        private IReportBack Status = null;
        public List<string> wavFiles = null;
        public string tempFolder = String.Empty;

        public long aproxFileSize = 0;
        public SampleMethods SampleMathod = SampleMethods.E;
        public Thread backgroundThread = null;

        public void JoinMP3FilesInBackground(MP3Struct info, List<string>files, IReportBack report)
        {
            Status = report;
            mp3Info = info;
            wavFiles = files;

            mp3Info = info;
            backgroundThread = new Thread(_JoinMP3Files);
            backgroundThread.Priority = ThreadPriority.Highest;
            backgroundThread.Start();
        }

        public void JoinMP3FileShare(MP3Struct info)
        {
            mp3Info = info;
            _JoinMP3Files();
        }

        public void AbortJoin()
        {
            if (backgroundThread != null)
                backgroundThread.Abort();
        }

        private void _JoinMP3Files()
        {

            try
            {
                List<string> trash = new List<string>();
 
                do
                {
                    tempFolder = Path.GetTempPath() + "MP3_Glue_" + Helper.String.Random(24);
                } while (Directory.Exists(tempFolder));

                Directory.CreateDirectory(tempFolder);

                List<string> wavfiles = new List<string>();

                Status.Report("Staring primary convertion...");
                // make wav versions of each file
                Status.ResetProgress();
                double factor = (double)100 / (double)wavFiles.Count;
                double n = 0;
                foreach (string mp3file in wavFiles)
                {
                    Status.Progress((int)(factor * n));
                    string wavfile = Path.Combine(tempFolder, Path.GetFileNameWithoutExtension(mp3file) + ".wav");
                    Status.Report(String.Format("converting \"{0}\" to wav", Path.GetFileName(mp3file)));
                    if (Helper.Audio.ConvertMp3ToWav(mp3file, wavfile))
                    {
                        wavfiles.Add(wavfile);
                        trash.Add(wavfile);
                    }
                    n++;
                    Status.Progress((int)(factor * n));
                }

                Status.Blank();

                WaveFormat bestFormat = null;
                WaveFormat worstFormat = null;
                WaveFormat useFormat = null;

                Status.Report("Analysing audio...");

                const long maxSize = 1024L * 1024L * 1024L * 2;
                long totalSize = 0;

                foreach (string file in wavfiles)
                {
                    WaveFormat thisFormat = Helper.Audio.GetWavFormat(file);
                    Status.Report(String.Format("{0} @ {1}", Path.GetFileName(file), thisFormat));

                    if (bestFormat == null)
                        bestFormat = thisFormat;

                    if (worstFormat == null)
                        worstFormat = thisFormat;

                    if (thisFormat.BitsPerSample < worstFormat.BitsPerSample)
                        worstFormat = thisFormat;
                    else
                        if (thisFormat.SampleRate < worstFormat.SampleRate)
                            worstFormat = thisFormat;
                        else
                            if (thisFormat.Channels < worstFormat.Channels)
                                worstFormat = thisFormat;

                    if (thisFormat.BitsPerSample > bestFormat.BitsPerSample)
                        bestFormat = thisFormat;
                    else
                        if (thisFormat.SampleRate > bestFormat.SampleRate)
                            bestFormat = thisFormat;
                        else
                            if (thisFormat.Channels > bestFormat.Channels)
                                bestFormat = thisFormat;

                    totalSize += Helper.Files.FileSize(file);
                }

                switch (SampleMathod)
                {
                    case SampleMethods.Smart:
                        if (bestFormat == worstFormat)
                        {
                            useFormat = bestFormat;
                            Status.Report("using existing sampling " + useFormat.ToString());
                        }
                        else
                        {
                            int upsampleFiles = 0;
                            foreach (string wavfile in wavfiles)
                            {
                                WaveFormat thisWavFormat = Helper.Audio.GetWavFormat(wavfile);
                                if (thisWavFormat.ToString() != bestFormat.ToString())
                                    upsampleFiles++;
                            }

                            int downsampleFiles = 0;
                            foreach (string wavfile in wavfiles)
                            {
                                WaveFormat thisWavFormat = Helper.Audio.GetWavFormat(wavfile);
                                if (thisWavFormat.ToString() != worstFormat.ToString())
                                    downsampleFiles++;
                            }

                            if (upsampleFiles < downsampleFiles)
                                useFormat = bestFormat;
                            else
                                useFormat = worstFormat;

                            Status.Report("smart sampling to " + useFormat.ToString());
                        }

                        break;

                    case SampleMethods.Upsample:
                        useFormat = bestFormat;
                        Status.Report("upsampling to " + useFormat.ToString());
                        break;
                    
                    case SampleMethods.Downsample:
                        useFormat = worstFormat;
                        Status.Report("downsampling to " + useFormat.ToString());
                        break;
                }

                Status.Blank();

                long numberOfParts = 1;

                if (totalSize > maxSize)
                {
                    numberOfParts = (totalSize - 1) / maxSize + 1;
                    Status.Report(String.Format("WARNING! The intermediate file size exceeds 2Gb. The MP3 file will be split into {0} parts.", numberOfParts));
                    Status.Blank();
                }
 
                List<string> normaliseFiles = new List<string>();
                foreach (string wavfile in wavfiles)
                {
                    WaveFormat thisWavFormat = Helper.Audio.GetWavFormat(wavfile);
                    Console.WriteLine(thisWavFormat.ToString() + " " + useFormat.ToString());
                    if(thisWavFormat.ToString() != useFormat.ToString())
                        normaliseFiles.Add(wavfile);
                }

                if (normaliseFiles.Count() > 0)
                {
                    Status.Report("Staring secondary convertion...");
                    Status.ResetProgress();
                    factor = (double)100 / (double)normaliseFiles.Count;
                    n = 0;
                    foreach (string wavfile in normaliseFiles)
                    {
                        Status.Report(String.Format("resampling file {0} @ {1}", Path.GetFileName(wavfile), useFormat));
                        Helper.Audio.ChangeWavFormat(wavfile, useFormat );
                        n++;
                        Status.Progress((int)(factor * n));
                    }

                    Status.Blank();
                }

                string tempFile = String.Empty;

                if (numberOfParts == 1)
                {
                    do
                    {   
                        tempFile = Path.Combine(tempFolder, Helper.String.Random(24) + ".wav");
                    } while (File.Exists(tempFile));

                    trash.Add(tempFile);

                    JoinWavFiles(tempFile, useFormat, wavfiles);

                    Status.Blank();
                    Status.Report(String.Format("Generating \"{0}\"", mp3Info.OutputFile));

                    Status.ResetProgress();
                    aproxFileSize = new System.IO.FileInfo(tempFile).Length;
                    Helper.Audio.ConvertWavToMP3(tempFile, mp3Info.OutputFile, useFormat, ProgressHandler);

                    updateID3Tags(mp3Info.OutputFile);

                    Status.Blank();
                    Status.Report(String.Format("Join Complete."));
                    Status.Blank();
                }
                else
                {
                    long blockSize = (wavfiles.Count() - 1)/ numberOfParts + 1;
                    List<List<string>>blocks = Helper.Lists.SplitList(wavfiles, (int)blockSize);

                    int i = 1;
                    List<string> parts = new List<string>();
                    foreach (List<string> block in blocks)
                    {
                        do
                        {   
                            tempFile = Path.Combine(tempFolder, Helper.String.Random(24) + ".wav");
                        } while (File.Exists(tempFile));

                        trash.Add(tempFile);
                        Status.Report(String.Format("Preparing Part {0} \"{1}\"", i++, Path.GetFileName(tempFile)));
                        JoinWavFiles(tempFile, useFormat, block);
                        parts.Add(tempFile);
                        Status.Blank();
                    }

                    i = 1;

                    foreach (string part in parts)
                    {
                        string mp3File = MakePartFileName(mp3Info.OutputFile, i++);

                        Status.Report(String.Format("Generating \"{0}\"", mp3File));

                        Status.ResetProgress();
                        aproxFileSize = new System.IO.FileInfo(part).Length;

                        Helper.Audio.ConvertWavToMP3(part, mp3File, useFormat, ProgressHandler);

                        updateID3Tags(mp3File);
                        mp3Info.Track++;
                    }

                   Status.Blank();
                   Status.Report(String.Format("Join Complete."));
                }

                foreach (string file in trash)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Status.Report(ex.Message);
                    }
                }

                try
                {
                    Directory.Delete(tempFolder);
                }
                catch (Exception ex)
                {
                }


                Status.Finished();
            }
            catch (Exception ex)
            {
                Status.Report(ex.Message);
            }
        }

        public void ProgressHandler(object writer, long inputBytes, long outputBytes, bool finished)
        {
            if (finished)
            {
                Status.Progress(100);
            }
            else
            {
                try
                {
                    Console.WriteLine(String.Format("{0}  {1}", inputBytes.ToString(), outputBytes.ToString()));
                    double factor = ((double)inputBytes / (double)aproxFileSize) * 100;
                    Status.Progress((int)factor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private bool SmartJoinWavFiles(string outputFile, WaveFormat wavFormat, IEnumerable<string> wavFiles)
        {
            if (wavFiles.Count() > 100)
            {
                List<string> multiParts= new List<string>();

                List<List<string>> partsList = Helper.Lists.SplitList(wavFiles, 100);
                Status.Report("Staring multi-part join...");
                foreach(List<string> part in partsList)
                {
                    string tempfile = Path.GetTempFileName() + ".wav";
                    multiParts.Add(tempfile);

                    if(!JoinWavFiles(tempfile, wavFormat, part))
                        return false;
                }

                Status.Report("Staring final join...");

                return JoinWavFiles(outputFile, wavFormat, multiParts);
            }
            else
            {
                Status.Report("Staring join...");
                return JoinWavFiles(outputFile, wavFormat, wavFiles);
            }
        }

        private bool JoinWavFiles(string outputFile, WaveFormat wavFormat, IEnumerable<string> wavFiles)
        {
            byte[] buffer = new byte[1024];
            try
            {
                using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputFile, wavFormat))
                {
                    Status.Report("Staring join...");
                    Status.ResetProgress();
                    double factor = (double)100 / (double)wavFiles.Count();
                    double n = 0;
                    foreach (string sourceFile in wavFiles)
                    {
                        Status.Report(String.Format("processing file \"{0}\"", Path.GetFileName(sourceFile)));
                        using (WaveFileReader reader = new WaveFileReader(sourceFile))
                        {
                            n++;
                            Status.Progress((int)(factor * n));
                            if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                                return false;

                            int read;
                            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                                waveFileWriter.Write(buffer, 0, read);
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private string MakePartFileName(string file, int part)
        {
            string parent = Path.GetDirectoryName(file);
            string name = Path.GetFileNameWithoutExtension(file);

            name += String.Format(" - Part {0}", part) + Path.GetExtension(file);

            return Path.Combine(parent,name);
        }

        private bool updateID3Tags(string file)
        {
            try
            {
                ID3Info info = new ID3Info(file, true);
                info.ID3v1Info.HaveTag = true;
                info.ID3v1Info.Title = Helper.String.Clip(mp3Info.Title, 30);
                info.ID3v1Info.Artist = Helper.String.Clip(mp3Info.Artist, 30);
                info.ID3v1Info.Album = Helper.String.Clip(mp3Info.Album, 30);
                info.ID3v1Info.Genre = 255;
                info.ID3v1Info.Year = DateTime.Now.Year.ToString();
                info.ID3v1Info.Comment = string.Empty;
                info.ID3v1Info.TrackNumber = mp3Info.Track;

                info.ID3v2Info.ClearAll();
                info.ID3v2Info.HaveTag = true;
                info.ID3v2Info.SetMinorVersion(3);
                info.ID3v2Info.SetTextFrame("TIT2", mp3Info.Title);
                info.ID3v2Info.SetTextFrame("TPE1", mp3Info.Artist);
                info.ID3v2Info.SetTextFrame("TALB", mp3Info.Album);
                info.ID3v2Info.SetTextFrame("TCON", mp3Info.Genre);
                info.ID3v2Info.SetTextFrame("TRCK", mp3Info.Track.ToString());

                using (MemoryStream stream = Helper.Imaging.ImageToMemoryStream(mp3Info.Artwork, System.Drawing.Imaging.ImageFormat.Jpeg))
                {

                    AttachedPictureFrame ATP = new AttachedPictureFrame(new FrameFlags(), "", TextEncodings.Ascii, "image/jpg", AttachedPictureFrame.PictureTypes.Cover_Front, stream);
                    info.ID3v2Info.AttachedPictureFrames.Add(ATP);
                    info.Save();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }*/
    }
}
