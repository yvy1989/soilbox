using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlant : MonoBehaviour
{
    public int TimesToGrowUp;
    public float initialGrowBroto;
    public float sizePlantDuration;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BrotoEnable", initialGrowBroto);
        StartCoroutine(GrowCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BrotoEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    
    IEnumerator GrowCoroutine()
    {
        for (int i = 0; i < TimesToGrowUp; i++)
        {
            yield return new WaitForSeconds(sizePlantDuration);
            Debug.Log("cresceu");
            transform.GetChild(0).gameObject.transform.localScale += new Vector3(0,0,100);
            transform.GetChild(0).gameObject.transform.localPosition -= new Vector3(0,0.301f,0);



        }
    }
}
