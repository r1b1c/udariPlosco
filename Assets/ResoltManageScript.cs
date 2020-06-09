using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Object = System.Object;


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
        
        //LoadData();
       // WriteNewScore("HnriL2JelbhcHXQObqDlHnJhPt62",56);
       LoadLeaderBoard();
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

    private void LoadLeaderBoard()
    {
        //vrnenajvečje 4ri rezultate ki pa niso vrnjeni sortirano!
        FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").LimitToLast(4).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error se je pojavil");
                //todo če ni interneta se izpiše lokalni rezultati iz playerpref
            } else if (task.IsCompleted)
            {
               
                DataSnapshot snapshot = task.Result;
                // v returnedDictonery imamo Vrnjen Dictonery<key(identifyer od vnosa),<Dictonery<string(ime "uid"ali"score"),Object(rezultat uid ali uid)>>
                Dictionary<string, Object> returnedDictonery = (Dictionary<string, Object>) snapshot.Value;
                
                
                foreach (KeyValuePair<string, Object> item in returnedDictonery)
                {
                    
                    Debug.Log("score:"+((Dictionary<string,Object>)item.Value)["score"]+"  uid:"+((Dictionary<string,Object>)item.Value)["uid"]);
                   
                  //Debug.Log("tip je "+((Dictionary<string,Object>)item.Value)["uid"].GetType());
                }

               
                Debug.Log("uspešno smo vrnili podatke ");
                

            } else if (task.IsCanceled)
            {
                //todo če ni interneta se izpiše lokalno iz playerpref
                Debug.Log("klicanje je bilo preklicano");
            }
        } );
    }
    private void WriteNewScore(string userId, int score) {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        // spodnja vrstica ustvaru nov ključ ki ga uporabi za shranjevanje tako v score in user-score
        string key = _databaseReference.Child("scores").Push().Key;
        
        LeaderboardEntry entry = new LeaderboardEntry(userId, score);
        Dictionary<string, Object> entryValues = entry.ToDictionary();
        //ustvari Dictonery childUpdates, da lahko shrani dve stvari na enkrat
        Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
        childUpdates["/scores/" + key] = entryValues;
        // ko shrani v user score, kjer ma vsak posebaj svoje score po user-score doda userid pod katerim ima igralec svoje rezultate
        childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

        _databaseReference.UpdateChildrenAsync(childUpdates);
    }
}


internal class LeaderboardEntry {
    public string uid;
    public int score = 0;

    public LeaderboardEntry() {
    }

    public LeaderboardEntry(string uid, int score) {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, Object> ToDictionary() {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        result["uid"] = uid;
        result["score"] = score;

        return result;
    }
}
