using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SoilBehavior : MonoBehaviour
{
    public int TerrenoId;

    private bool isSelected = false; //verifica o solo q esta ativo e vai ficar vermelho

    public float price;

    public string description;

    public bool isAvaiable = true; // variavel q define se o terreno pode ser comprado

   

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnManageSoilwhithId += changeAvaiablility;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if (GameManager.Instance.status == GameManager.GameStatus.selectTerrain)//verifica se esta escolhendo o terreno
        {
            if (!isSelected && !GameManager.Instance.isMenuActive)
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
 

        //Debug.Log("passou");
    }
    private void OnMouseExit()
    {
        if (GameManager.Instance.status == GameManager.GameStatus.selectTerrain)//verifica se esta escolhendo o terreno
        {
            if (!isSelected && !GameManager.Instance.isMenuActive)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }


        
        //Debug.Log("saiu");
    }


    void changeAvaiablility(int id)// funcao q vair ser usada no evento e recebe o valor como parametro do game manager
    {
        if (id == TerrenoId)
        {
            isAvaiable = false;
        }
    }

}
