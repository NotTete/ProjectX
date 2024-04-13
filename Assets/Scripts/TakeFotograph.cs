using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeFotograph : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] TextMeshPro text;
    public void SaveCameraView()
    {
        int resWidth = Screen.width;
        int resHeight = Screen.height;

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 32);
        camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(resWidth, resHeight, TextureFormat.RGBA32, false);

        camera.Render();

        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] byteArray = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes("AD", byteArray);
        Debug.Log("Saved");


    }
}
