using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// Scripta odgovorna za kreiranje spawnerja z njegovim obnašanjem,
// dodajanje ploščice v posamezno traso
public class spawneraction : MonoBehaviour
{
    public float width = 10f;
    public float height = 5f;
    public GameObject pianotile;
    public float delay = 0.5f;
    public float min = -5f;
    public float max = 10f;

    // Start is called before the first frame update
    void Start()
    {
        spawnuntill();
    }
    void spawnuntill()
    {
        Transform position = freeposition();
        float rand = UnityEngine.Random.Range(min, max);
        Vector3 offset = new Vector3(0, rand, 0);
        // čefreepostion vrne traso kreiramo novo kopijo ploščice
        if (position)
        {
            GameObject piano = Instantiate(pianotile, position.transform.position + offset, Quaternion.identity);
            piano.transform.parent = position;
        }
        //če nam freeposition() vrne not null kličemo funkcijo čez nek čas(delay)
        if (freeposition())
        {
            Invoke("spawnuntill", delay);
        }

    }
// kreiramo gizmos spawnerja, znotraj katerega se nahajajo posamezne trase
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }


    // Update is called once per frame

    void Update()
    {
      //ko so vse trase brez ploščic kreiramo novo
        if (checkforempty())
        {
            spawnuntill();
        }
    }

    void spawner()
    {
        //Gremo skozi vse otroke spawnerja (trase)
        foreach(Transform child in transform)
        {
          // kopiramo instanco pianotile (ploščica) in ji da isti položaj kot je njena trasa brez sprememb v rotaciiji
            GameObject piano = Instantiate(pianotile, child.position, Quaternion.identity);
          // hierarhično vstavimo kot starša traso ki smo jo ustvarili v position script
            piano.transform.parent = child;
        }
    }
// če so vse trase prazne (brez ploščice) vrne true
    bool checkforempty()
    {
        foreach (Transform child in transform)
        {
           if(child.childCount > 0)
            {
                return false;
            }
        }
        return true;

    }
// vrne prvo traso ki je prazna ali pa null
    Transform freeposition()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                return child;
            }
        }
        return null;
    }
}
