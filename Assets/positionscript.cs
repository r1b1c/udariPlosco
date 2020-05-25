using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
// scripta odgovorna za kreiranje tras kjer bodo ploščice tekle 
public class positionscript : MonoBehaviour
{
  //višina in širina posamezne trase
    public float width = 10f;
    public float height = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }
    //v scene view nam označi posamezno traso
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }


    // Update is called once per frame
    void Update()
    {

    }
}
