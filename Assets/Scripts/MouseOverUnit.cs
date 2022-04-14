using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverUnit : MonoBehaviour
{
    bool isPanelAvcive = false;
    GameObject painel;
    // Start is called before the first frame update
    void Start()
    {
        painel = GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject;
        painel.SetActive(isPanelAvcive); //desativa o painel
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
