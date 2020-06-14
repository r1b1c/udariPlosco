using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using TMPro;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Object = System.Object;


public class ResoltManageScript : MonoBehaviour
{
    private Player data;
    private int scorepoints = score.scorepoints;
    private DatabaseReference _databaseReference;
    private string dataUrl="https://udariploscodb.firebaseio.com/";
    private List<LeaderboardEntry> listTop5=null;
    
    // Start is called before the first frame update
     void Start()
    {
        Debug.Log("data saving 1");
        Debug.Log("data saving lool");

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dataUrl);
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        
        //fake objekt todo static objekt Player ki se nastavi ob prijavi, odstrani ob odjavi (mogoče v playerpref bool IsSigned=true/false) 
        data=new Player("t.v@g.com","QmiTV4E3LuWUPe2xGf4UMO1kRWF3","cevap9");
        
        //LoadData();
       WriteNewScore("HnriL2JelbhcHXQObqDlHnJhPt62",scorepoints, "cevap9");
     //  WriteNewScore("0IR96IFxotQthr70IZJKgiuzUG32",15,"jozek1996");

      RetriveLeaderBoard();
        
      // Debug.Log("kaj zaj prej");
      
      
        
    }
    
    public void LoadData()
    {
        FirebaseDatabase.DefaultInstance.GetReference(data.id)
            .GetValueAsync().ContinueWithOnMainThread(task =>
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
    private async  void RetriveLeaderBoard()
    {
        //vrnenajvečje 4ri rezultate ki pa niso vrnjeni sortirano!
       await FirebaseDatabase.DefaultInstance.GetReference("scores").OrderByChild("score").LimitToLast(5).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error se je pojavil");
                //todo če ni interneta se izpiše lokalni rezultati iz playerpref
            } else if (task.IsCanceled)
            {
                //todo če ni interneta se izpiše lokalno iz playerpref
                Debug.Log("klicanje je bilo preklicano");
            }else if (task.IsCompleted)
            {
               
                DataSnapshot snapshot = task.Result;
                
                // v returnedDictonery imamo Vrnjen Dictonery<key(identifyer od vnosa),<Dictonery<string(ime "uid"ali"score"),Object(rezultat uid ali uid)>>
                Dictionary<string, Object> returnedDictonery = (Dictionary<string, Object>) snapshot.Value;
                List<LeaderboardEntry> listOfLeaderboard=new List<LeaderboardEntry>();
                long returnedScore;
                Object returnedObjectScore;
                string returnedId;
                Object returnedIdObject;
                string returnedUsername;
                Object returnedUsernameObject;
                foreach (KeyValuePair<string, Object> item in returnedDictonery)
                {
                    returnedObjectScore =((Dictionary<string, Object>) item.Value)["score"];
                    returnedIdObject=((Dictionary<string,Object>)item.Value)["uid"];
                    returnedUsernameObject = ((Dictionary<string, Object>) item.Value)["username"];
                    returnedId = (string) returnedIdObject;
                    returnedScore =(long)returnedObjectScore;
                    returnedUsername = (string) returnedUsernameObject;
                    listOfLeaderboard.Add(new LeaderboardEntry(returnedId,(int)returnedScore,returnedUsername));
                }
                IntListQuickSort(listOfLeaderboard);
                listTop5 = listOfLeaderboard;
            } 
        } );
       string name;
       string score;
       for (int i = 1; i < 6; i++)
       {
           name = "Name" + i;
           score = "Score" + i;
           Debug.Log(score);
           TextMeshProUGUI tScore=transform.Find(score).GetComponent<TextMeshProUGUI>();
           TextMeshProUGUI tName=transform.Find(name).GetComponent<TextMeshProUGUI>();
                
           if (tName!=null && tScore!=null)
           {
                tName.text = listTop5[i-1].username; // namesto scora izpiši PLayerusername
               tScore.text = listTop5[i-1].score.ToString();
           }
       }
    }
    private void WriteNewScore(string userId, int score,string username) {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        // spodnja vrstica ustvaru nov ključ ki ga uporabi za shranjevanje tako v score in user-score
        string key = _databaseReference.Child("scores").Push().Key;
        
        LeaderboardEntry entry = new LeaderboardEntry(userId, score,username);
        Dictionary<string, Object> entryValues = entry.ToDictionary();
        //ustvari Dictonery childUpdates, da lahko shrani dve stvari na enkrat
        Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
        childUpdates["/scores/" + key] = entryValues;
        // ko shrani v user score, kjer ma vsak posebaj svoje score po user-score doda userid pod katerim ima igralec svoje rezultate
        childUpdates["/user-scores/" + userId + "/" + key] = entryValues;
        

        _databaseReference.UpdateChildrenAsync(childUpdates);
    }

    private static void IntListQuickSort (List<LeaderboardEntry> data, int l, int r)
    {
        int i, j;
        int x;
 
        i = l;
        j = r;
        
        x = data [(l + r) / 2].score; /* find pivot item */
        
        while (true) {
            while (data[i].score > x)
                i++;
            while (x > data[j].score)
                j--;
            if (i <= j) {
                exchange (data, i, j);
                i++;
                j--;
            }
            if (i > j)
                break;
        }
        if (l < j)
            IntListQuickSort (data, l, j);
        if (i < r)
            IntListQuickSort (data, i, r);
    }

    private static void IntListQuickSort (List<LeaderboardEntry> data)
    {
        IntListQuickSort (data, 0, data.Count - 1);
    }

    private static void exchange (List<LeaderboardEntry> data, int m, int n)
    {
        LeaderboardEntry temporary;

        temporary = data[m];
        data [m] = data [n];
        data [n] = temporary;
    }
}


public class LeaderboardEntry {
    public string uid;
    public string username;
    public int score = 0;
    public LeaderboardEntry() {
    }
    public LeaderboardEntry(string uid, int score,string username) {
        this.uid = uid;
        this.username = username;
        this.score = score;
    }
    public Dictionary<string, Object> ToDictionary() {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        result["uid"] = uid;
        result["score"] = score;
        result["username"] = username;

        return result;
    }
}
