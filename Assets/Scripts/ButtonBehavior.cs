using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("1- solo, 2- arvore, 3-Animal")]
    public int UnityType;

    public Text CarbonValueTxt;
    public Text MoneyValueTxt;

    // Start is called before the first frame update
    void Start()
    {
        if (UnityType == 1)
        {
            CarbonValueTxt.text = GameManager.Instance.CostPlantatipnCarbon.ToString() + " Kg CO2/Ano";
            MoneyValueTxt.text = GameManager.Instance.CostPlantationValue.ToString() + " $";
        }
        if (UnityType == 2)
        {
            CarbonValueTxt.text = GameManager.Instance.TreeCarbonValue.ToString() + " Kg CO2/Ano";
            MoneyValueTxt.text = GameManager.Instance.CostTreeValue.ToString()+" $";
        }
        if (UnityType == 3)
        {
            CarbonValueTxt.text = GameManager.Instance.AnimalCarbonValue.ToString() + " Kg CO2/Ano";
            MoneyValueTxt.text = GameManager.Instance.CostAnimalValue.ToString() + " $";
        }

    }

    void Update()
    {
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("saiu");
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("entrou");
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
