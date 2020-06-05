using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class tileaction : MonoBehaviour
{
    public SpriteRenderer color;
    public int scorevalue = 1;
    // Start is called before the first frame update

    public Rigidbody2D rb;
    public float speed = 500f;
    public int score = 0;
    public AudioClip touchsound;
    private int i = 1;
    private bool isclicked;
    void Start()
    {
        isclicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //določanje hitrosti plošč(neodvisno od frameov)
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speed * Time.deltaTime, 0);
        if (FindObjectOfType<score>().scoree > i * 10)
        {
            speed += 200f;
            i++;
        }
    }
// ko kliknemo spremeni barvo, doda točko, sound
    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (isclicked == false)
            {
                UnityEngine.Debug.Log(speed);
                AudioSource.PlayClipAtPoint(touchsound, transform.position);
                color.color = Color.yellow;
                FindObjectOfType<score>().Scoreupdate(scorevalue);
                isclicked = true;

            }
        }

    }

    //preverjamo če smo kliknili na plošče (če so obarvane rumeno)
    void OnCollisionEnter2D(Collision2D col)
    {
        if (color.color == Color.yellow)
        {
            UnityEngine.Debug.Log("ok");

        }
        // ob dotiku borderja odpri rezultat
        else if(col.collider.tag == "border")
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }
}
