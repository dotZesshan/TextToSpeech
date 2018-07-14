using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TTS
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Speak());
        }

        public static void LoadSounds(Dictionary<string, string> textToSoundMapping)
        {
            XmlReader xReader = XmlReader.Create("urduPhoneme.xml");
            while (xReader.Read())
            {
                switch (xReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (string.Equals(xReader.Name, "phoneme"))
                        {
                            var sound = xReader["ph"];
                            var text = xReader.ReadInnerXml();
                            textToSoundMapping.Add(text, sound);
                            Console.WriteLine("Text: " + text + " |Sound: " + sound);
                        }
                        break;
                }
            }
            xReader.Close();
        }
    }
}
