using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
  public string email;
  public string id;
  public string username;

  public Player()
  {
  }

  public Player(string email, string id,string username)
  {
    this.email = email;
    this.id = id;
    this.username = username;
  }
  public Player(string email, string id)
  {
    this.email = email;
    this.id = id;
  }
  public Player( string id)
  {
    this.id = id;
  }
  


}
