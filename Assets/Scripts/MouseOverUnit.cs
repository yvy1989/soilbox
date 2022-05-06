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
        myPlant = Unit.GetComponent<GrowPlant>();
        painel = GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject;
        painel.SetActive(isPanelAvcive); //desativa o painel
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myPlant.CurrentlevelsCount);
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
        painel.SetActive(false);
        Destroy(Unit);
    }
}
