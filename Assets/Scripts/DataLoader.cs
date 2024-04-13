using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public ClientData[] dialogues;
    public FlowerData flowers;
}

[Serializable]
public class ClientData
{
    public string name;
    public ClientOptionsData[] options;
}

[Serializable]
public class ClientOptionsData
{
    public string dialogue;
    public string preview;
    public int[] next;
}

[Serializable]
public class FlowerData
{
    public string[] capullos;
    public string[] tallos;
    public string[] complementos;
}

public class DataLoader : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    public static GameData gameData;
    void Start()
    {
        string data = jsonFile.ToString();
        Debug.Log(data);
        gameData = JsonUtility.FromJson<GameData>(data);
    }
}

