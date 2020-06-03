
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    private Image ozadje;
    private Sprite themeSprite1;
    private Sprite themeSprite2;
    private Sprite themeSprite3;

    // Start is called before the first frame update
    void Start()
    {
       
        ozadje=GameObject.Find("Image").GetComponent<Image>();
        // teme morajo biti v Resource folderju, da spodnje metode delajo
        themeSprite1=Resources.Load<Sprite>("zvezde");
        themeSprite2=Resources.Load<Sprite>("sonce");
        themeSprite3=Resources.Load<Sprite>("morje");
        
        //prevri katera tema je aktivna in mute
        changeTheme();
        checkMute();
    }
    private void changeTheme()
    {
        if (PlayerPrefs.GetInt("tema")==1)
        {
            ozadje.sprite = themeSprite1;
        } else if (PlayerPrefs.GetInt("tema")==2)
        {
            ozadje.sprite = themeSprite2;
        }else if (PlayerPrefs.GetInt("tema") == 3)
        {
            ozadje.sprite = themeSprite3;
        }
            
    }
    private void checkMute()
    {
        if (PlayerPrefs.GetInt("Muted") == 1)
            AudioListener.pause = true;
        else
            AudioListener.pause = false;
    }
}
