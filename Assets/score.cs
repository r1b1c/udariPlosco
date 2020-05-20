using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{

    public Text mytext;

    public static int scorepoints = 0;
    public int scoree = 0;

    //določanje rezultata, štetje točk
    public void Scoreupdate(int score)
    {
        scoree += score;
        scorepoints = scoree;
        mytext.text = scoree.ToString();
    }

    //test ce commita

}
