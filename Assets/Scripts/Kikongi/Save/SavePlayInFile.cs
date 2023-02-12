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

    public static void SaveTriggerSound()
    {
        
    }

    // TODO regarder projet Techem pour serialiser xml et ouvrir une seule fois la connexion au fichier
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
