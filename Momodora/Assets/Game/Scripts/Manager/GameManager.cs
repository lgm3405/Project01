using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<string, MapData> mapDatabase;
    public Vector2Int currMapPosition;
    public MapData currMap;
    public bool checkMapUpdate = false;
    public bool cameraStop = false;
    public bool[] saveCheck = new bool[5];
    public int userSaveServer = default;

    public Image loadingImage;
    public float gameTime = default;

    public static string SavePath => Application.persistentDataPath + "/Save/";

    void Awake()
    {
        if (instance == null || instance == default) { instance = this; DontDestroyOnLoad(instance.gameObject); }
        else { Destroy(gameObject); }

        if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }

        gameTime = 0f;
        userSaveServer = 0;

        for (int i = 0; i < 5; i++)
        {
            saveCheck[i] = false;
        }

        mapDatabase = new Dictionary<string, MapData>();
        MapData[] map = Resources.LoadAll<MapData>("Maps");
        foreach (MapData mapData in map)
        {
            mapDatabase.Add(mapData.name, mapData);
        }

        currMap = Instantiate(mapDatabase["Stage1Start"], Vector2.zero, Quaternion.identity);
    }

    void Update()
    {
        gameTime = Time.time;
    }

    public static void Save(SaveLoad saveData, string saveFileName)
    {
        string saveJson = JsonUtility.ToJson(saveData);
        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("Save Success : " + saveFilePath);
    }

    public bool LoadSuccess()
    {
        return currMap.isLoadEnd;
    }

    public void CameraOnceMove(int fieldIndex, int type)
    {
        Camera.main.GetComponent<CameraMove>().CameraOnceMove(fieldIndex, type);
    }
    public static SaveLoad Load(string saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";
        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("No such saveFile exists");
            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        SaveLoad saveData = JsonUtility.FromJson<SaveLoad>(saveFile);
        return saveData;
    }

    public void SaveFileCheck(string saveFileName, int checkCount)
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        if (File.Exists(saveFilePath))
        {
            saveCheck[checkCount] = true;
        }
        else
        {
            saveCheck[checkCount] = false;
        }
    }
}
