using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverUnit : MonoBehaviour
{
    GrowPlant myPlant;

    public GameObject Unit;
    bool isPanelAvcive = false;
    GameObject painel;
    // Start is called before the first frame update
    void Start()
    {
        myPlant = Unit.GetComponent<GrowPlant>();//pega a referencia da unidade
        painel = GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject;
        painel.SetActive(isPanelAvcive); //desativa o painel
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myPlant.CurrentlevelsCount);
    }

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnMouseDown()
    {
        
        isPanelAvcive = !isPanelAvcive;

        painel.SetActive(isPanelAvcive);



    }

    public void colher()
    {
        if (myPlant.isReady)//se a plante estiver pronta p colher ou derrubar
        {
            if (myPlant.typeUnity == 1)// se for arvore
            {
                painel.SetActive(false);
                myPlant.downTree();
                Invoke("waitToDownTree", 4f);
            }
            else// se nao for arvore
            {
                painel.SetActive(false);
                Destroy(Unit);
            }
            
        }
        else
        {
            GameManager.Instance.ShowInfo("Ainda nao e possivel colher ou destruir");
            painel.SetActive(false);
        }
        
    }

    public void cancelar()
    {
        painel.SetActive(false);
        isPanelAvcive = false;
    }

    void waitToDownTree()
    {

        Destroy(Unit);
    }
}
