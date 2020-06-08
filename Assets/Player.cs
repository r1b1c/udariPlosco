using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
  public string email;
  public string id;
  public int[] bestScores=new int[10];

  public Player()
  {
  }

  public Player(string email, string id,int [] score)
  {
    this.email = email;
    this.id = id;
    bestScores = score;

  }
  public Player(string email, string id)
  {
    this.email = email;
    this.id = id;
  }
  


}
