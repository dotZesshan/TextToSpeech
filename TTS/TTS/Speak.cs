using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTS
{
    public partial class Speak : Form
    {
        Dictionary<string, string> _textToSoundMapping = new Dictionary<string, string>();
        
        public Speak()
        {
            InitializeComponent();
            Program.LoadSounds(_textToSoundMapping);
        }

        private Dictionary<string, string> ExtractMappings(string urduString)
        {
            var words = urduString.Split(new char[] { ' ' });
            var sounds = new Dictionary<string, string>();


            foreach (var word in words)
            {
                var soundstring = string.Empty;
                foreach (var harf in word)
                {
                    string sound;
                    if (_textToSoundMapping.TryGetValue(harf.ToString(), out sound))
                    {
                        soundstring += sound;
                    }
                }
                sounds.Add(word, soundstring);
            }
            return sounds;
        }

        private void ShowMapping(Dictionary<string, string> sounds)
        {
            listView1.Items.Clear();
            foreach (var mapping in sounds)
            {
                var listViewItem = new ListViewItem(mapping.Key);
                listViewItem.SubItems.Add(mapping.Value);
                listView1.Items.Add(listViewItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var sounds = ExtractMappings(textBox1.Text);
                ShowMapping(sounds);
                Talk(sounds);
            }
        }

        public void Talk(Dictionary<string, string> sounds)
        {
            foreach (var pair in sounds)
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.SetOutputToDefaultAudioDevice();

                PromptBuilder pb4 = new PromptBuilder();
                string str = "<phoneme alphabet=\"ipa\" ph=\"" + pair.Value + "\">" + pair.Key + "</phoneme>";
                pb4.AppendSsmlMarkup(str);
                synth.Rate = -5;
                synth.Speak(pb4);
            }
        }
    }
}
