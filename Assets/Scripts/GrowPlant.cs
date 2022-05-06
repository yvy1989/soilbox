using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{

   
    [Header("0 -> plants | 1 -> trees")]
    [Header("typeUnity")]
    public int typeUnity;

    public int levelsCount;
    public int CurrentlevelsCount;

    public int Plant_final_stage;

    public float sizePlantDuration;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(GrowCoroutine());
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
}
