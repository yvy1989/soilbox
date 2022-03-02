using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintBehavior : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    //public GameObject prefab;

    public string unitName;/////////////////////////!!!!!!!!!!! IMPORTANTE o mesmo nome do aaset em Resources.Load via inspector

    GameObject _PrefabUnit;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        _PrefabUnit = Resources.Load(unitName) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 3)))//1<<3 tag solo
        {
            transform.position = hit.point;
        }

        if (Input.GetMouseButton(0))
        {
            //Instantiate(prefab, transform.position, transform.rotation);
            //GameObject g = Instantiate(Resources.Load($"Prefabs/{unitName}"), transform.position, transform.rotation) as GameObject;
            Instantiate(_PrefabUnit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
