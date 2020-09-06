using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;

public static class SavePlayInFile
{
    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }

    /// <summary>
    /// Reads an object instance from a binary file.
    /// </summary>
    /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the binary file.</returns>
    public static T ReadFromBinaryFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }

    private static DirectoryInfo GetDirInfo()
    {
        var path = Application.persistentDataPath;
        var pathFile = path + "/MusicPlayed";
        var dir = new DirectoryInfo(path);
        return dir;
    }

    private static int GetIdFile()
    {
        var extension = ".dat";
        var dir = GetDirInfo();
        var files = dir.GetFiles();
        int nbLast = 0;

        if (files.Length == 0)
        {
            return 0;
        }

        foreach (FileInfo file in files)
        {
            string id = file.Name.Replace("MusicPlayed", string.Empty)
                            .Replace(extension, string.Empty);
            int nb = int.Parse(id) + 1;
            if (nb > nbLast)
            {
                nbLast = nb;
            }
        }
        

        return nbLast;
    }

    private static string GetFileById(int id)
    {
        return Application.persistentDataPath + $"/MusicPlayed{id}.dat";
    }

    public static int SaveinXml(List<ICommand> datas)
    {
        int id = GetIdFile();
        string path = GetFileById(id);
        File.AppendAllText(path, "<Movements>");

        using (FileStream file = File.Open(path, FileMode.Append))
        {
            foreach (var data in datas)
            {
                DataContractSerializer bf = new DataContractSerializer(data.GetType());
                MemoryStream streamer = new MemoryStream();
                bf.WriteObject(streamer, data);
                streamer.Seek(0, SeekOrigin.Begin);
                file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);
            }
        }

        File.AppendAllText(path, "</Movements>");

        return id;
    }

    public static void DeleteFromXml(int id)
    {
        var kikongi = Helper.FindByTag(Names.KIKONGI);
        string path = GetFileById(id);
        File.Delete(path);
    }

    public static int GetNbFiles()
    {
        var dir = GetDirInfo();
        return dir.GetFiles().Length;
    }

    public static void ReadFromXml(int id)
    {
        var kikongi = Helper.FindByTag(Names.KIKONGI);
        string path = GetFileById(id);
        XDocument xdoc = XDocument.Load(path);
        foreach (XNode node in xdoc.DescendantNodes())
        {
            if (node is XElement)
            {
                var element = (XElement)node;

                if (element.Name.LocalName.Equals("PlayNoteKikongiCommand"))
                {
                    eNote notePlay = eNote.NONE;

                    foreach (XNode nodeChild in element.Nodes())
                    {
                        var elementChild = (XElement)nodeChild;

                        if (elementChild.Name.LocalName.Equals("NoteName"))
                        {
                            notePlay = (eNote)Enum.Parse(typeof(eNote), elementChild.Value);
                        }
                    }

                    var playNoteKikongiCommand = new PlayNoteKikongiCommand(kikongi.GetComponentsInChildren<AudioSource>(), notePlay);
                    CommandManager.Instance.AddCommand(playNoteKikongiCommand);
                }
            }
        }
    }
}
