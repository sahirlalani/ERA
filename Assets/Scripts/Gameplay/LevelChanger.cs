using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{

    //public GameObject LevelController;
    //public Scenes[] Levels;
    //public string[] sceneList;
    //private static int CurrentAreaLevel = 0;
    //bool GoneUpOnce = false;
    
    public MenuController menuController;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //Levels[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnTriggerEnter2D (Collider2D collider)
    {
       // if (!GoneUpOnce)
       // {    
           // GoUpLevel();
            //GoneUpOnce = true;
        //}
       // else
        //{
            //GoDownLevel();
            //GoneUpOnce = false;
       // }

       //GoUpLevel();
        
        MenuController.WinTriggered = true; 

    }

    /*
    public void GoUpLevel()
    {
        Levels[CurrentAreaLevel].SetActive(false);
        Levels[CurrentAreaLevel + 1].SetActive(true);
        CurrentAreaLevel += 1;
    }

    public void GoDownLevel()
    {
        Levels[CurrentAreaLevel].SetActive(false);
        Levels[CurrentAreaLevel - 1].SetActive(true);
        CurrentAreaLevel -= 1;
    }

    public void NextScene()
    {
        //SceneManagement.LoadScene("UpperLevel1");
    }
    */
}
