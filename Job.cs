using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NAudio.Wave;
using NAudio.Lame;
using ID3;
using ID3.ID3v2Frames.BinaryFrames;


namespace MP3Glue
{

    public enum JobType { None, Convertion, Join, Tag };  
    public class Job
    {
        public class Tag
        {
            public string source;
            public string title;
            public string album;
            public string artist;
            public string genre;
            public byte track;
            public Image artwork;
            private IStatus status;

            public Tag(string source, string title, string album, string artist, string genre, byte track, Image artwork)
            {
                this.source = source;
                this.title = title;
                this.album = album;
                this.artist = artist;
                this.genre = genre;
                this.track = track;
                this.artwork = artwork;
            }

            public void Process(IStatus status)
            {
                this.status = status;
                status.Report("Updataing tags...");

                ID3Info info = new ID3Info(this.source, true);
                info.ID3v1Info.HaveTag = true;
                info.ID3v1Info.Title = Helper.String.Clip(this.title, 30);
                info.ID3v1Info.Artist = Helper.String.Clip(this.artist, 30);
                info.ID3v1Info.Album = Helper.String.Clip(this.album, 30);
                info.ID3v1Info.Genre = 255;
                info.ID3v1Info.Year = DateTime.Now.Year.ToString();
                info.ID3v1Info.Comment = string.Empty;
                info.ID3v1Info.TrackNumber = this.track;

                info.ID3v2Info.ClearAll();
                info.ID3v2Info.HaveTag = true;
                info.ID3v2Info.SetMinorVersion(3);
                info.ID3v2Info.SetTextFrame("TIT2", this.title);
                info.ID3v2Info.SetTextFrame("TPE1", this.artist);
                info.ID3v2Info.SetTextFrame("TALB", this.album);
                info.ID3v2Info.SetTextFrame("TCON", this.genre);
                info.ID3v2Info.SetTextFrame("TRCK", this.track.ToString());

                using (MemoryStream stream = Helper.Imaging.ImageToMemoryStream(this.artwork, System.Drawing.Imaging.ImageFormat.Jpeg))
                {

                    AttachedPictureFrame ATP = new AttachedPictureFrame(new FrameFlags(), "", TextEncodings.Ascii, "image/jpg", AttachedPictureFrame.PictureTypes.Cover_Front, stream);
                    info.ID3v2Info.AttachedPictureFrames.Add(ATP);

                    try
                    {
                        info.Save();
                    }
                    catch (Exception ex)
                    {
                        status.Report(String.Format("Unable to save tags. (\"{0}\")", ex.Message));
                    }
                }

                //info.Save();
            }
        }

        public class Conversion
        {
            public string source;
            public string destination;
            public WaveFormat format;
            private double aproxFileSize;
            private IStatus status;

            public Conversion(string source, string destination, WaveFormat format)
            {
                this.source = source;
                this.destination = destination;
                this.format = format;
            }

            public void Process(IStatus status)
            {
                this.status = status;
                status.Report(string.Format("Converting {0} to {1}", Path.GetFileName(this.source), this.format.ToString()));
                status.ResetProgress();
                string temp = Path.GetTempFileName() + ".wav";
                Helper.Audio.ConvertMp3ToWav(this.source, temp);
                File.Delete(this.source);
                Helper.Audio.ChangeWavFormat(temp, this.format);

                aproxFileSize = new System.IO.FileInfo(temp).Length;
                Helper.Audio.ConvertWavToMP3(temp, this.destination, ProgressHandler);
                File.Delete(temp);
            }

            public void ProgressHandler(object writer, long inputBytes, long outputBytes, bool finished)
            {
                if (finished)
                {
                    this.status.Progress(100);
                }
                else
                {
                    try
                    {
                        Console.WriteLine(String.Format("{0}  {1}", inputBytes.ToString(), outputBytes.ToString()));
                        double factor = ((double)inputBytes / (double)aproxFileSize) * 100;
                        this.status.Progress((int)factor);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public class Join
        {
            public List<string> source = null;
            public string destination;
            public WaveFormat format;
            private IStatus status;

            public Join(List<string>source, string destination, WaveFormat format)
            {
                this.source = source;
                this.destination = destination;
                this.format = format;
            }

            public void Process(IStatus status)
            {
                this.status = status;

                status.Report(string.Format("Starting join..."));

                Stream output = new FileStream(this.destination, FileMode.Create);

                status.ResetProgress();
                int i = 1;
                foreach (string file in this.source)
                {

                    status.Report(String.Format("Processing file {0}", Path.GetFileName(file)));
                    Mp3FileReader reader = new Mp3FileReader(file);

                    if ((output.Position == 0) && (reader.Id3v2Tag != null))
                    {
                        output.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                    }

                    Mp3Frame frame;
                    while ((frame = reader.ReadNextFrame()) != null)
                    {
                        output.Write(frame.RawData, 0, frame.RawData.Length);
                    }

                    reader.Close();
                    status.Progress(i / this.source.Count * 100);
                    i++;
                }

                status.Progress(100);
                output.Flush();
                output.Close();

                status.Report("Join compete");
            }
        }

        public JobType type = JobType.None;
        public Conversion conversion = null;
        public Join join = null;
        public Tag tag = null;

        public Job(Conversion conversion)
        {
            this.type = JobType.Convertion;
            this.conversion = conversion;
        }

        public Job(Join join)
        {
            this.type = JobType.Join;
            this.join = join;
        }
        
        public Job(Tag tag)
        {
            this.type = JobType.Tag;
            this.tag = tag;
        }
    }
}
