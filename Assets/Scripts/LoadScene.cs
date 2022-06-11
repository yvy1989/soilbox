using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

   
    public void loadSceneByName(string _name)
    {

 
        SceneManager.LoadScene(_name);

        
    }

    public void extitGame()
    {
        Application.Quit();
    }



}
