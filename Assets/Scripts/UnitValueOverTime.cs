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


    public GameObject vaca;
    public GameObject unit;
    public GameObject ParticleCoin;
    public GameObject ParticleGas;
    public Transform gasPoint;

    
    void Start()
    {
        timeMoney = GameManager.Instance.timeMoney;
        timeMaxCarbon = GameManager.Instance.timeMaxCarbon;
        CarbonAmount = GameManager.Instance.CarbonAmount;
        MoneyAmount = GameManager.Instance.MoneyAmount;
        storageAmount = GameManager.Instance.AnimalStorageValue;

        unit = transform.GetChild(0).gameObject;

        StartCoroutine(AddMoney(timeMoney, MoneyAmount, storageAmount));

        StartCoroutine(CarbonDamage(timeMaxCarbon, CarbonAmount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AddMoney(float _timeMoney,float _MoneyAmount, float _storageAmount)
    {

        while (true)
        {
            GameObject coin;

            yield return new WaitForSeconds(_timeMoney);

            GameManager.Instance.addCattleMoney(_MoneyAmount);
            GameManager.Instance.fillStorage(_storageAmount);

            coin = Instantiate(ParticleCoin, new Vector3(unit.transform.position.x, unit.transform.position.y+1, unit.transform.position.z), Quaternion.Euler(-90, -90, 0));
           

            yield return new WaitForSeconds(1.5f);
            Destroy(coin);

        }

    }

    IEnumerator CarbonDamage(float _timeMaxCarbon, float _CarbonAmount)
    {

        while (true)
        {
            GameObject gas = null;
            yield return new WaitForSeconds(_timeMaxCarbon);
            GameManager.Instance.addCarbon(_CarbonAmount);
            gas = Instantiate(ParticleGas, gasPoint.position, vaca.transform.localRotation);


            yield return new WaitForSeconds(1.5f);


            Destroy(gas);

        }

    }
}
