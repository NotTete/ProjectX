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
                float ns = clone.GetComponent<GrabAndDrag>().scalingNormal;
                float bs = clone.GetComponent<GrabAndDrag>().scalingBig;
                clone.transform.localScale = new Vector3(bs, bs, bs);

                clone.GetComponent<GrabAndDrag>().cloned = true;
                clone.GetComponent<GrabAndDrag>().selected = true;
                clone.GetComponent<GrabAndDrag>().grabbed = false;

                g.checkToReset(true);
            }
        }
    }
}
