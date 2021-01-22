using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace Vitae
{
    class FileSave
    {
        public static void Stream_Save(ObservableCollection<TreeElements> tree, string Path)
        {
            if (tree != null)
                if (Path != null)
                {
                    File.Delete(Path);
                    using (Stream stream = File.OpenWrite(Path))
                    {
                        XmlSerializer serialize = new XmlSerializer(typeof(ObservableCollection<TreeElements>));
                        serialize.Serialize(stream, tree);
                    }
                }
        }
        //
        public static void Stream_Open(ref ObservableCollection<TreeElements> tree, string Directory_Path, string File_Path)
        {
            if (Directory.Exists(Directory_Path))
                if (File.Exists(Directory_Path + @"\" + File_Path))
                {
                    using (Stream stream = File.OpenRead(Directory_Path + @"\" + File_Path))
                    {
                        if (stream.Length != 0)
                        {
                            XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<TreeElements>));
                            tree = (ObservableCollection<TreeElements>)deserializer.Deserialize(stream);

                        }
                    }
                }
                else
                {
                    File.Create(Directory_Path + @"\" + File_Path);
                    tree = new ObservableCollection<TreeElements>();
                }
            else
            {
                Directory.CreateDirectory(Directory_Path);
                File.Create(Directory_Path + @"\" + File_Path);
                tree = new ObservableCollection<TreeElements>();

            }
        }

        public static void Stream_Save(List<string> Sample, string Path)
        {
            if (Sample != null)
                if (Path != null)
                {
                    using (Stream stream = new FileStream(Path,FileMode.Create))
                    {
                        XmlSerializer serialize = new XmlSerializer(typeof(List<string>));
                        serialize.Serialize(stream, Sample);
                    }

                }
        }

        public static void Stream_Open(ref List<string> Sample, string Directory_Path, string File_Path)
        {

            if (Directory.Exists(Directory_Path))
                if (File.Exists(Directory_Path + @"\" + File_Path))
                {
                    using (Stream stream = File.OpenRead(Directory_Path + @"\" + File_Path)) 
                    { 
                        if (stream.Length != 0)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
                            Sample = (List<string>)serializer.Deserialize(stream);
                            if (Sample == null)
                            {
                            Sample = new List<string>();
                            }
                        }
                        else Sample = new List<string>();
                    }
                }
                else
                {
                    File.Create(Directory_Path + @"\" + File_Path);
                    Sample = new List<string>();
                }
            else
            {
                Directory.CreateDirectory(Directory_Path);
                File.Create(Directory_Path + @"\" + File_Path);
                Sample = new List<string>();
            }
        }

        public static void Stream_Save(int number, string Path)
        {
            if (number != 0)
                if (Path != null)
                {
                    using (Stream stream = new FileStream(Path, FileMode.Create))
                    {
                        XmlSerializer serialize = new XmlSerializer(typeof(int));
                        serialize.Serialize(stream, number);
                    }
                }
        }

        public static void Stream_Open(ref int number, string Path)
        {
            if (Path != null)
                if (File.Exists(Path))
                {
                    using (Stream stream = File.OpenRead(Path)) 
                    { 
                        if (stream.Length != 0)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(int));
                            number = (int)serializer.Deserialize(stream);
                        } 
                    }
                }
                else
                {
                    File.Create(Path);
                    number = 1;
                }
        }
        public static void SaveXamlPackage(string _fileName, RichTextBox richTextBox)
        {
            TextRange range;
            FileStream stream;
            range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            stream = new FileStream(_fileName, FileMode.Create);
            range.Save(stream, DataFormats.XamlPackage);
            stream.Close();
        }

        public static void LoadXamlPackage(string _fileName,RichTextBox richTextBox)
        {
            TextRange range;
            FileStream stream;
            if (File.Exists(_fileName))
            {
                range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                stream = new FileStream(_fileName, FileMode.OpenOrCreate);
                range.Load(stream, DataFormats.XamlPackage);
                stream.Close();
            }
        }


        public static List<ulong> Load_Ulong(string path)
        {
            List<ulong> values = new List<ulong>();
            Directory.CreateDirectory(path.Substring(0, path.LastIndexOf(@"\")));
            if (!File.Exists(path))
            {
                using (File.Create(path))
                {

                }
            }
            using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (stream.Length != 0 & values != null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<ulong>));
                    values = (List<ulong>)serializer.Deserialize(stream);

                }
                else
                    values = new List<ulong>();
            }
            return values;
        }

        public static void Save_Ulong(List<ulong> stat, string path)
        {

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ulong>));
                serializer.Serialize(stream, stat);
            }
        }
    }
}
