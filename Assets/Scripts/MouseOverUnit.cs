using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverUnit : MonoBehaviour
{
    UnitUpgrade myUnit;

    public GameObject Unit;
    bool isPanelAvcive = false;
    GameObject painel;
    // Start is called before the first frame update
    void Start()
    {
        myUnit = Unit.GetComponent<UnitUpgrade>();//pega a referencia da unidade
        painel = GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject;
        painel.SetActive(isPanelAvcive); //desativa o painel
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(myPlant.CurrentlevelsCount);
    }

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnMouseDown()
    {
        
        isPanelAvcive = !isPanelAvcive;

        painel.SetActive(isPanelAvcive);



    }

    public void colher()
    {
        if (myUnit.isReady)//se a plante estiver pronta p colher ou derrubar
        {

            if (myUnit.typeUnity == 0)// se for planta
            {
                if (GameManager.Instance.currentStorage < GameManager.Instance.finalStorage)
                {
                    painel.SetActive(false);
                    Destroy(Unit);

                    GameManager.Instance.addMoney(myUnit.MoneyReward);// por enquanto tanto arvore qnto planta usam a mesma variavel
                    GameManager.Instance.fillStorage(GameManager.Instance.StoragePlantFill);// valor de armazenamento de plantas
                }
                else
                {
                    GameManager.Instance.ShowInfo("full storage");
                }
                
            }


            if (myUnit.typeUnity == 1)// se for arvore
            {
                if (GameManager.Instance.currentStorage < GameManager.Instance.finalStorage)
                {
                    painel.SetActive(false);
                    myUnit.downUnit();
                    Invoke("waitToDownTree", 4f);// espera 4 segundos antes de destruir a arvore
                    GameManager.Instance.addCarbon(GameManager.Instance.DownTreeCarbon);// da dano de carbono por derrubar arvore;

                    GameManager.Instance.addMoney(myUnit.MoneyReward);// por enquanto tanto arvore qnto planta usam a mesma variavel
                    GameManager.Instance.fillStorage(GameManager.Instance.StorageTreeFill);// valor de armazenamento de arvores
                }
                else
                {
                    GameManager.Instance.ShowInfo("full storage");
                }
                
            }



            if (myUnit.typeUnity == 3)// se for um objeto ex: celeiro
            {
                painel.SetActive(false);
                myUnit.downUnit();
                Invoke("waitToDownTree", 4f);// espera 4 segundos antes de destruir

            }
            /*
            else// se nao for arvore
            {           
                painel.SetActive(false);
                Destroy(Unit);
            }*/

            

            //GameManager.Instance.CostPlantatipnCarbon;

        }
        else
        {
            GameManager.Instance.ShowInfo("It is not yet possible to harvest or destroy");
            painel.SetActive(false);
        }
        
    }

    public void cancelar()
    {
        painel.SetActive(false);
        isPanelAvcive = false;
    }

    void waitToDownTree()
    {

        Destroy(Unit);
    }
}
