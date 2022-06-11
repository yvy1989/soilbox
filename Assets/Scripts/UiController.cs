using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController Instance;

    public Text TimeUI;
    public Image CreditAmount;
    public Image RedCarbonAmount;
    public Image GreenCarbonAmount;

    public Image CurrentStorageAmount;
    public Image FinalStorageAmount;

    public Text currentStorageTxt;
    public Text finalStorageTxt;

    float fillMoney;
    float fillCarbon;


    [Header("EfectsBar")]
    public GameObject RedCarbonEfectBar;
    public GameObject GreenCarbonEfectBar;

    [Header("FX prefabs")]
    public GameObject starPositive;
    public GameObject ExplosionNegative;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUiUpdate();
        MoneyUiUpdate();
        CarbonUiUpdate();

        StorageUiUpdate();

    }

    private void StorageUiUpdate()
    {
        currentStorageTxt.text = GameManager.Instance.currentStorage.ToString();
        finalStorageTxt.text = GameManager.Instance.finalStorage.ToString();

        FinalStorageAmount.fillAmount = GameManager.Instance.finalStorage / 100;

        if (GameManager.Instance.currentStorage <= GameManager.Instance.finalStorage)
        {
            CurrentStorageAmount.fillAmount = GameManager.Instance.currentStorage / 100;
        }
    }

    private void CarbonUiUpdate()
    {
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

    private void MoneyUiUpdate()
    {
        fillMoney = GameManager.Instance.currentMoney / GameManager.Instance.MaxMoney;
        CreditAmount.fillAmount = fillMoney;
    }

    private void TimeUiUpdate()
    {
        TimeUI.text = GameManager.Instance.hour.ToString("00") + ":" + GameManager.Instance.minutes.ToString("00") + ":" + GameManager.Instance.seconds.ToString("00");
    }

    public void startEfect(float _time,bool _effect)
    {
        StartCoroutine(waitTimeEffectBar(_time, _effect));
    }

    public IEnumerator waitTimeEffectBar(float _time, bool _efect)//true positive carbon - false negative efect
    {
        if (_efect)
        {
            GreenCarbonEfectBar.SetActive(true);
            yield return new WaitForSeconds(_time);
            GreenCarbonEfectBar.SetActive(false);
        }
        else
        {
            RedCarbonEfectBar.SetActive(true);
            yield return new WaitForSeconds(_time);
            RedCarbonEfectBar.SetActive(false);
        }
        
    }
}
