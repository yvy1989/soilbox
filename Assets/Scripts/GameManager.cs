using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public enum GameStatus
    {
        selectTerrain, ManageTerrain, GameOver, Win
    }

    public GameStatus status;

    public List<GameObject> myTerrains;
    public GameObject TempTerrain;

    RaycastHit TempHit;

    public static GameManager Instance;
    // Start is called before the first frame update


    public bool isMenuActive = false; //esconder o canvas

    ////////////////////////////////////VAI VIRAR UI manager
    public Canvas menuCanvas;

    public GameObject MainMenu;

    public GameObject ConfirmationMenu;

    SoilBehavior soil;

    public Text Soil_ID;

    public Text OperationTxt;

    ///////////////////////////////////////////////////////////

    int MainMenuOption;


    private void Awake()
    {
        menuCanvas.enabled = isMenuActive;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }
    void Start()
    {
        status = GameStatus.selectTerrain;
        ConfirmationMenu.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        menuCanvas.enabled = isMenuActive;

        Clique();

        Debug.Log(MainMenuOption);

    }

    private void Clique()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !isMenuActive) //verifica se clicou com o mouse e nao esta em cima de um GameObject
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            ////posicao menu de acordo com o clique do mouse
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f; //distance of the plane from the camera
            MainMenu.GetComponent<RectTransform>().position = screenPoint;
            /////



            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("soil"))
                {
                    isMenuActive = true;
                    hit.transform.GetComponent<MeshRenderer>().enabled = true;
                    TempHit = hit;// para ser usado em cancelOperation

                    soil = hit.collider.gameObject.GetComponent<SoilBehavior>(); //pega o objeto Soil via raycast

                    if (soil != null) Soil_ID.text = soil.TerrenoId.ToString();// atribui a UI o valor do texto///////////////////////////////////////////////UI

                    TempTerrain = hit.collider.gameObject;

                }

            }

        }
    }

    public void cancelSelectionOperation()//////////////////////////////////////////////////////////////////////////////////////////////////////////////////UI
    {
        isMenuActive = false;
        TempHit.transform.GetComponent<MeshRenderer>().enabled = false;
    }


    public void BuyTerrain(int _option)
    {
        OperationTxt.text = "compra";
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
    }

    public void SellTerrain(int _option)
    {
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
        OperationTxt.text = "venda";
    }

    public void ManageTerrain(int _option)
    {
        //////verificar se o id_terreno pertence ao player
        if (myTerrains != null)
        {
            foreach (var terrain in myTerrains)
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }
        MainMenuOption = _option;
    }

    ///////////////////////////////////////////
    public void ConfirmOperation()
    {      
        if (MainMenuOption == 1) {//verificar se tem grana

            myTerrains.Add(TempTerrain);
            TempTerrain = null;
        }
        if (MainMenuOption == 2)
        {

            //remover e adicionar grana
            
        }

        ConfirmationMenu.SetActive(false);
    }

    public void NotConfirmOperation()
    {
        ConfirmationMenu.SetActive(false);
    }

}
