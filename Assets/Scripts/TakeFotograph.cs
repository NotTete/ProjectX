using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class TakeFotograph : MonoBehaviour
{
    AudioManager audioManager;
    private Animator animator;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("ScreenshotNotice").GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [SerializeField] Camera camera;
    public void SaveCameraView()
    {
        camera.enabled = true;
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

        Color[] pixels = screenshot.GetPixels(0, 0, resWidth, resHeight);

        int minHeight = 20000;
        int maxHeight = -1;
        int minWidth = 20000;
        int maxWidth = -1;

        for(int i = 0; i <resHeight; i++)
        {
            for(int j = 0; j <resWidth; j++)
            {
                if (pixels[i * resWidth + j].a != 0)
                {
                    minHeight = Mathf.Min(minHeight, i);
                    maxHeight = Mathf.Max(maxHeight, i);
                    minWidth = Mathf.Min(minWidth, j);
                    maxWidth = Mathf.Max(maxWidth, j);
                }
            }
        }

        Color[] col;
        try
        {
            col = screenshot.GetPixels(minWidth, minHeight, maxWidth - minWidth, maxHeight - minHeight);

            Texture2D crop = new Texture2D(maxWidth - minWidth, maxHeight - minHeight, TextureFormat.RGBA32, false);

            crop.SetPixels(0, 0, maxWidth - minWidth, maxHeight - minHeight, col);
            byte[] byteArray = crop.EncodeToPNG();

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + " \\" + "TakeAPlant";
            Directory.CreateDirectory(filepath);
            System.IO.File.WriteAllBytes(filepath + $"\\{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png", byteArray);

            audioManager.PlaySFX(audioManager.cameraSnap);

            Debug.Log("Saved");
        }
        catch (Exception)
        {
            camera.enabled = false;
            return;
        }

        camera.enabled = false;
    }


}
