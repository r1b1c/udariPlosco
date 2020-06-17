using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RememberLogin : MonoBehaviour
{
    public Toggle remeber;

    public InputField uporIme, geslo;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("UporabniskoIme") != ""&& PlayerPrefs.GetString("Geslo")!="")
        {
            uporIme.text = PlayerPrefs.GetString("UporabniskoIme");
            geslo.text = PlayerPrefs.GetString("Geslo");
            remeber.isOn = true;
        }
    }

    public void SavePlayerData()
    {
        if (remeber.isOn)
        {
            PlayerPrefs.SetString("UporabniskoIme",uporIme.text);
            PlayerPrefs.SetString("Geslo",geslo.text);

        }
        else
        {
            PlayerPrefs.SetString("UporabniskoIme","");
            PlayerPrefs.SetString("Geslo","");
        }
    }

    // Update is called once per frame
   
}
