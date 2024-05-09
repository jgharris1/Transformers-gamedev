using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        SetActiveHud(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)&& !isPaused)
        {
            SetActivePause(true);
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            SetActivePause(false);
        }
    }
    public void SetActiveHud(bool state)
    {
        pauseMenu.SetActive(!state);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetActivePause(bool state)
    {
        pauseMenu.SetActive(state);

        Time.timeScale = state ? 0 : 1;
        if(state){

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<grapplescript>().enabled = false;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<grapplescript>().enabled = true;

        }
        isPaused = state;
    }

    public void exitgame(){
        Application.Quit();
    }
    // public void Resume(){
    //     SetActiveHud(true);
    //     Time.timeScale = 1;
    // }

    // Update is called once per frame
    
}
