using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitValueOverTime : MonoBehaviour
{
    /// <summary>
    /// /Adiciona ou remove valores de acordo com o tempo ex: vacas vao adicionando dinheiro e removendo carbono com o tempo
    /// </summary>
    // Start is called before the first frame update

    public float timeMoney, timeMaxCarbon, CarbonAmount, MoneyAmount;
    void Start()
    {
        StartCoroutine(AddAndRemoveValues(timeMoney, timeMaxCarbon, CarbonAmount, MoneyAmount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AddAndRemoveValues(float _timeMoney,float _timeMaxCarbon,float _CarbonAmount,float _MoneyAmount)
    {
        float tempCarbon = 0;
        while (true)
        {
            yield return new WaitForSeconds(_timeMoney);

            GameManager.Instance.addMoney(_MoneyAmount);

            if (tempCarbon > _timeMaxCarbon)
            {
                GameManager.Instance.addCarbon(_CarbonAmount);
                tempCarbon = 0;
            }

            tempCarbon++;
        }

    }
}
