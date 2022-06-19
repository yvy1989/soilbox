using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellCo2 : MonoBehaviour
{
    public GameObject SellCo2Menu;
    //public bool isSellPanelEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        SellCo2Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Sell()
    {
        

        SellCo2Menu.SetActive(true);
    }

    public void BtnYes()
    {
        GameManager.Instance.sellCarbon(5, 10);
        SellCo2Menu.SetActive(false);
    }

    public void BtnNot()
    {
        SellCo2Menu.SetActive(false);
    }

}
