using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRepetitions : MonoBehaviour
{
    List<GrabAndDrag> grabAndDrag;
    int last_index = -1;
    void Start()
    {
        grabAndDrag = new List<GrabAndDrag>();
        foreach (GrabAndDrag g in transform.GetComponentsInChildren<GrabAndDrag>())
        {
            grabAndDrag.Add(g);
        }
    }

    void Update()
    {
        for(int i = 0; i < grabAndDrag.Count; i++)
        {
            if (grabAndDrag[i].selected && last_index != i && last_index != -1 && !grabAndDrag[i].grabbed)
            {
                grabAndDrag[last_index].checkToReset(true);
                last_index = i;
            } else if(grabAndDrag[i].selected && last_index != i && !grabAndDrag[i].grabbed)
            {
                last_index = i;
            }
        }
    }
}
