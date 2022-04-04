using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{
    public float CostPlantationValue;
    public float CostPlantatipnCarbon;


    public float CostTreeValue;
    public float TreeCarbonValue;


    public GameObject[] UnitPrefab_blueprint;

    public void spawn_Units_blueprint_ByIndex(int index)
    {


        switch (index)
        {
            case 0:// plantacao;
                {
                    if (GameManager.Instance.currentMoney > 0)
                    {
                        GameManager.Instance.addCarbon(CostPlantatipnCarbon);// adicao de carbono qndo planta milho
                        GameManager.Instance.RemoveMoney(CostPlantationValue);// remover dinheiro
                        Instantiate(UnitPrefab_blueprint[index]);// acessado via Button
                    }
                    else
                    {
                        GameManager.Instance.ShowInfo("Voce nao tem dinheiro sufuciente");
                    }


                    break;
                }
            case 1:// arvore;
                {
                    if (GameManager.Instance.currentMoney > 0)
                    {
                        GameManager.Instance.addCarbon(TreeCarbonValue);// remocao de carbono qndo planta arvores
                        GameManager.Instance.RemoveMoney(CostTreeValue);// remover dinheiro
                        Instantiate(UnitPrefab_blueprint[index]);// acessado via Button
                    }
                    else
                    {
                        GameManager.Instance.ShowInfo("Voce nao tem dinheiro sufuciente");
                    }
                    break;
                }

        }
    }
}
