using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Image CreditAmount;
    public Image RedCarbonAmount;
    public Image GreenCarbonAmount;

    float fillMoney;
    float fillCarbon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillMoney =  GameManager.Instance.currentMoney / GameManager.Instance.MaxMoney;

        CreditAmount.fillAmount = fillMoney;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        float currCarb = GameManager.Instance.currentCarbon;

        //Debug.Log(currCarb / -100);

        

        if (currCarb > 0) //vermelho
        {
            RedCarbonAmount.fillAmount = (currCarb / 100);
        }
        if (currCarb < 0) //verde
        {
            GreenCarbonAmount.fillAmount = (currCarb / -100);

        }

    }
}
