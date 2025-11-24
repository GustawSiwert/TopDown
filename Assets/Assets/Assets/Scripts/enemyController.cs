using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour //Måste ha samma namn som scriptet
{
    private Animator anim; //refererar till animator
    public Transform target; //refererar till target som i detta fall är Player
    public Transform homePos; //refererar till Enemy's hemposition, dit vi vill att den gå tillbaka till. 
    public float speed; //refererar till hur snabbt enemy får gå
    public float maxRange; //refererar till enemy's max range, så långt som den får gå
    public float minRange; //referarer till enemy's minimum range, så nära den får gå, så den inte kan knuffa vår player 
    void Start()
    {
        anim = GetComponent<Animator>(); //hämta animator
    }
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer(); //om player är inom max range så följ efter player, när du kommit tillräckligt nära "minRange" sluta följa
        }
        else if(Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHome(); //om player lämnar max range, sluta följa och gå till hempositionen
        }
    }
    public void FollowPlayer()
    {
        anim.SetBool("WithinRange", true); //om enemy följer player starta withinRange animationerna
        anim.SetFloat("MoveHorizontal", (target.position.x - transform.position.x)); //enemy följer efter x axeln
        anim.SetFloat("MoveVertical", (target.position.y - transform.position.y)); //enemy följer efter y axeln
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime); //härma players rörelser
    }
    public void GoHome()
    {
        anim.SetFloat("MoveHorizontal", (homePos.position.x - transform.position.x)); //enemy går hem efter x axeln
        anim.SetFloat("MoveVertical", (homePos.position.y - transform.position.y)); //enemy går hem efter y axeln
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime); //player härmar vilken position hem har
        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            anim.SetBool("WithinRange", false); //om hem positonen är 0 (enemy är hemma) sätt WithinRange till falskt
        }
    }
    //Om vi vill att enemy inte ska putta player
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            speed = 0f;
        }
    }
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            speed = 2f;
        }
    }
}
