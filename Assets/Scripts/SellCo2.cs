using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellCo2 : MonoBehaviour
{
    public float carbonDamage;
    public float moneyReward;


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
        GameManager.Instance.sellCarbon(carbonDamage, moneyReward);
        SellCo2Menu.SetActive(false);
    }

    public void BtnNot()
    {
        SellCo2Menu.SetActive(false);
    }

}
