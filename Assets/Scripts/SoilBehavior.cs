using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SoilBehavior : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().enabled = true;
        //Debug.Log("passou");
    }
    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
        //Debug.Log("saiu");
    }



}
