using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}