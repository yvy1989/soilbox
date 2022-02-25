using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public event Action<int,bool> OnManageSoilwhithId;

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

    public int startMoney;
    public int currentMoney;

    public int startCarbon;
    public int currentCarbon;

    ////////////////////////////////////VAI VIRAR UI manager////////////////////////////
    public Canvas menuCanvas;

    public GameObject MainMenu;

    public GameObject ConfirmationMenu;

    public GameObject InfoMenu;
    public Text InfoText;


    SoilBehavior soil;

    public Text Soil_ID;

    public Text OperationTxt;

    public Text MoneyTxt;
    public Text CarbonTxt;

    ///////////////////////////////////////////////////////////////////////////////////

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
        currentMoney = startMoney;
        currentCarbon = startCarbon;

        status = GameStatus.selectTerrain;
        ConfirmationMenu.SetActive(false);
        InfoMenu.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        menuCanvas.enabled = isMenuActive;

        Clique();

        Debug.Log(MainMenuOption);
        UpdateCreditsValues();
    }

    private void UpdateCreditsValues()
    {
        CarbonTxt.text = currentCarbon.ToString();
        MoneyTxt.text = currentMoney.ToString();
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

            InfoMenu.SetActive(false);/// desabilita infoBox

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
        OperationTxt.text = "venda";
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
       
    }

    public void ManageTerrain(int _option)
    {
        // testar
        MainMenuOption = _option;///verificar se usa
    }

    ///////////////////////////////////////////
    public void ConfirmOperation()// Compra e Venda
    {


        if (MainMenuOption == 1) {//COMPRA    verificar se tem grana e se esta disponivel
            if (soil.isAvaiable && currentMoney>=soil.price)
            {
                myTerrains.Add(TempTerrain);
                TempTerrain = null;
                //evento para mudar o valor de disponibilidade depois da compra
                if (OnManageSoilwhithId != null) //// CHAMDA DO ENVENTO
                    OnManageSoilwhithId(soil.TerrenoId,false);//passa como parametro o id para soilBehavior para deixar indisponivel

                currentMoney -= soil.price;//retira o valor do terreno do current money

                ConfirmationMenu.SetActive(false);
                cancelSelectionOperation();// desabilita main menu
            }
            else
            {
                if(currentMoney < soil.price)
                {
                    ShowInfo("Voce nao tem dinheiro");
                }
                else
                {
                    ShowInfo("Terreno indisponivel");
                }
                
                //Debug.Log("Indisponivel");
                return;

            }


        }
        if (MainMenuOption == 2)// venda ////BUG DE VENDA 2X ***************************************************************************
        {
            foreach (var item in myTerrains)
            {
                SoilBehavior tempSoil = item.GetComponent<SoilBehavior>();
                if (tempSoil != null)
                {
                    if(tempSoil.TerrenoId == soil.TerrenoId && !soil.isAvaiable)
                    {
                        Debug.Log("Vendeu");
                        currentMoney += soil.price;
                        //evento para mudar o valor de disponibilidade depois da venda
                        if (OnManageSoilwhithId != null) //// CHAMDA DO ENVENTO
                            OnManageSoilwhithId(soil.TerrenoId, true);//passa como parametro o id para soilBehavior para deixar disponivel

                        ShowInfo("Venda Realizada com sucesso!!");

                        ConfirmationMenu.SetActive(false);
                        cancelSelectionOperation();// desabilita main menu
                        return;
                    }

                    
                }
                
            }
            ShowInfo("Esse terreno nao e seu!!"); //varreu toda a lista e nao encontrou
            return;
        }

    }

    private void ShowInfo(string _info)
    {
        InfoMenu.SetActive(true);
        InfoText.text = _info;
        isMenuActive = true;
      
    }

    public void NotConfirmOperation()
    {
        ConfirmationMenu.SetActive(false);
    }

}
