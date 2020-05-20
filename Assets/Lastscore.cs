using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lastscore : MonoBehaviour
{
    public Text mytext;
    public static int scorepoints = 0;

    void Start()
    {
        scorepoints = score.scorepoints;
        mytext.text = scorepoints.ToString();
    }

    public void savescore(int scoree)
    {
        //lastscore += scoree;
    }
}
