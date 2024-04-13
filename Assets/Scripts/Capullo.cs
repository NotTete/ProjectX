using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capullo : MonoBehaviour
{
    public string name = null;

    void Start()
    {
    }

    public void Initialize(string nombre)
    {
        name = nombre;
    }
}
