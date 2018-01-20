using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerPrefsHelper
{
    public static void SetList<T>(string key, List<T> list)
    {
        var b = new BinaryFormatter();
        var m = new MemoryStream();
        b.Serialize(m, list);

        PlayerPrefs.SetString(key, Convert.ToBase64String(m.GetBuffer()));
    }

    public static List<T> GetList<T>(string key)
    {
        List<T> list;

        var data = PlayerPrefs.GetString(key);

        if (!string.IsNullOrEmpty(data))
        {
            var b = new BinaryFormatter();
            var m = new MemoryStream(Convert.FromBase64String(data));
            list = (List<T>)b.Deserialize(m);
            return list;
        }

        return null;
    }

    public static void SetBool(string key, bool b)
    {
        PlayerPrefs.SetInt(key, b ? 1 : 0);
    }

    public static bool GetBool(string key, bool defaultBool = false)
    {
        return PlayerPrefs.GetInt(key, defaultBool ? 1 : 0) == 1;
    }
}