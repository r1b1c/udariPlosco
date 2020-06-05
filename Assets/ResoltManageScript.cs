using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class ResoltManageScript : MonoBehaviour
{
    private Player data;
    private DatabaseReference _databaseReference;
    private string dataUrl="https://udariploscodb.firebaseio.com/";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("data saving 1");

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dataUrl);
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        SaveData();
    }

    private void SaveData()
    {
        data=new Player("t.v@g.com","QmiTV4E3LuWUPe2xGf4UMO1kRWF3");
        string jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);
      //  _databaseReference.Child(data.Id).SetValueAsync("0");
        Debug.Log("data saved 2");
    }

    public void LoadData()
    {
        
    }
}
