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
                Debug.Log(clone.transform.position);
                clone.GetComponent<GrabAndDrag>().cloned = true;
                g.checkToReset(true);
            }
        }
    }
}
