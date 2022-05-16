using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUpgrade : MonoBehaviour
{
    public bool isReady = false;

    public GameObject TreeDown;
    public GameObject TreeFinal;

   
    [Header("0 -> plants | 1 -> trees| 2 -> animal")]
    [Header("typeUnity")]
    public int typeUnity;

    public float MoneyReward; //dinheiro que se ganha ao colher ou dano ao derrubar // por enquanto tanto arvore qnto planta usam a mesma variavel

    public int levelsCount;
    public int CurrentlevelsCount;

    public int Plant_final_stage;

    public float sizePlantDuration;

    // Start is called before the first frame update
    void Start()
    {
        if (typeUnity != 2)// se nao for animal
        {
            StartCoroutine(GrowCoroutine());
        }
        
    }

    private void Update()
    {
        Debug.Log(isReady);
    }



    IEnumerator GrowCoroutine()
    {
        for (int i = 0; i < levelsCount; i++)
        {
            yield return new WaitForSeconds(sizePlantDuration);
            //Debug.Log("cresceu");//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            transform.GetChild(i).gameObject.SetActive(true);

            disableChild(i);
        }

        isReady = true;
    }

    void disableChild(int exept)// desabilita os outros childs
    {
        for (int i = 0; i < levelsCount; i++)
        {
            if (i == exept)
            {
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);

        }
    }

    public void downTree()
    {
        TreeFinal.SetActive(false);
        TreeDown.SetActive(true);
        isReady = false;
    }
}
