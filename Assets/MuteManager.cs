using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{
    public Button muteB;
    private Sprite tema1;
    private Sprite tema2;
    private Sprite tema3;
    public Sprite mutedPicture;
    public Sprite unmutedPicture;
    private bool isMuted;
    // Start is called before the first frame update
    void Start()
    {
        CheckStatus();

    }
    //ob kliku gumba se kliče ta metoda
    public void MutePressed()
    {
        isMuted = !isMuted;
        muteB.image.sprite = isMuted ? mutedPicture : unmutedPicture;
        AudioListener.pause = isMuted;
        // če je isMuted true vstavi 1 če ne vstavi v playerprefs 0
        // da ostane shranjeno za naslednjo sekcijo
        PlayerPrefs.SetInt("Muted",isMuted ? 1:0);
        
    }

    // preveri če je v prefernces natavljeno muted or unmuted
    private void CheckStatus()
    {
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            isMuted = true;
            muteB.image.sprite = mutedPicture;
        }
        else
        {
            isMuted = false;
            muteB.image.sprite = unmutedPicture;
        }
        AudioListener.pause = isMuted;
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
