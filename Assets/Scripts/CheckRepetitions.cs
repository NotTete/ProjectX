using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRepetitions : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        foreach (GrabAndDrag g in transform.GetComponentsInChildren<GrabAndDrag>())
        {
            if(g.selected && !g.cloned)
            {
                GameObject clone = Instantiate(g.gameObject, g.transform);
                clone.GetComponent<GrabAndDrag>().cloned = true;
                g.checkToReset(true);
            }
        }
    }
}
