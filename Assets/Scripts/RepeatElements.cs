using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class RepeatElements : MonoBehaviour
{
    Queue<GameObject> grabAndDragQueue;

    void Start() {
        grabAndDragQueue = new Queue<GameObject>();
    }

    public void AddElement(GameObject obj)
    {
        GameObject clone = Instantiate(obj, transform, false);
        if(grabAndDragQueue.Count >= 10)
        {
            GameObject erase_obj = grabAndDragQueue.Dequeue();
            Destroy(erase_obj);
        }

        grabAndDragQueue.Enqueue(clone);
        obj.GetComponent<GrabAndDrag>().checkToReset(true);
    }
}
