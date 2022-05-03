using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 snapPosition = new Vector3(Mathf.RoundToInt(hit.point.x/GridSizeToSnap.x)*GridSizeToSnap.x,
                                               hit.point.y, 
                                               Mathf.RoundToInt(hit.point.z/GridSizeToSnap.z)*GridSizeToSnap.z);
            transform.position = snapPosition;
        }

        if (Input.GetMouseButton(0))//clicou com o esquerdo do mouse
        {
            //Instantiate(prefab, transform.position, transform.rotation);
            //GameObject g = Instantiate(Resources.Load($"Prefabs/{unitName}"), transform.position, transform.rotation) as GameObject;
            //Vector3 snapPosition = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y,Mathf.RoundToInt(transform.position.z));
            Instantiate(_PrefabUnit, transform.position, transform.rotation);

            GameManager.Instance.addCarbon(GameManager.Instance.CostPlantatipnCarbon);// adicao de carbono qndo planta milho
            GameManager.Instance.RemoveMoney(GameManager.Instance.CostPlantationValue);// remover dinheiro

            //Instantiate(_PrefabUnit, snapPosition, transform.rotation);
            Destroy(gameObject);
        }
        if (Input.GetMouseButton(1))//clicou com o direito do mouse
        {
            Destroy(gameObject);
        }
    }
}
