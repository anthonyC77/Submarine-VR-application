using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Helper
{
    public static List<T> GetEnumList<T>()
    {
        T[] array = (T[])Enum.GetValues(typeof(T));
        List<T> list = new List<T>(array);
        return list;
    }

    public static T GetEnumValueByName<T>(string name)
    {
        return ((T)Enum.Parse(typeof(T), name));
    }

    public static eNote GetNoteCalled(string colliderName)
    {
        var eNoteCalled = eNote.NONE;
        Enum.TryParse(colliderName, out eNoteCalled);
        return eNoteCalled;
    }

    public static bool IsNoteCalled(string colliderName)
    {
        var listNotes = GetEnumList<eNote>().Where(n => n != eNote.NONE);
        var eNoteCalled = GetNoteCalled(colliderName);
        return listNotes.Contains(eNoteCalled);
    }

    public static Material GetPlanetMaterial(string planetName)  
    {
       return (Material)Resources.Load("Materials/" + planetName.ToString(), typeof(Material));
    }

    public static Material GetRecorderMaterial(string colorName)
    {
        return (Material)Resources.Load("Materials/Recorder/" + colorName, typeof(Material));
    }

    // test

    public static Color GetColor(string colliderName)
    {
        var note = GetNoteCalled(colliderName);

        switch (note)
        {
            case eNote.NONE:
                Debug.Log("Error color in white");
                return Color.white;
            case eNote.Fa_1:
                return new Color(228, 126, 32); // orange
            case eNote.Sol_2:
                return new Color(108, 59, 9); // brown
            case eNote.La_3:
                return Color.cyan;
            case eNote.Mi_4:
                return Color.green;
            case eNote.Si_6:
                return Color.gray;
            case eNote.Do_7:
                return new Color(94, 12, 173); // purple
            case eNote.Re_8:
                return Color.red;
            case eNote.SiBemol_9:
                return Color.blue;
            default:
                Debug.Log("Error color in white");
                return Color.white;
        }
    }

    public static List<Transform> FindChildrensByTag(string tag)
    {
       return FindByTag(tag).GetComponentsInChildren<Transform>().ToList();  
    }

    public static List<GameObject> FindByTags(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).ToList();
    }

    public static GameObject FindByTag(string tag)
    {
        return GameObject.FindGameObjectWithTag(tag);
    }

    public static ePlanet GetPlanetByNote(eNote note)
    {
        switch (note)
        {
            case eNote.Fa_1:
                return ePlanet.Jupiter;
            case eNote.Sol_2:
                return ePlanet.Mercure;
            case eNote.La_3:
                return ePlanet.Terre;
            case eNote.Mi_4:
                return ePlanet.Venus;
            case eNote.Si_6:
                return ePlanet.Saturne;
            case eNote.Do_7:
                return ePlanet.Neptune;
            case eNote.Re_8:
                return ePlanet.Mars;
            case eNote.SiBemol_9:
                return ePlanet.Uranus;
        }

        return ePlanet.NONE;
    }
}