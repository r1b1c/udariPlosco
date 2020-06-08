using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class ResoltManageScript : MonoBehaviour
{
    private Player data;
    private int scorepoints = score.scorepoints;
    private DatabaseReference _databaseReference;
    private string dataUrl="https://udariploscodb.firebaseio.com/";
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("data saving 1");

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dataUrl);
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        
        //fake objekt todo static objekt Player ki se nastavi ob prijavi, odstrani ob odjavi (mogoče v playerpref bool IsSigned=true/false) 
        data=new Player("t.v@g.com","QmiTV4E3LuWUPe2xGf4UMO1kRWF3",new []{10,9,8,7,5,6,4,3,2,1});
        
        
            SaveData();
        //LoadData();
    }

    private void SaveData()
    { 
        string jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);
       ScoreBoard sb=new ScoreBoard();
      // _databaseReference.Child("scoreBoard").SetValueAsync(JsonUtility.ToJson(sb)); 
        //_databaseReference.Child(data.id).SetValueAsync(jsonData);
        Debug.Log("data saved 2"+data.email);
    }

    public void LoadData()
    {
        FirebaseDatabase.DefaultInstance.GetReference(data.id)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("error se je pojavil");
                    //todo če ni interneta se izpiše lokalni rezultati iz playerpref
                } else if (task.IsCompleted)
                {
                    Debug.Log("uspešno smo vrnili podatke");
                    DataSnapshot snapshot = task.Result;
                    string returnedData = snapshot.GetRawJsonValue();
                    Debug.Log("vrnejeno "+returnedData);
                } else if (task.IsCanceled)
                {
                    //todo če ni interneta se izpiše lokalno iz playerpref
                    Debug.Log("klicanje je bilo preklicano");
                }
            });
    }
}

[Serializable]
internal class ScoreBoard
{
    public int [] top100=new int[100];

    public ScoreBoard()
    {
    }
}
[Serializable]
internal class ScoreClassForBoard
{
    public string id;
    public int score;

    public ScoreClassForBoard()
    {
        this.id = "neobstaja";
        score = 0;
    }

    public ScoreClassForBoard(string id, int score)
    {
        this.id = id;
        this.score = score;
    }
}
