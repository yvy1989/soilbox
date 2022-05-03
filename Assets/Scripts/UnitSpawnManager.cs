using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{

    public GameObject[] UnitPrefab_blueprint;

    public void spawn_Units_blueprint_ByIndex(int index)// chamada qndo se clica no botao e recebe o index como parametro
    {


        switch (index)
        {
            case 0:// plantacao;
                {

                    if (GameManager.Instance.currentMoney > 0)
                    {
                        
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
                        GameManager.Instance.addCarbon(GameManager.Instance.TreeCarbonValue);// remocao de carbono qndo planta arvores
                        GameManager.Instance.RemoveMoney(GameManager.Instance.CostTreeValue);// remover dinheiro
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
