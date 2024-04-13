using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capullo : MonoBehaviour
{
    public string name = null;
    public Emotions emotions;

    void Start()
    {
        emotions = new Emotions();
    }

    public void Initialize(string nombre)
    {
        name = nombre;
        emotions = DataLoader.flowerData.capullos[nombre];
    }
}
