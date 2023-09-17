using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private string path;

    public Data data { get; private set; }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        path = Application.persistentDataPath + "/Data.dat";
        LoadData();
    }

    private void LoadData()
    {
        if (!File.Exists(path))
        {
            data = new Data();
            return;
        }

        data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(path));
    }

    private void SaveData()
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(data));
    }

    public class Data
    {
        public string Id;

        public Data()
        { 
            Id = Random.value.ToString();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
