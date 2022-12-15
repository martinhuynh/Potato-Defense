using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public aButton escButton;
    public aButton mButton;
    public aShop mShop;

    public static bool gameIsPaused = false;
    public  bool inShop = false; 
    /*to decided whether should suspend Escape key
    otherwise there will be bug because pause menu will be called in shop by Escape key*/
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inShop == false) //only can use Escape key when player isn't have shop opened
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
                escButton.gameObject.SetActive(!escButton.gameObject.activeSelf);
                mButton.gameObject.SetActive(!mButton.gameObject.activeSelf);
                //put display button and pause menu job to PauseGame and ResumeGame functions

                if (gameIsPaused == false)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if(inShop == false)
            {
                mShop.gameObject.SetActive(true);
                escButton.gameObject.SetActive(false);
                mButton.gameObject.SetActive(false);
                IsInShop();
            }
            else
            {
                mShop.gameObject.SetActive(false);
                escButton.gameObject.SetActive(true);
                mButton.gameObject.SetActive(true);
                NotInShop();
            }

           
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;

        pauseMenu.gameObject.SetActive(true);
        escButton.gameObject.SetActive(false);
        mButton.gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;

        pauseMenu.gameObject.SetActive(false);
        escButton.gameObject.SetActive(true);
        mButton.gameObject.SetActive(true);
    }

    public void IsInShop()
    {
        inShop = true;
    }
    public void NotInShop()
    {
        inShop = false;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerInventory.reset();
        PlayerStats.restart();
        Time.timeScale = 1;
    }

    public int returnInShopStatus()
    {
        int boolNumber = 0;
        if(inShop == false)
        {
            boolNumber = 0;
        }
        else
        {
            boolNumber = 1;
        }
        return boolNumber;
    }
}
