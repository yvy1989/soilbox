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

    public GameObject unit;
    public GameObject ParticleCoin;

    
    void Start()
    {
        unit = transform.GetChild(0).gameObject;

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
            GameObject coin;
            yield return new WaitForSeconds(_timeMoney);

            GameManager.Instance.addMoney(_MoneyAmount);


            coin = Instantiate(ParticleCoin, new Vector3(unit.transform.position.x, unit.transform.position.y+1, unit.transform.position.z),Quaternion.Euler(-90, -90, 0));

            if (tempCarbon > _timeMaxCarbon)
            {
                GameManager.Instance.addCarbon(_CarbonAmount);
                tempCarbon = 0;
            }

            yield return new WaitForSeconds(1f);
            Destroy(coin);
            tempCarbon++;
        }

    }
}
