using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAnimalCosts : MonoBehaviour
{
    public Text moneyReward;
    public Text carbonDamage;
    public Text timeAmount;
    public Text timeAmountCarbon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyReward.text = GameManager.Instance.MoneyAmount.ToString() + "$";
        carbonDamage.text = GameManager.Instance.CarbonAmount.ToString() +"Kg CO2";
        timeAmount.text = GameManager.Instance.timeMoney.ToString() + " Seconds";
        timeAmountCarbon.text = (GameManager.Instance.timeMoney*2).ToString() + " Seconds";


    }
}
