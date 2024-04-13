using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UiAnimator : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private bool _isCreatingFlower = false;
    public bool IsCreatingFlower
    {
        get
        {
            return _isCreatingFlower;
        }
        private set
        {
            Debug.Log(animator.GetBool("isCreatingFlower"));
            _isCreatingFlower = value;
            animator.SetBool("isCreatingFlower", value);

        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
