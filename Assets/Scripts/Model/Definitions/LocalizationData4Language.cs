using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Localization", fileName = "Localization")]
class LocalizationData4Language : ScriptableObject
{
    [SerializeField] private string _localizationImportFileName;

    [SerializeField] public List<LocalizationEntry> LocalizationEntriesList;

    private static LocalizationData4Language _instance;
    private static string _loadedFilename;
    public static LocalizationData4Language I(string filename)
    {
        if (_instance == null || _loadedFilename != filename )
        {
            _instance = LoadGameSettings(filename);
            _loadedFilename = filename;
        }
        return _instance;
    }

    private static LocalizationData4Language LoadGameSettings(string _filename)
    {

        var a = Resources.Load<LocalizationData4Language>(_filename);
        return Resources.Load<LocalizationData4Language>(_filename);
    }

    [ContextMenu("Collect Data From File")]
    public void CollectData()
    {
        TextAsset localizationData = Resources.Load($"Localization/{_localizationImportFileName}") as TextAsset;
        var stringArray = localizationData.ToString().Split("\n");
        LocalizationEntriesList.Clear();
        foreach (var str in stringArray)
        {
            var arr = str.Split("\t");
            Debug.Log(str);
            var a = new LocalizationEntry(arr[0], arr[1], arr[2]);
            LocalizationEntriesList.Add(a);
        } 
    }
}

[Serializable]
class LocalizationEntry
{
    [SerializeField] public string _key;
    [SerializeField] public string _source;
    [SerializeField] public string _translation;

    public LocalizationEntry(string key, string source, string translation)
    {
        _key = key;
        _source = source;
        _translation = translation;
    }
}



