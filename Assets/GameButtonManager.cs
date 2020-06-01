using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonManager : MonoBehaviour
{
    private bool PressedPause;
    // Start is called before the first frame update
    void Start()
    {
        PressedPause = false;
    }
    
    public void BackPressed()
    {
        //if potreben da damo stanje nazaj
        if (PressedPause)
            Time.timeScale = 1;
        SceneManager.LoadScene("start");
    }
    //pavzira igro, če odpavziramo počaka 1s in odpavzira
    public void PausePressed()
    {
        PressedPause = !PressedPause;
        if (PressedPause)
            Time.timeScale = PressedPause ? 0 : 1;
        else
            StartCoroutine(WaitCoroutine());
    }
    //korutina, ki čaka 1 s ko odpavziramo
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = PressedPause ? 0 : 1;
    }
    public void RestartPressed()
    {
        if (PressedPause)
            Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

}
