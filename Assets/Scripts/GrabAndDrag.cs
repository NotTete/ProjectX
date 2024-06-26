using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabAndDrag : MonoBehaviour
{
    [SerializeField] Collider2D table;
    [SerializeField] Vector2 initial_position; 
    [SerializeField] Vector2 click_position;
    [SerializeField] bool extra;
    [SerializeField] public float scalingNormal = 0.07f;
    [SerializeField] public float scalingBig = 0.10f;
    [SerializeField] GameObject objecto;
    private Animator animator;
    public bool selected = false;
    public bool grabbed = false;
    public bool cloned = false;

    float grabbed_frames = 0f;
    Vector3 offset = Vector3.zero;
    Vector2 position_before_movement;
    Ray ray;


    Collider2D collider;
    void Start()
    {
        animator = objecto.GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        if(!selected) gameObject.transform.localScale = new Vector3(scalingNormal, scalingNormal, scalingNormal);
    }

    void Update()
    {
        if(animator.GetBool("isInteractable"))
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.right, Mathf.Infinity);
                grabbed = hit.collider == collider;
                offset = transform.position - new Vector3(hit.point.x, hit.point.y, transform.position.z);
                position_before_movement = new Vector2(transform.position.x , transform.position.y);
            }

            if(grabbed)
            {
                if (Input.GetMouseButton(0))
                {
                    gameObject.layer = 7;
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    transform.position = new Vector3(ray.origin.x, ray.origin.y, transform.position.z) + offset;
                    grabbed_frames += Time.deltaTime;
                    selected = true;
                    gameObject.transform.localScale = new Vector3(scalingBig, scalingBig, scalingBig);
                }
                else if (grabbed_frames <= 0.2f && (transform.position + offset - new Vector3(initial_position.x, initial_position.y, transform.position.z)).sqrMagnitude < 0.1f)
                {
                    transform.position = new Vector3(click_position.x, click_position.y, transform.position.z);

                    grabbed_frames = 0;
                    grabbed = false;
                    selected = true;

                }
                else if(grabbed_frames <= 0.2f && (position_before_movement - new Vector2(transform.position.x, transform.position.y)).sqrMagnitude < 0.1f)
                {
                    checkToReset(true);
                    grabbed = false;
                    grabbed_frames = 0;
                } else {
                    checkToReset();
                    grabbed = false;
                    grabbed_frames = 0;

                }
            }
    }

    public void checkToReset(bool force = false)
    {
        if(!collider.IsTouching(table) || force) {
            transform.position = new Vector3(initial_position.x, initial_position.y, transform.position.z);
            if(cloned) Destroy(gameObject);
            selected = false;
            gameObject.layer = 6;
            gameObject.transform.localScale = new Vector3(scalingNormal, scalingNormal, scalingNormal);
        }
    }
}
