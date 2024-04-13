using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDataRaw
{
    public ClientDataRaw[] dialogues;
    public FlowerDataRaw flowers;
    public string[] emociones;
}

[Serializable]
public class ClientDataRaw
{
    public string name;
    public ClientOptionsDataRaw[] options;
}

[Serializable]
public class ClientOptionsDataRaw
{
    public string dialogue;
    public int[] next;
}

[Serializable]
public class FlowerDataRaw
{
    public FlowerPartDataRaw[] capullos;
    public FlowerPartDataRaw[] tallos;
    public FlowerPartDataRaw[] complementos;
}

[Serializable]
public class FlowerPartDataRaw
{
    public string name;
    public int[] value;
}

[Serializable]
public class DialogueOptionDataRaw
{
    public string preview;
    public string dialogue;
    public int[] next;
}

public class ClientData
{
    public string name;
    public List<ClientOptionsData> options;
}

public class ClientOptionsData
{
    public string dialogue;
    public Emotions next;
}

public class FlowerData
{
    public Dictionary<string, Emotions> capullos;
    public Dictionary<string, Emotions> tallos;
    public Dictionary<string, Emotions> complementos;
}

public class Emotions
{
    public int melancolia;
    public int alegria;
    public int miedo;
    public int amor;
    public int paz;

    public void Add(Emotions other)
    {
        melancolia += other.melancolia;
        alegria += other.alegria;
        miedo += other.miedo;
        amor += other.amor;
        paz += other.paz;
    }

    public void Substract(Emotions other)
    {
        melancolia -= other.melancolia;
        alegria -= other.alegria;
        miedo -= other.miedo;
        amor -= other.amor;
        paz -= other.paz;
    }
}

public class DataLoader : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    public static GameDataRaw gameData;
    public static FlowerData flowerData;
    List<ClientData> clientData;
    void Awake()
    {
        string data = jsonFile.ToString();
        Debug.Log(data);
        gameData = JsonUtility.FromJson<GameDataRaw>(data);
        CargarFlores();
        CargarClientes();
    }

    private void CargarFlores()
    {
        flowerData = new FlowerData();
        flowerData.capullos = new Dictionary<string, Emotions>();
        flowerData.tallos = new Dictionary<string, Emotions>();
        flowerData.complementos = new Dictionary<string, Emotions>();

        foreach (FlowerPartDataRaw part in gameData.flowers.capullos)
        {
            Emotions conversion = new Emotions();
            conversion.melancolia = part.value[0];
            conversion.alegria = part.value[1];
            conversion.miedo = part.value[2];
            conversion.amor = part.value[3];
            conversion.paz = part.value[4];

            flowerData.capullos.Add(part.name, conversion);
        }

        foreach (FlowerPartDataRaw part in gameData.flowers.tallos)
        {
            Emotions conversion = new Emotions();
            conversion.melancolia = part.value[0];
            conversion.alegria = part.value[1];
            conversion.miedo = part.value[2];
            conversion.amor = part.value[3];
            conversion.paz = part.value[4];

            flowerData.tallos.Add(part.name, conversion);
        }

        foreach (FlowerPartDataRaw part in gameData.flowers.complementos)
        {
            Emotions conversion = new Emotions();
            conversion.melancolia = part.value[0];
            conversion.alegria = part.value[1];
            conversion.miedo = part.value[2];
            conversion.amor = part.value[3];
            conversion.paz = part.value[4];

            flowerData.complementos.Add(part.name, conversion);
        }
    }

    private void CargarClientes()
    {
        clientData = new List<ClientData>();
        foreach(ClientDataRaw clientRaw in gameData.dialogues) 
        { 
            ClientData cliente = new ClientData();
            cliente.name = clientRaw.name;
            cliente.options = new List<ClientOptionsData>();
            foreach(ClientOptionsDataRaw optionsRaw in clientRaw.options) {
                ClientOptionsData options = new ClientOptionsData();
                options.dialogue = optionsRaw.dialogue;
                options.next = new Emotions();
                options.next.melancolia = optionsRaw.next[0];
                options.next.alegria = optionsRaw.next[1];
                options.next.miedo = optionsRaw.next[2];
                options.next.amor = optionsRaw.next[3];
                options.next.paz = optionsRaw.next[4];
                cliente.options.Add(options);
            }
            clientData.Add(cliente);
        }
    }
}

