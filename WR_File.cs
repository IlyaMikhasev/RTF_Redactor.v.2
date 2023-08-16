using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace RTF_Redactor.v._2
{
    [Serializable]
    public class WR_File
    {
        [XmlAttribute]
        public string _path;
        public WR_File() { }
        public string OpenFile()
        {
            using (OpenFileDialog of = new OpenFileDialog())
            {
                of.Filter = "RTF files (*.rtf)|*.rtf";
                of.InitialDirectory = Environment.CurrentDirectory;

                if (of.ShowDialog() == DialogResult.OK)
                {
                    return of.FileName;
                }
            }
            return "none";
        }
        public string SaveFile(object sender) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "RTF files (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return string.Empty;
            string filename = saveFileDialog.FileName;
            ((RichTextBox)sender).SaveFile(filename);
            return filename;
        }
        static public void Serealize_it(string filename, List<WR_File> objectGrath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<WR_File>));
            using (Stream fStream = new FileStream(filename,
                FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fStream, objectGrath);
            }
        }
        static public void Deserealize_it(string filename, out List<WR_File> lst)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<WR_File>));
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    lst = (List<WR_File>)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                lst = new List<WR_File>();
            }
        }
    }
    [Serializable]
    public class Notes {
        [XmlAttribute]
        public string _note;
        public Notes() { }
        public Notes(string note)
        {
            _note = note;
        }
        static public void Serealize_it(string filename, List<Notes> objectGrath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Notes>));
            using (Stream fStream = new FileStream(filename,
                FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fStream, objectGrath);
            }
        }
        static public void Deserealize_it(string filename, out List<Notes> lst)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Notes>));
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    lst = (List<Notes>)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                lst = new List<Notes>();
            }
        }
    }
}
