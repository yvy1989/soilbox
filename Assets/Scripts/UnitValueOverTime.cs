using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitValueOverTime : MonoBehaviour
{

    /// <summary>
    /// /Adiciona ou remove valores de acordo com o tempo ex: vacas vao adicionando dinheiro e removendo carbono com o tempo
    /// </summary>
    // Start is called before the first frame update

    float timeMoney, timeMaxCarbon, CarbonAmount, MoneyAmount,storageAmount;

    public GameObject unit;
    public GameObject ParticleCoin;
    public GameObject ParticleGas;

    
    void Start()
    {
        timeMoney = GameManager.Instance.timeMoney;
        timeMaxCarbon = GameManager.Instance.timeMaxCarbon;
        CarbonAmount = GameManager.Instance.CarbonAmount;
        MoneyAmount = GameManager.Instance.MoneyAmount;
        storageAmount = GameManager.Instance.AnimalStorageValue;

        unit = transform.GetChild(0).gameObject;

        StartCoroutine(AddAndRemoveValues(timeMoney, timeMaxCarbon, CarbonAmount, MoneyAmount, storageAmount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AddAndRemoveValues(float _timeMoney,float _timeMaxCarbon,float _CarbonAmount,float _MoneyAmount, float _storageAmount)
    {
        
        float tempCarbon = 0;
        while (true)
        {
            GameObject coin;
            GameObject gas = null;
            yield return new WaitForSeconds(_timeMoney);

            GameManager.Instance.addCattleMoney(_MoneyAmount);
            GameManager.Instance.fillStorage(_storageAmount);

            coin = Instantiate(ParticleCoin, new Vector3(unit.transform.position.x, unit.transform.position.y+1, unit.transform.position.z),Quaternion.Euler(-90, -90, 0));
           

            if (tempCarbon >= _timeMaxCarbon)
            {
                GameManager.Instance.addCarbon(_CarbonAmount);
                
                gas = Instantiate(ParticleGas, new Vector3(unit.transform.position.x, unit.transform.position.y, unit.transform.position.z), Quaternion.Euler(-90, -90, 0));
                tempCarbon = 0;
            }

            yield return new WaitForSeconds(1.5f);
            Destroy(coin);
            Destroy(gas);
            tempCarbon++;
            //Debug.Log(tempCarbon);
        }

    }
}
