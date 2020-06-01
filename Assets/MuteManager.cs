using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{
    public Image ozadje;
    public Button muteB;
    public Sprite TemaSprite1;
    public Sprite TemaSprite2;
    public Sprite TemaSprite3;
    public Sprite mutedPicture;
    public Sprite unmutedPicture;
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;

    
    private bool isMuted;
    // Start is called before the first frame update
    void Start()
    {
        CheckStatus();
        changeTheme();

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

    public void BackPressed()
    {
        SceneManager.LoadScene("start");
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
    //pred začetkom nastavi primerno temo
    private void changeTheme()
    {
        int i = PlayerPrefs.GetInt("tema");
        if (i==1)
        {
            ozadje.sprite =TemaSprite1;
            toggle1.isOn = true;

        } else if (i==2)
        {
            ozadje.sprite = TemaSprite2;
            toggle2.isOn = true;
        }else if (i == 3)
        {
            ozadje.sprite = TemaSprite3;
            toggle3.isOn = true;
        }
    }
    
    // nastavi in spremeni temo za celotno aplikacijo
    public void ChangeToTheme1()
    {
        ozadje.sprite = TemaSprite1;
        PlayerPrefs.SetInt("tema",1);
        
    }
    public void ChangeToTheme2()
    {
        ozadje.sprite = TemaSprite2;
        PlayerPrefs.SetInt("tema",2);
        
    }
    public void ChangeToTheme3()
    {
        ozadje.sprite = TemaSprite3;
        PlayerPrefs.SetInt("tema",3);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
