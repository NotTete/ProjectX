using UnityEngine;

public class RepeatElements : MonoBehaviour
{
    void Update()
    {
        foreach (GrabAndDrag g in transform.GetComponentsInChildren<GrabAndDrag>())
        {
            if (g.selected && !g.cloned && !g.grabbed)
            {
                GameObject clone = Instantiate(g.gameObject, transform);
                clone.GetComponent<GrabAndDrag>().cloned = true;
                clone.GetComponent<GrabAndDrag>().selected = false;
                clone.GetComponent<GrabAndDrag>().grabbed = false;

                g.checkToReset(true);
            }
        }
    }
}
