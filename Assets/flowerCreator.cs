using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCreator : MonoBehaviour
{
    string capullo = null;
    string tallos = null;
    List<string> complementos;

    Emotions stats = new Emotions();

    void Start()
    {
        List<string> list = new List<string>();
        AddCapullo("hojas");
        AddCapullo("espinas-ej");
    }

    void Update()
    {

    }

    void AddCapullo(string nombre) 
    {
        RemoveCapullo();

        GameObject hijo = new GameObject("Capullo");
        hijo.AddComponent<Capullo>();
        Capullo capullo = hijo.GetComponent<Capullo>();
        capullo.Initialize(nombre);
        stats.Add(capullo.emotions);

        hijo.transform.SetParent(transform);
    }

    void RemoveCapullo()
    {
        Transform antiguoHijo = transform.Find("Capullo");
        if(antiguoHijo != null )
        {
            Capullo antiguoCapullo = antiguoHijo.gameObject.GetComponent<Capullo>();
            stats.Substract(antiguoCapullo.emotions);
            Destroy(antiguoHijo.gameObject);
        }

    }
}
