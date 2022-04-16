using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class DataManager
{
    public static event Action DataChanged = () => { };

    private static readonly Dictionary<string, Dictionary<string, string>> Dictionary = new Dictionary<string, Dictionary<string, string>>();
    private static string _column = "Value";
    public static string Column
    {
        get { return _column; }
        set { _column = value; DataChanged(); }
    }
    public static void Auto()
    {
        Column = "Value";
    }

    public static void Read(string path = "Data")
    {
        if (Dictionary.Count > 0) return;

        var textAssets = Resources.LoadAll<TextAsset>(path);

        foreach (var textAsset in textAssets)
        {
            var text = ReplaceMarkers(textAsset.text);
            var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");

            foreach (Match match in matches)
            {
                text = text.Replace(match.Value, match.Value.Replace("\"", null).Replace(",", "[comma]").Replace("\n", "[newline]"));
            }

            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var column = lines[0].Split(',').Select(i => i.Trim()).ToList();

            for (var i = 1; i < column.Count; i++)
            {
                if (!Dictionary.ContainsKey(column[i]))
                {
                    Dictionary.Add(column[i], new Dictionary<string, string>());
                }
            }

            for (var i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',').Select(j => j.Trim()).Select(j => j.Replace("[comma]", ",").Replace("[newline]", "\n")).ToList();
                var key = columns[0];

                for (var j = 1; j < column.Count; j++)
                {
                    Dictionary[column[j]].Add(key, columns[j]);
                }
            }
        }

        Auto();
    }
    public static float Parse(string Key)
    {
        if (Dictionary.Count == 0)
        {
            Read();
        }

        if (!Dictionary.ContainsKey(Column)) throw new KeyNotFoundException("Language not found: " + Column);
        if (!Dictionary[Column].ContainsKey(Key)) throw new KeyNotFoundException("Translation not found: " + Key);

        return float.Parse(Dictionary[Column][Key]);
    }
    /*
    public static string Localize(string localizationKey, params object[] args)
    {
        var pattern = Parse(localizationKey);

        return string.Format(pattern, args);
    }
    */
    private static string ReplaceMarkers(string text)
    {
        return text.Replace("[Newline]", "\n");
    }
}
