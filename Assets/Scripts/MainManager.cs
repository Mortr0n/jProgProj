using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance; // public static means all values are shared by all instances of this class

    public Color TeamColor;

    private void Awake()
    {
        // if the instance already exists we don't want another one when we load the menu scene again so destroy the new one
        if (Instance != null)  // make it a Singleton
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; // stores the current instance of MainManager allowing MainManager.Instance to be called from any other script
        DontDestroyOnLoad(gameObject);  // it's in the name the game object attached to this script will not be destoyed on scene change

        LoadColor();
    }

    [System.Serializable] //must have this attribute in order to use the JsonUtility
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}
