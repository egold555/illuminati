using System;
using System.Media;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Net;

namespace illuminati
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //do everything in order
            playMusic();
            pictureBox1.Image = illuminati.Properties.Resources.scp;
            backgroundWorker1.RunWorkerAsync();
        }

       

        private void playMusic()
        {
            try
            {
                SoundPlayer sndplayr = new
                         SoundPlayer(illuminati.Properties.Resources.music);
                sndplayr.PlayLooping(); //loop the xfiles theme song
                

            }
            catch (Exception ex)
            {
                //for some reason if a error occures it puts it on the screen (this shouldent happen)
                MessageBox.Show(ex.Message + ": " + ex.StackTrace.ToString(),
                               "Error");
                return;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //exit the app when you push a key
            //crashes vhost
            //wonder why..
            Environment.Exit(0);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            String country, state, city, lon, lat, zip, ip;
            WebClient client = new WebClient();

            try
            {
                //get the users ip
                ip = client.DownloadString("https://api.ipify.org");

                //download evrtything needed
                country = client.DownloadString("http://ip-api.com/line/" + ip + "?fields=country");
                state = client.DownloadString("http://ip-api.com/line/" + ip + "?fields=regionName");
                city = client.DownloadString("http://ip-api.com/line/" + ip + "?fields=city");
                lon = client.DownloadString("http://ip-api.com/line/" + ip + "?fields=lon");
                lat = client.DownloadString("http://ip-api.com/line/" + ip + "?fields=lat");
                zip = client.DownloadString("http://ip-api.com/line/" + ip + "?fields=zip");
            }
            catch (Exception ex)
            {
                //if user isnt connected to internet cancel the speech
                // MessageBox.Show(ex.Message + ": " + ex.StackTrace.ToString(),  "Error");//optional error

                return;
            }


            // Initialize a new instance of the SpeechSynthesizer
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();

            //Set the volume to 100 because why not
            synth.Volume = 100;

            // Speak a string
            synth.Speak("We are the illuminati. You have entered the illuminati.");
            pictureBox1.Image = illuminati.Properties.Resources.eye_hand_horus_illuminati_indie_Favim_com_236960;
            synth.Speak("Now your life is in our hands.");
            pictureBox1.Image = illuminati.Properties.Resources.earth_clipart_black_and_white_aiek4Kzi4;
            synth.Speak("Your current location is" + city + state + country + "zipcode" + zip + "at approximately" + lon + lat + "degrees.");
            synth.Speak("Illuminati is watching.");
            pictureBox1.Image = illuminati.Properties.Resources.scp;
        }

    
    }
}
