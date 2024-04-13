using UnityEngine;

public class TableClose : MonoBehaviour
{
    public GameObject UIParent;
    public GameObject UIFlowers;

    Animator uiAnimator;
    Animator flowersAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        uiAnimator = UIParent.GetComponent<Animator>();
        flowersAnimator = UIFlowers.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void CloseFlowerMenu()
    {

        Debug.Log("Piton");
        uiAnimator.SetBool("isCreatingFlower", false);
        flowersAnimator.SetBool("isCreatingFlower", false);
    }
}
