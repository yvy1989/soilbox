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
    RaycastHit hit;
    Vector3 movePoint;
    public Vector3 GridSizeToSnap;
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

        if (Input.GetMouseButton(0))//clicou com o esquerdo do mouse
        {
            if (_PrefabUnit != null)
            {
                //Instantiate(prefab, transform.position, transform.rotation);
                //GameObject g = Instantiate(Resources.Load($"Prefabs/{unitName}"), transform.position, transform.rotation) as GameObject;
                //Vector3 snapPosition = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y,Mathf.RoundToInt(transform.position.z));
                Instantiate(_PrefabUnit, transform.position, transform.rotation);

                if (_PrefabUnit.GetComponent<UnitUpgrade>().typeUnity == 0)// pega a referencia do script growPlant e verifica que tipo de unidade e
                {
                    GameManager.Instance.addCarbon(GameManager.Instance.CostPlantatipnCarbon);// adicao de carbono qndo planta milho
                    GameManager.Instance.RemoveMoney(GameManager.Instance.CostPlantationValue);// remover dinheiro
                }

                if (_PrefabUnit.GetComponent<UnitUpgrade>().typeUnity == 1)// pega a referencia do script growPlant e verifica que tipo de unidade e
                {
                    GameManager.Instance.addCarbon(GameManager.Instance.TreeCarbonValue);// remocao de carbono qndo planta arvores
                    GameManager.Instance.RemoveMoney(GameManager.Instance.CostTreeValue);// remover dinheiro
                }



                //Instantiate(_PrefabUnit, snapPosition, transform.rotation);
                Destroy(gameObject);
            }

        }
        if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Escape))//clicou com o direito do mouse ou apertou escape cancela o spawn da unidade
        {
            Destroy(gameObject);
        }
    }
}
