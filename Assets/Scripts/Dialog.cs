using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Dialog.DialogueComponent;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class Dialog : MonoBehaviour
{
    // TETE TE ODIO POR HACER LA UI ENTERA ANIMADA
    public string nextSceneName;
    public GameObject UIParent;
    public GameObject UIFlowers;
    public GameObject UIClient;
    public RectTransform parent;
    public GameObject optionButton;
    public TextMeshProUGUI textComponent;
    public DialogueComponent[] dialogs;
    public float textSpeed;
    public bool isOutro = false;

    private Color currentColor;
    private Color darkerColor;
    private Image dialogboxImage;
    private Animator uiAnimator;
    private Animator flowersAnimator;
    private Animator clientAnimator;
    private bool hasOptions = false;
    private List<GameObject> displayedButtons = new();
    public string msgBuffer;
    private int dialogIdx = 0;
    TextMeshProUGUI speakerNameText;
    Image speakerNameBubble;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.shopBell);
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogboxImage = this.GetComponent<Image>();
        speakerNameText = GameObject.FindGameObjectWithTag("NombreDialogo").GetComponent<TextMeshProUGUI>();
        speakerNameBubble = GameObject.FindGameObjectWithTag("BurbujaNombreLocutor").GetComponent<Image>();
        ActivateFlower(false);
        uiAnimator = UIParent.GetComponent<Animator>();
        flowersAnimator = UIFlowers.GetComponent<Animator>();
        clientAnimator = UIClient.GetComponent<Animator>();

        dialogIdx = 0;
        msgBuffer = string.Empty;
        Debug.Log(dialogs);
        msgBuffer = dialogs[dialogIdx].text;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void Call()
    {
        dialogIdx = 0;
        msgBuffer = string.Empty;
        Debug.Log(dialogs);
        msgBuffer = dialogs[dialogIdx].text;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButtonDown(0)) return;

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

    public Color DarkenColor
    {
        get 
        {
            float h, s, v;
            Color.RGBToHSV(currentColor, out h, out s, out v);
            s += 0.25f;
            v += 0.15f;
            return Color.HSVToRGB(h, s, v);
        }
    }

    void StartDialogue()
    {
        dialogIdx = 0;
        speakerNameText.text = dialogs[dialogIdx].name;

        currentColor = dialogs[dialogIdx].color;
        currentColor.a = 0.8f;
        dialogboxImage.color = currentColor;
        speakerNameBubble.color = DarkenColor;

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
            currentColor = dialogs[dialogIdx].color;
            currentColor.a = 0.8f;
            dialogboxImage.color = currentColor;
            speakerNameBubble.color = DarkenColor;
            speakerNameText.text = dialogs[dialogIdx].name;

            if (dialogs[dialogIdx].options.Length > 0)
            {
                hasOptions = true;

                foreach (DialogueOptions option in dialogs[dialogIdx].options)
                {
                    GameObject newOptionButton = Instantiate(optionButton);
                    Button jj = newOptionButton.GetComponent<Button>();
                    jj.image.color = DarkenColor;
                    TextMeshProUGUI btnText = jj.GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = option.optionDescription;
                    jj.transform.SetParent(parent.transform, false);
                    jj.onClick.AddListener(() => ShowOptionResponse(option));

                    displayedButtons.Add(newOptionButton);
                }
            }
            else
            {
                hasOptions = false;
            }
            msgBuffer = dialogs[dialogIdx].text;

            textComponent.text = string.Empty;
            currentColor = dialogs[dialogIdx].color;
            currentColor.a = 0.8f;
            dialogboxImage.color = currentColor;
            speakerNameBubble.color = DarkenColor;
            StartCoroutine(TypeLine());
        }
        else
        {
            ReturnLogic();
        }
    }

    [System.Serializable]
    public class DialogueComponent
    {
        public string text;
        public string name;
        public Color color;
        public DialogueOptions[] options;

        [System.Serializable]
        public class DialogueOptions
        {
            public string optionDescription;
            public string optionResponse;
            public bool loops;
            public Color color;
            public string name;
        }
    }

    /**
     * Shows a response in a multiple option,
     * only spits one response.
     */
    void ShowOptionResponse(DialogueOptions option)
    {
        currentColor = option.color;
        currentColor.a = 0.8f;
        dialogboxImage.color = currentColor;
        speakerNameBubble.color = DarkenColor;

        if (!(textComponent.text == msgBuffer)) return;

        foreach (GameObject btn in displayedButtons)
        {
            Destroy(btn);
        }
        displayedButtons = new List<GameObject>();

        textComponent.text = string.Empty;
        speakerNameBubble.color = DarkenColor;

        hasOptions = false;
        if (option.optionResponse == "") {
            Debug.Log("IDX: " + dialogIdx + 1);
            Debug.Log("LEN: " + dialogs.Length);
            if (dialogIdx + 1 >= dialogs.Count())
            {
                ReturnLogic();
                return;
            }

            dialogIdx += 1;
            msgBuffer = dialogs[dialogIdx].text;
            currentColor = dialogs[dialogIdx].color;
            currentColor.a = 0.8f;
            dialogboxImage.color = currentColor;
            speakerNameBubble.color = DarkenColor;


            StartCoroutine(TypeLine());
            return;
        };

        speakerNameBubble.color = DarkenColor;
        msgBuffer = option.optionResponse;
        StartCoroutine(TypeLine());
        speakerNameBubble.color = DarkenColor;
        if (option.loops) dialogIdx -= 1;
        speakerNameBubble.color = DarkenColor;
    }

    void ActivateFlower(bool flowers)
    {
        foreach(GrabAndDrag gab in UIFlowers.GetComponentsInChildren<GrabAndDrag>())
        {
            gab.enabled = flowers;
        }
    }

    void ReturnLogic()
    {
        dialogIdx = 0;

        if (!isOutro)
        {
            // TODO: Añadir sonido mesa
            ActivateFlower(true);
            uiAnimator.SetBool("isCreatingFlower", true);
            flowersAnimator.SetBool("isCreatingFlower", true);
            clientAnimator.SetBool("isCreatingFlower", true);
            audioManager.PlaySFX(audioManager.tableOpen);
            this.gameObject.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("Next Scene!");
        }
    }
}