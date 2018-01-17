using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Threading;
using ID3;
using ID3.ID3v2Frames.BinaryFrames;
using System.Reflection;
using NAudio.Wave;

namespace MP3Glue
{
    public interface IStatus
    {
        void Report(string text);
        void Blank();
        void Finished();
        void Progress(int value);
        void ResetProgress();
    }

    public partial class MainForm : Form, MP3Glue.IStatus
    {

        public class FormatMetric
        {
            public int          value;
            public WaveFormat   format;
            public int          group;

            public FormatMetric(int value, WaveFormat format)
            {
                this.value = value;
                this.format = format;
                this.group = 0;
            }
        }

        public Dictionary<string, FormatMetric> formats = new Dictionary<string,FormatMetric>();
        public AudioEngine engine = new AudioEngine(); 
        public static MainForm thisForm;
        private List<MP3File> fileQueue = new List<MP3File>();
        private string tempFolder;


        public MainForm()
        {
            thisForm = this;
            InitializeComponent();

            for (int i = 1; i < 99; i++)
                comboTrack.Items.Add(i);
            
            foreach (int bits in new List<int>() { 16, 32 })
            {
                foreach (int rate in new List<int>() { 8000, 22050, 44100 })
                {
                   foreach (int channels in new List<int>() { 1, 2 })
                   {
                       WaveFormat format = new WaveFormat(rate,bits,channels);
                       formats.Add(Helper.Audio.ppWaveFormat(format), new FormatMetric(0,format));
                       comboConversion.Items.Add(Helper.Audio.ppWaveFormat(format));
                   }
                }
            }

            this.Text = String.Format("MP3Glue v{0}.{1}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(), Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()); 
        }

        private bool listFileContains(string file)
        {
            foreach(ListViewItem item in listFile.Items)
                if(item.SubItems[3].Text == file)
                    return(true);

            return(false);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog PickFile = new OpenFileDialog();
            PickFile.Multiselect = true;
            PickFile.Filter = "MP3 Files (.mp3)|*.mp3";
            PickFile.FilterIndex = 1;
 
            if (PickFile.ShowDialog() == DialogResult.OK)
            {
                Application.UseWaitCursor = true;
                foreach(String file in PickFile.FileNames)
                {
                    if (listFileContains(file))
                        continue;

                    Application.DoEvents();

                    MP3File mp3file = new MP3Glue.MP3File(file);
                    fileQueue.Add(mp3file);
                    
                    FileInfo info = new FileInfo(file);
                    ListViewItem item = new ListViewItem(Path.GetFileName(file));
                    item.SubItems.Add(Helper.String.ppByteSize(mp3file.info.Length));
                    item.SubItems.Add(Helper.Audio.ppWaveFormat(mp3file.format));
                    item.SubItems.Add(file);
                    listFile.Items.Add(item);

                    if (formats.ContainsKey(Helper.Audio.ppWaveFormat(mp3file.format)))
                    {
                        formats[Helper.Audio.ppWaveFormat(mp3file.format)].value++;
                    }
                    else
                    {
                        formats.Add(Helper.Audio.ppWaveFormat(mp3file.format), new FormatMetric(1, mp3file.format));
                        comboConversion.Items.Add(Helper.Audio.ppWaveFormat(mp3file.format));
                    }
 
                    updateTagView(file);
                    Application.DoEvents();
                }

                Dictionary<string, long> coloured = new Dictionary<string, long>();

                if (formats.Count > 0)
                {
                    int i = 1;
                    foreach (var item in formats.OrderByDescending(r => r.Value.value))
                    {
                        if (i == 1)
                        {
                            comboConversion.SelectedIndex = comboConversion.FindStringExact(item.Key);
                        }

                        formats[item.Key].group = i++;
                    }
                }

                foreach (ListViewItem item in listFile.Items)
                {
                    string key = item.SubItems[2].Text;
                    switch (formats[key].group)
                    {
                        case 1:
                            item.BackColor = Color.LightGreen;
                            break;

                        case 2:
                            item.BackColor = Color.LightYellow;
                            break;

                        case 3:
                            item.BackColor = Color.Pink;
                            break;

                        default:
                            item.BackColor = Color.Red;
                            break;
                    }


                    Console.WriteLine(item.SubItems[2]);
                }

                

                Application.UseWaitCursor = false;

                listFile.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //Cursor.Current = Cursors.Default;
            }

        }

        private void updateTagView(string file)
        {
            try
            {
                ID3Info info = new ID3Info(file, true);

                string text = Helper.String.Normalise(info.ID3v2Info.GetTextFrame("TIT2"));
                if (!String.IsNullOrEmpty(text))
                {
                    if (text != textTitle.Text)
                        if (textTitle.Text == String.Empty)
                            textTitle.Text = text;
                        else
                            textTitle.ForeColor = Color.Red;
                }

                text = Helper.String.Normalise(info.ID3v2Info.GetTextFrame("TPE1"));
                if (!String.IsNullOrEmpty(text))
                {
                    if (text != textArtist.Text)
                        if (textArtist.Text == String.Empty)
                            textArtist.Text = text;
                        else
                            textArtist.ForeColor = Color.Red;
                }

                text = Helper.String.Normalise(info.ID3v2Info.GetTextFrame("TALB"));
                if (!String.IsNullOrEmpty(text))
                {
                    if (text != textAlbum.Text)
                        if (textAlbum.Text == String.Empty)
                            textAlbum.Text = text;
                        else
                            textAlbum.ForeColor = Color.Red;
                }

                text = Helper.String.Normalise(info.ID3v2Info.GetTextFrame("TCON"));
                if (!String.IsNullOrEmpty(text))
                {
                    if (text != textGenre.Text)
                        if (textGenre.Text == String.Empty)
                            textGenre.Text = text;
                        else
                            textGenre.ForeColor = Color.Red;
                }

                text = Helper.String.Normalise(info.ID3v2Info.GetTextFrame("TRCK"));
                if (String.IsNullOrEmpty(text))
                {
                    comboTrack.Text = "1";
                }
                else
                {
                    if (String.IsNullOrEmpty(comboTrack.Text))
                    {
                        int n = int.Parse(text);
                        if (n > 0 && comboTrack.Text == string.Empty)
                            comboTrack.Text = n.ToString();
                    }
                }

                if (pictureArtwork.Image == null)
                {
                    List<Frame> frames = new List<Frame>();
                    foreach (AttachedPictureFrame ATP in info.ID3v2Info.AttachedPictureFrames.Items)
                        if (ATP.PictureType == AttachedPictureFrame.PictureTypes.Cover_Front)
                            ViewPicture(ATP);
                }
             }
            catch(Exception ex)
            {
            }
        }

        private bool ViewPicture(AttachedPictureFrame AP)
        {
            try
            {
                pictureArtwork.Image = Image.FromStream(AP.Data);
                if (pictureArtwork.Image.Height < pictureArtwork.Height && pictureArtwork.Image.Width < pictureArtwork.Width) 
                    pictureArtwork.SizeMode = PictureBoxSizeMode.CenterImage;
                else
                    pictureArtwork.SizeMode = PictureBoxSizeMode.Zoom;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void listFile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listFile.SelectedItems)
                listFile.Items.Remove(item);
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            WaveFormat format = formats[comboConversion.Text].format;

            if (listFile.Items.Count == 0)
            {
                SystemSounds.Beep.Play();
                return;
            }

            if (buttonJoin.Text == "Finish")
            {
                this.Close();
                return;
            }

            if (buttonJoin.Text == "Cancel")
            {
                engine.Abort();
                Report("Join terminated.");
                buttonJoin.Text = "Finish";
                return;
            }

            SaveFileDialog PickFile = new SaveFileDialog();
            PickFile.Filter = "MP3 Files (.mp3)|*.mp3";
            PickFile.FilterIndex = 1;
            PickFile.RestoreDirectory = true;

            if (PickFile.ShowDialog() != DialogResult.OK)
                return;

            string mp3File = PickFile.FileName;

            buttonAdd.Enabled = false;
            buttonRemove.Enabled = false;
            buttonUp.Enabled = false;
            buttonDown.Enabled = false;
            buttonDelete.Enabled = false;
            comboConversion.Enabled = false;

            //buttonJoin.Enabled = false;

            tabFileInfo.SelectedTab = tabStatus;

            do
            {
                tempFolder = Path.GetTempPath() + "MP3_Glue_" + Helper.String.Random(24);
            } while (Directory.Exists(tempFolder));

            Directory.CreateDirectory(tempFolder);

            List<Job> jobs = new List<Job>();

            List<string> tempFiles = new List<string>();

            foreach (ListViewItem item in listFile.Items)
            {
                string tempFile = tempFolder + "\\" + item.SubItems[0].Text;
                tempFiles.Add(tempFile);
                File.Copy(item.SubItems[3].Text, tempFile);
 
                if (item.SubItems[2].Text != comboConversion.Text){
                    jobs.Add(new Job(new Job.Conversion(tempFile, tempFile, format)));
                }
            }

            jobs.Add(new Job(new Job.Join(tempFiles, mp3File, format)));

            string title = textTitle.Text;
            string album = textAlbum.Text;
            string artist = textArtist.Text;
            string genre = textGenre.Text;
            byte track = String.IsNullOrEmpty(comboTrack.Text) ? (Byte)1 : Byte.Parse(comboTrack.Text); 

            jobs.Add(new Job(new Job.Tag(mp3File, title, album, artist,genre,track, pictureArtwork.Image)));

            buttonJoin.Text = "Cancel";
            this.engine.ProcessJobs(jobs, this);
        }

        public void Report(string text)
        {
            try
            {
                //Check if invoke requied if so return - as i will be recalled in correct thread
                if (Helper.Threading.ControlInvokeRequired(textStatus, () => Report(text))) return;
                textStatus.AppendText(text + Environment.NewLine);
            }
            catch (Exception ex)
            {
            }
        }

        public void Blank()
        {
           Report(String.Empty);
        }

        public void Finished()
        {
            if (Helper.Threading.ControlInvokeRequired(buttonJoin, () => Finished())) return;
            Report("Finished...");
            buttonJoin.Text = "Finish";
        }

        public void ResetProgress()
        {
            if (Helper.Threading.ControlInvokeRequired(progressBar, () => ResetProgress())) return;
            progressBar.Value = 0;
        }

        public void Progress(int value)
        {
            if (Helper.Threading.ControlInvokeRequired(progressBar, () => Progress(value))) return;
            progressBar.Value = value;
        }

        private void textTitle_TextChanged(object sender, EventArgs e)
        {
            textTitle.ForeColor = Color.Black;
        }

        private void textArtist_TextChanged(object sender, EventArgs e)
        {
            textArtist.ForeColor = Color.Black;
        }

        private void textAlbum_TextChanged(object sender, EventArgs e)
        {
            textAlbum.ForeColor = Color.Black;
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if(listFile.SelectedItems.Count == 0)
            {
                SystemSounds.Beep.Play();
                return;
            }

            foreach (ListViewItem item in listFile.SelectedItems)
            {
                if (item.Index > 0)
                {
                    int index = item.Index - 1;
                    listFile.Items.RemoveAt(item.Index);
                    listFile.Items.Insert(index, item);
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (listFile.SelectedItems.Count == 0)
            {
                SystemSounds.Beep.Play();
                return;
            }

            foreach (ListViewItem item in listFile.SelectedItems)
            {
                if (item.Index < listFile.Items.Count - 1)
                {
                    int index = item.Index + 1;
                    listFile.Items.RemoveAt(item.Index);
                    listFile.Items.Insert(index, item);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listFile.SelectedItems.Count == 0)
            {
                SystemSounds.Beep.Play();
                return;
            }

            foreach (ListViewItem item in listFile.SelectedItems)
                listFile.Items.RemoveAt(item.Index);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!string.IsNullOrEmpty(tempFolder)){
                try
                {
                    Directory.Delete(tempFolder, true);
                }
                catch(Exception ex)
                {
                }
            }
        }

        private void buttonArtwork_Click(object sender, EventArgs e)
        {
            OpenFileDialog PickFile = new OpenFileDialog();
            PickFile.Filter = "Image Files|*.bmp;*.jpg;*.gif";
            PickFile.FilterIndex = 1;
            PickFile.RestoreDirectory = true;

            if (PickFile.ShowDialog() != DialogResult.OK)
                return;

            pictureArtwork.Load(PickFile.FileName);
            
        }
    }
}
