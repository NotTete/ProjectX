using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Dialog.DialogueComponent;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class Dialog : MonoBehaviour
{
    // TETE TE ODIO POR LA UI ENTERA ANIMADA
    public GameObject UIParent;
    public GameObject UIFlowers;
    public RectTransform parent;
    public GameObject optionButton;
    public TextMeshProUGUI textComponent;
    public DialogueComponent[] dialogs;
    public float textSpeed;

    private Animator uiAnimator;
    private Animator flowersAnimator;
    private bool hasOptions = false;
    private List<GameObject> displayedButtons = new();
    private string msgBuffer;
    private int dialogIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiAnimator = UIParent.GetComponent<Animator>();
        flowersAnimator = UIFlowers.GetComponent<Animator>();
        msgBuffer = dialogs[dialogIdx].text;

        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        else if (!hasOptions && textComponent.text == msgBuffer)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = msgBuffer;
        }

    }

    void StartDialogue()
    {
        dialogIdx = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in msgBuffer.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (dialogIdx < dialogs.Length - 1)
        {
            dialogIdx++;
            if (dialogs[dialogIdx].options.Length > 0)
            {
                hasOptions = true;

                foreach (DialogueOptions option in dialogs[dialogIdx].options)
                {
                    GameObject newOptionButton = Instantiate(optionButton);
                    Button jj = newOptionButton.GetComponent<Button>();
                    TextMeshProUGUI btnText = jj.GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = option.optionDescription;
                    jj.transform.SetParent(parent.transform, false);
                    jj.onClick.AddListener(() => ShowOptionResponse(option.optionResponse));

                    displayedButtons.Add(newOptionButton);
                }
            }
            else
            {
                hasOptions = false;
            }
            msgBuffer = dialogs[dialogIdx].text;

            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // TODO: Añadir sonido mesa
            uiAnimator.SetBool("isCreatingFlower", true);
            flowersAnimator.SetBool("isCreatingFlower", true);
            gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class DialogueComponent
    {
        public string text;
        public DialogueOptions[] options;

        [System.Serializable]
        public class DialogueOptions
        {
            public string optionDescription;
            public string optionResponse;
        }
    }

    void ShowOptionResponse(string message)
    {
        if (!(textComponent.text == msgBuffer)) return;

        foreach (GameObject btn in displayedButtons)
        {
            Destroy(btn);
        }
        displayedButtons = new List<GameObject>();

        textComponent.text = string.Empty;
        msgBuffer = message;
        hasOptions = false;
        StartCoroutine(TypeLine());
    }
}