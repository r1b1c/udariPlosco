using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    public Image ozadje;
    //private Sprite themeSprite1;
    //private Sprite themeSprite2;
    //private Sprite themeSprite3;
    public Sprite themeSprite1;
    public Sprite themeSprite2;
    public Sprite themeSprite3;

    // Start is called before the first frame update
    void Start()
    { //themeSprite1=Resources.Load<Sprite>("خلفيات نصوص");
      //  themeSprite2=Resources.Load<Sprite>("f8c1c13d8dda5e495350720e55c2cdf3");
     //   themeSprite3=Resources.Load<Sprite>("SpriteName");
        //prevri katera tema je aktivna in mute
        changeTheme();
        checkMute();
    }

    // Update is called once per frame
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
    void Update()
    {
        
    }
}
