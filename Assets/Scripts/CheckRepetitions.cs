using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRepetitions : MonoBehaviour
{
    List<GrabAndDrag> grabAndDrag;
    void Start()
    {
        grabAndDrag = new List<GrabAndDrag>();
        foreach (GrabAndDrag g in transform.GetComponentsInChildren<GrabAndDrag>())
        {
            grabAndDrag.Add(g);
        }
    }

    public void checkRepetitions(int id)
    {
        for (int i = 0; i < grabAndDrag.Count; i++)
        {
            if (grabAndDrag[i].selected && grabAndDrag[i].GetInstanceID() != id)
            {
                grabAndDrag[i].checkToReset(true);
            }
        }
    }
}
