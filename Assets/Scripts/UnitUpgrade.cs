using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// controle das unidades ex: solo arvores animais etc
/// </summary>
public class UnitUpgrade : MonoBehaviour
{
    public bool isReady = false;

    public GameObject TreeDown;
    public GameObject TreeFinal;


    NavMeshAgent AnimalAgent;
    Animator animalAnim;
    public Transform[] GrassPosition;

   
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
        else // e um animal
        {
            animalAnim = transform.GetComponentInChildren<Animator>();
            AnimalAgent = transform.GetComponentInChildren<NavMeshAgent>();
            //criar rotina de IA

            StartCoroutine(AnimalAnimation());

        }
        
    }
    

    

    private void Update()
    {
        //Debug.Log(isReady);
        if (animalAnim != null)
        {
            animalAnim.SetFloat("speed", AnimalAgent.velocity.magnitude);
            Debug.Log(AnimalAgent.velocity.magnitude);
        }
        

    }

    IEnumerator AnimalAnimation()
    {
        while(true)
        {
            AnimalAgent.SetDestination(GrassPosition[Random.Range(0, GrassPosition.Length)].position);

            yield return new WaitForSeconds(2);

        }
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
