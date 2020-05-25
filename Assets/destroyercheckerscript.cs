using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//Scripta odgovorna za uničenje ploščice ob dotiku meje (border GameObject)
public class destroyercheckerscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);

    }
}
