using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe responsavel por gerenciar os spawns apos sair da UI ou cancelar estes spawns
/// mover por snapGrid dentro do terreno
/// Spawnar o asset que se encontra dentro da pasta RESOURCES
/// </summary>

public class BlueprintBehavior : MonoBehaviour
{
    bool canAdd = false;

    UnitUpgrade unit = null;
    RaycastHit hit;
    Vector3 movePoint;
    public Vector3 GridSizeToSnap;
    MouseOverUnit m; // variavel temporaria para saber verificar se posso adicionar unidades ao terreno

    //public GameObject prefab;

    public string unitName;/////////////////////////!!!!!!!!!!! IMPORTANTE o mesmo nome do asset na pasta Resources via inspector

    GameObject _PrefabUnit;
    // Start is called before the first frame update
    void Start()
    {

        _PrefabUnit = Resources.Load(unitName) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 3)))//1<<3 tag solo
        {
            Vector3 snapPosition = new Vector3(Mathf.RoundToInt(hit.point.x / GridSizeToSnap.x) * GridSizeToSnap.x,
                                               hit.point.y,
                                               Mathf.RoundToInt(hit.point.z / GridSizeToSnap.z) * GridSizeToSnap.z);
            transform.position = snapPosition;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 7)))//1<<7 unit
        {
            unit = hit.transform.GetComponent<UnitUpgrade>();
            canAdd = !unit.isOnTerrain;
        }
        else
        {
            canAdd = true;
        }



        if (Input.GetMouseButton(0) && canAdd)//clicou com o esquerdo do mouse
        {
            //m.isAvaiable = true;//teste

            if (_PrefabUnit != null)
            {

                _PrefabUnit.GetComponent<UnitUpgrade>().isOnTerrain = true;
                Instantiate(_PrefabUnit, transform.position, transform.rotation);

                

                if (_PrefabUnit.GetComponent<UnitUpgrade>().typeUnity == 0)// se for planta
                {
                    GameManager.Instance.addCarbon(GameManager.Instance.CostPlantatipnCarbon);// adicao de carbono qndo planta milho
                    //Efeito dano carbono
                    UiController.Instance.efectBar(false);
                    //Audio dano carbono
                    GameManager.Instance.RemoveMoney(GameManager.Instance.CostPlantationValue);// remover dinheiro

                }

                if (_PrefabUnit.GetComponent<UnitUpgrade>().typeUnity == 1)// se for arvore
                {
                    GameManager.Instance.addCarbon(GameManager.Instance.TreeCarbonValue);// remocao de carbono qndo planta arvores
                    GameManager.Instance.RemoveMoney(GameManager.Instance.CostTreeValue);// remover dinheiro

                }

                if (_PrefabUnit.GetComponent<UnitUpgrade>().typeUnity == 3)// se for celeiro
                {
                    
                    GameManager.Instance.upGradeStorage(25f);// atualiza o storage em 25 unidades
                    GameManager.Instance.RemoveMoney(GameManager.Instance.CostStorageValue);// remover dinheiro
                }

                Destroy(gameObject);
            }

        }
        if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Escape))//clicou com o direito do mouse ou apertou escape cancela o spawn da unidade
        {
            Destroy(gameObject);
        }
    }
}
