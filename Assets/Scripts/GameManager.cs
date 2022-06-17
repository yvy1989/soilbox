using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Tempo")]
    public float CurrentTimer;
    public int hour = 0;
    public int minutes = 0;
    public int seconds = 0;
    public int milliseconds =0;

    public Text TextRankUI;


    [Header("GameOver")]
    public Text GameOverTxt;
    public GameObject GameOverPanel;
    public bool isGameOver = false; //controla so o jogo acabou

    [Header("Plantas")]
    public float CostPlantationValue; //custo de dinheiro para plantar
    public float CostPlantatipnCarbon;//custo de carbono para plantar
    public float StoragePlantFill;//custo de storage para colher


    [Header("Arvore")]
    public float CostTreeValue;//custo de dinheiro para plantar arvore
    public float TreeCarbonValue;//custo de carbono por arvore para remover
    public float DownTreeCarbon;//custo de carbono por derrubar arvore
    public float StorageTreeFill;//custo de storage para madeira arvore


    [Header("Animal")]
    public float CostAnimalValue;//custo de dinheiro para criar animal
    public float AnimalCarbonValue;//custo de carbono para criar animal
    

    [Header("Armazem")]
    public float initialStorage, currentStorage, finalStorage;
    public float CostStorageValue;//custo de dinheiro para criar um celeiro/silo

    public event Action<int,bool> OnManageSoilwhithId;
    public event Action<int> OnChangeCamera;

    public enum GameStatus
    {
        selectTerrain, ManageTerrain, GameOver, Win
    }

    [Header("Game Status")]
    public GameStatus status;

    public List<GameObject> myTerrains; //lista que guarda seus terrenos
    public GameObject TempTerrain; // terreno temporario q vai ser usado no raycast

    RaycastHit TempHit;

    public static GameManager Instance;
    // Start is called before the first frame update


    public bool isMenuActive = false; //esconder o canvas

    [Header("Dinheiro")]
    public float startMoney;
    public float currentMoney;// MEU DINHEIRO ATUAL
    public float MaxMoney;


    [Header("Carbono")]
    public float startCarbon;
    public float currentCarbon;
    public float maxCarbon;

    ////////////////////////////////////UI ////////////////////////////
    public Canvas menuCanvas;

    public GameObject MainMenu;

    public GameObject Settings;
    bool isSettingActive;

    public GameObject ConfirmationMenu;

    public GameObject InfoMenu;
    Vector2 InfoMenScreenPoin;
    public Text InfoText;


    SoilBehavior soil;

    public Text Soil_ID;
    

    public Text OperationTxt;

    public Text MoneyTxt;
    public Text CarbonTxt;
    ////////////////////////////////ACESSADO VIA MOUSEOVERUNIT//////////////////////
    public bool CanClickUnit;

    ///////////////////////////////////////////////////////////////////////////////////

    int MainMenuOption;

    

    private void Awake()
    {
        isGameOver = false;
        menuCanvas.enabled = isMenuActive;
        Instance = this;

    }
    void Start()
    {
        CanClickUnit = true;
        AudioController.Instance.changeMusicToGame();// muda para a musica do jogo

        GameOverPanel.SetActive(false);

        Settings.SetActive(false);
        isSettingActive = false;

        currentMoney = startMoney;
        currentCarbon = startCarbon;

        currentStorage = initialStorage;

        status = GameStatus.selectTerrain;
        ConfirmationMenu.SetActive(false);
        InfoMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        ////////////////////////////////////////////////////////////////GAME FUNCIONANDO//////////////////////////////////////////////////////////
        if (isGameOver == false)// verifica se o jogo terminou
        {
            GameTime();

            menuCanvas.enabled = isMenuActive;

            Settings.SetActive(isSettingActive);

            Clique();

            UpdateCreditsValues();

            LimitCarbonControll();
            LimitMoneyControll();
            
            LimitStorageAmount();

            checkGameOver();
            

            if (isGameOver)
            {
                if (currentMoney <= 0)
                {
                    GameOverTxt.text = "Your money is gone, try again";
                }
                if (currentCarbon >= maxCarbon)
                {
                    GameOverTxt.text = "You've emitted too much carbon into the atmosphere, try again";
                }
                GameOverPanel.SetActive(true);
                TextRankUI.text = hour.ToString("00") + " Hours " + minutes.ToString("00") + " Minutes and " + seconds.ToString("00") + " seccnds";
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        




    }

    private void LimitStorageAmount()
    {
        if (currentStorage >= finalStorage)
        {
            currentStorage = finalStorage;
        }
    }

    private void GameTime()
    {
        CurrentTimer += Time.deltaTime;

        minutes = Mathf.FloorToInt((CurrentTimer / 60F) % 60F);
        seconds = Mathf.FloorToInt(CurrentTimer % 60F);
        milliseconds = Mathf.FloorToInt((CurrentTimer * 100F) % 100F);

        if (minutes >= 59)
        {
            hour++;
        }

    }

    public void ChangeVisibilitySettings()
    {
        isSettingActive = !isSettingActive;
    }

    public void resetGame()
    {
        GameOverPanel.SetActive(false);
        AudioController.Instance.changeMusicToMenu();
        
        SceneManager.LoadScene("menu");    
    }



    private void checkGameOver()
    {
        if(currentCarbon>= maxCarbon || currentMoney <= 0)
        {
            isGameOver = true;
        }
    }

    private void LimitCarbonControll()// usado na barra de carbono
    {
        if (currentCarbon <= -100)
            currentCarbon = -100;
        if (currentCarbon >= 100)
            currentCarbon = 100;
    }

    private void LimitMoneyControll()//usado na barra de dinheiro
    {
        if (currentMoney <= 0)
            currentMoney = 0;
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
            InfoMenScreenPoin = screenPoint;
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

    public void cancelInfo()//////////////////////////////////////////////////////////////////////////////////////////////////////////////////UI
    {
        InfoMenu.SetActive(false);
    }


    public void BuyTerrain(int _option)
    {
        OperationTxt.text = "BUY";
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
    }

    public void SellTerrain(int _option)
    {
        OperationTxt.text = "SELL";
        MainMenuOption = _option;
        ConfirmationMenu.SetActive(true);
       
    }

    public void ManageTerrain(int _option)
    {
        MainMenuOption = _option;///verificar se usa

        if (myTerrains.Count==0)
        {
            ShowInfo("You don't own a land");
            
        }
        //verificar se o terreno e seu (se esta na lista)
        foreach (var item in myTerrains)
        {
            SoilBehavior itemLista = item.GetComponent<SoilBehavior>();// terreno q esta na lista ou nao
            SoilBehavior temp = TempTerrain.GetComponent<SoilBehavior>();// terreno temporario q veio via raycast qndo clicou
            
            if(itemLista!=null)// tem terreno na lista e voce clicou em um terreno
            {
                if ((itemLista.TerrenoId == temp.TerrenoId) )//terreno e seu
                {
                    status = GameStatus.ManageTerrain;

                    
                    ////passar como parametro de um evento o id do terreno q se esta gerenciando
                    if (OnChangeCamera != null) //// CHAMDA DO ENVENTO
                        OnChangeCamera(itemLista.TerrenoId);//passa como parametro o id para trocar a camera
                    cancelSelectionOperation();// desabilita main menu
                    break;

                }
                else
                {
                    ShowInfo("this land is not yours");
                }
            }
        }
    
    }

    ///////////////////////////////////////////
    public void ConfirmOperation()// Compra e Venda
    {


        if (MainMenuOption == 1) {//COMPRA    verificar se tem grana e se esta disponivel
            if (soil.isAvaiable && currentMoney>=soil.price)
            {
                GameObject terrain = new GameObject();// cria um novo gameObject
                terrain = TempTerrain;// pega o GameObject q veio do raycast
                myTerrains.Add(terrain);//adiciona o terreno criado na lista de terrenos

                TempTerrain = null;
                //evento para mudar o valor de disponibilidade depois da compra
                if (OnManageSoilwhithId != null) //// CHAMDA DO ENVENTO
                    OnManageSoilwhithId(soil.TerrenoId,false);//passa como parametro o id para soilBehavior para deixar indisponivel

                currentMoney -= soil.price;//retira o valor do terreno do current money
                AudioController.Instance.PlayEfect(6); //feedback positivo qndo compra terreno
                


                ConfirmationMenu.SetActive(false);
                cancelSelectionOperation();// desabilita main menu
            }
            else
            {
                if(currentMoney < soil.price)
                {
                    ShowInfo("You don`t have enough money");
                }
                else
                {
                    ShowInfo("Land Unavailable");
                }
                
                return;

            }


        }
        if (MainMenuOption == 2)// venda ////
        {
            foreach (var item in myTerrains)
            {
                SoilBehavior tempSoil = item.GetComponent<SoilBehavior>();
                if (tempSoil != null)
                {
                    if(tempSoil.TerrenoId == soil.TerrenoId && !soil.isAvaiable)
                    {
                        currentMoney += soil.price;
                        //evento para mudar o valor de disponibilidade depois da venda
                        if (OnManageSoilwhithId != null) //// CHAMDA DO ENVENTO
                            OnManageSoilwhithId(soil.TerrenoId, true);//passa como parametro o id para soilBehavior para deixar disponivel

                        ShowInfo("Sale made successfully!!");
                        myTerrains.Remove(item);// remove o terreno da lista

                        ConfirmationMenu.SetActive(false);
                        cancelSelectionOperation();// desabilita main menu
                        return;
                    }

                    
                }
                
            }
            ShowInfo("this land is not yours!"); //varreu toda a lista e nao encontrou
            return;
        }

    }

    public void ShowInfo(string _info)
    {
        InfoMenu.SetActive(true);
        InfoMenu.GetComponent<RectTransform>().position = InfoMenScreenPoin;
        InfoText.text = _info;
        //isMenuActive = true;
      
    }

    public void NotConfirmOperation()
    {
        ConfirmationMenu.SetActive(false);
    }

    public void upGradeStorage(float amount)
    {
        if (finalStorage < 100)// para nao ultrapassar o limite do deposito
        {
            finalStorage += amount;
        }
        
    }

    public void fillStorage(float amount)
    {
        if (currentStorage < finalStorage)//verifica se o meu deposito nao esta cheio
        {

            currentStorage += amount;
        }
        else// caso contrario mandar msg erro
        {
            ShowInfo("full warehouse, create more barns/silos");
        }
    }


    public void addCarbon(float amount)
    {
        AudioController.Instance.PlayEfect(9);
        UiController.Instance.startEfect(0.6f, false);
        currentCarbon += amount;
    }



    public void RemoveCarbon(float amount)
    {
        AudioController.Instance.PlayEfect(6);
        UiController.Instance.startEfect(0.6f, true);
        currentCarbon -= amount;
    }

    public void addMoney(float amount)
    {
        AudioController.Instance.PlayEfect(5);
        currentMoney += amount;
    }

    public void RemoveMoney(float amount)
    {
        currentMoney -= amount;
    }

}
