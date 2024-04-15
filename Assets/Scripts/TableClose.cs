using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static Dialog;

public class TableClose : MonoBehaviour
{
    public Dialog newDialog;
    public GameObject UIParent;
    public GameObject UIFlowers;
    public GameObject UIClient;
    public string nextSceneName;
    public bool isOutro = false;

    AudioManager audioManager;

    // Logic for the end dialog
    public string outroClientMessage;
    public string outroClientName;
    public Color outroClientColor;

    Animator uiAnimator;
    Animator flowersAnimator;
    Animator clientAnimator;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.shopBell);
    }

    // Start is called before the first frame update
    void Start()
    {
        uiAnimator = UIParent.GetComponent<Animator>();
        flowersAnimator = UIFlowers.GetComponent<Animator>();
        clientAnimator = UIClient.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void CloseFlowerMenu()
    {
        uiAnimator.SetBool("isCreatingFlower", false);
        flowersAnimator.SetBool("isCreatingFlower", false);
        UIFlowers.SetActive(false);
        clientAnimator.SetBool("isCreatingFlower", false);

        newDialog.dialogs = null;

        Dialog.DialogueComponent outroDialog = new()
        {
            name = outroClientName,
            text = outroClientMessage,
            color = outroClientColor
        };
        Dialog.DialogueComponent[] outroDialogArr = new Dialog.DialogueComponent[1];
        outroDialogArr[0] = outroDialog;

        Debug.Log(outroDialogArr);

        newDialog.dialogs = outroDialogArr;
        newDialog.isOutro = true;
        newDialog.gameObject.SetActive(true);
        newDialog.Call();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.tableOpen);
    }
}
