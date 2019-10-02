﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Penek : MonoBehaviour
{
    public float Force;
    private Animator Anim;
    private Rigidbody2D Rigi;
    private GameObject Player;
    private Vector2 Vector;
    private Collider2D Col;
    private bool Active = true;
    TilemapCollider2D TC1;
    TilemapCollider2D TC2;
    TilemapCollider2D TC3;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Rigi = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Col = GetComponent<BoxCollider2D>();
        Anim.SetBool("Attack", false);
        TC1 = GameObject.Find("solidmiddle").GetComponent<TilemapCollider2D>();
        TC2 = GameObject.Find("solidbottom").GetComponent<TilemapCollider2D>();
        TC3 = GameObject.Find("solidbottom2").GetComponent<TilemapCollider2D>();
    }

    void Update()
    {
        if (Col.IsTouching(TC1) || Col.IsTouching(TC2) || Col.IsTouching(TC3))
        {
            Rigi.drag = 100;
            Col.isTrigger = false;
        }
        if(Vector2.Distance(transform.position, Player.transform.position) < 2f && Active)
        {
            Active = false;
            Anim.SetBool("Attack", true);
            StartCoroutine(Attack());
        }

        if (Vector2.Distance(Player.transform.position, transform.position) < 0.9f && Character1._Hit && Anim.GetBool("Attack") == false)
        {
            if (Player.transform.position.x > transform.position.x && Character1.AttackDirection == 1 ||
                Player.transform.position.x > transform.position.x && Character1.AttackDirection == 5 && Player.transform.position.y < transform.position.y ||
                Player.transform.position.x > transform.position.x && Character1.AttackDirection == 8 && Player.transform.position.y > transform.position.y ||

                Player.transform.position.y < transform.position.y && Character1.AttackDirection == 2 ||
                Player.transform.position.y < transform.position.y && Character1.AttackDirection == 7 && Player.transform.position.x < transform.position.x ||
                Player.transform.position.y > transform.position.y && Character1.AttackDirection == 8 && Player.transform.position.x > transform.position.x ||

                Player.transform.position.x < transform.position.x && Character1.AttackDirection == 3 ||
                Player.transform.position.x < transform.position.x && Character1.AttackDirection == 6 && Player.transform.position.y < transform.position.y ||
                Player.transform.position.x < transform.position.x && Character1.AttackDirection == 7 && Player.transform.position.y > transform.position.y ||

                Player.transform.position.y > transform.position.y && Character1.AttackDirection == 4 ||
                Player.transform.position.y < transform.position.y && Character1.AttackDirection == 5 && Player.transform.position.x > transform.position.x ||
                Player.transform.position.y < transform.position.y && Character1.AttackDirection == 6 && Player.transform.position.x < transform.position.x
                )
            {
                StartCoroutine(Die());
            }
        }

    }

    IEnumerator Attack()
    {
        Character1.Alert();
        Anim.speed = 0;
        yield return new WaitForSeconds(0.5f);
        Col.isTrigger = true;
        Anim.speed = 1;
        Rigi.AddForce((Player.transform.position - transform.position) * Force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        Rigi.drag = 20;
        Anim.SetBool("Attack", false);
        yield return new WaitForSeconds(3f);
        Col.isTrigger = false;
        Rigi.drag = 0;
        Active = true;
        Character1.NoAlert();
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("Lifepoint") && Anim.GetBool("Attack"))
        {
            Character1.MinusHp();
        }
    }

    IEnumerator Die()
    {
        GetComponent<Animator>().SetBool("Break", true);
        Rigi.simulated = false;
        yield return new WaitForSeconds(2f);
        Character1.NoAlert();
        Destroy(gameObject);
    }
}
