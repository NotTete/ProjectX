using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class texto : MonoBehaviour
{

    TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

   public void Activado()
    {
        textMeshProUGUI.text = "Screenshot taken: " + Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + " \\" + "TakeAPlant";
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        textMeshProUGUI.text = "";
    }
}
