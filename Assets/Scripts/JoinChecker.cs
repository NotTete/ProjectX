using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UF
{
    public List<int> lista;
    public UF(int n)
    {
        lista = new List<int>();
        for(int i = 0; i < n; i++)
        {
            lista.Add(-1);
        }
    }

    public bool sameSet(int a, int b)
    {
        return Find(a) == Find(b);
    }
    public int Find(int a)
    {
        return lista[a] < 0 ? a : lista[a] = Find(lista[a]);
    }

    public bool join(int a, int b)
    {
        a = Find(a);
        b = Find(b);

        if(a == b) { return false; }
        if (lista[a] > b) { return false; }
        lista[a] += lista[b]; lista[b] = a;
        return true;
    }
}

public class JoinChecker : MonoBehaviour
{
    public GameObject endGameButtonObj;
    private Button endGameButton;
    private Animator endGameButtonAnimator;

    GameObject[] parents;

    private void Awake()
    {
        endGameButton = endGameButtonObj.GetComponent<Button>();
        endGameButtonAnimator = endGameButtonObj.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

        parents = GameObject.FindGameObjectsWithTag("Flores");
        Debug.Log(parents.Length);
    }

    // Update is called once per frame
    void Update()
    {
        bool tallo;
        bool capullo;

        List<GrabAndDrag> lista = getSelectedElements(out tallo, out capullo);

        UF unionFind = new UF(lista.Count);

        if(lista.Count > 0)

        for(int i = 0; i < lista.Count;i++)
        {
            Collider2D collider1 = lista[i].gameObject.GetComponent<Collider2D>();

            for (int j = i + 1; j < lista.Count; j++)
            {
                Collider2D collider2 = lista[j].gameObject.GetComponent<Collider2D>();
                if (collider1.IsTouching(collider2))
                {
                    unionFind.join(j, i);
                }
            }
        }

        bool esValido = lista.Count > 0 && tallo && capullo;
        int k = 1;
        while (esValido && k < lista.Count)
        {
            esValido = unionFind.sameSet(k, 0);
            k++;
        }

        endGameButtonAnimator.SetBool("isValid", esValido);
        if (esValido)
        {
            endGameButton.enabled = true;
            // DEBUG
        } else
        {
            endGameButton.enabled = false;
        }
    }

    List<GrabAndDrag> getSelectedElements(out bool tallo, out bool capullo)
    {
        tallo = false;
        capullo = false;
        List<GrabAndDrag> list = new List<GrabAndDrag>();

        foreach (var element in parents)
        {
            foreach (var grab in element.GetComponentsInChildren<GrabAndDrag>())
            {

                if (grab.selected && !grab.grabbed) { 
                    list.Add(grab); 
                    tallo = tallo || element.name == "GrupoTallos";
                    capullo = capullo ||element.name == "GrupoCapullos"; 
                }
            }
        }
        return list;
    }
}
