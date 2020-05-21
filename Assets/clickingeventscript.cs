using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class clickingeventscript : MonoBehaviour
{

    //Kaj se zgodi ob kliku na kateri gumb
    public void startbutton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quitbutton()
    {
        Application.Quit();
    }

    public void aboutus()
    {
        SceneManager.LoadScene(2);
    }

    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
