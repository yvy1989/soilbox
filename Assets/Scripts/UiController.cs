using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text TimeUI;
    public Image CreditAmount;
    public Image RedCarbonAmount;
    public Image GreenCarbonAmount;

    public Image CurrentStorageAmount;
    public Image FinalStorageAmount;

    float fillMoney;
    float fillCarbon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeUI.text = GameManager.Instance.hour.ToString("00") + ":" + GameManager.Instance.minutes.ToString("00") + ":" + GameManager.Instance.seconds.ToString("00");

        fillMoney =  GameManager.Instance.currentMoney / GameManager.Instance.MaxMoney;

        FinalStorageAmount.fillAmount = GameManager.Instance.finalStorage/100;

        CreditAmount.fillAmount = fillMoney;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        float currCarb = GameManager.Instance.currentCarbon;

        //Debug.Log(currCarb / -100);

        

        if (currCarb >= 0) //vermelho
        {
            RedCarbonAmount.fillAmount = (currCarb / 100);
        }
        if (currCarb <= 0) //verde
        {
            GreenCarbonAmount.fillAmount = (currCarb / -100);

        }

    }
}
