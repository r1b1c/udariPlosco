using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
  public string email;
  public string id;
  //private int[] bestScores;

  public Player()
  {
  }

  public Player(string email, string id)
  {
    this.email = email;
    this.id = id;
  }


}
