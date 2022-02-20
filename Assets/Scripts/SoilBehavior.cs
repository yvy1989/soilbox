using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SoilBehavior : MonoBehaviour
{
    public int TerrenoId;

    private bool isSelected = false; //verifica o solo q esta ativo e vai ficar vermelho

    public float price;

    public string description;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if (!isSelected && !GameManager.Instance.isMenuActive)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }

        //Debug.Log("passou");
    }
    private void OnMouseExit()
    {
        if (!isSelected && !GameManager.Instance.isMenuActive)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        
        //Debug.Log("saiu");
    }


}
