﻿using UnityEngine;
using System.Collections;

public class moveScript : MonoBehaviour
{
    //public
    public bool Isattack = false;
    public bool StopAttack = false;
    public float Speed = 0.08f;
    //private 
    float horizontalSpeed,verticalSpeed; // Скорость движения
    Rigidbody2D Rigi;
    //static
    public static bool moveyes; // Если ходим
    public static Animator hero; 
    public static bool attack;
    

    //Джойстик
    public FloatingJoystick JStick;
    public static bool attackButt = false;
    public static bool activate;
    // Старт!
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; //Фиксируем курсор
        //Cursor.visible = false; //Делаем его невидимым
        Rigi = gameObject.GetComponent<Rigidbody2D>();
        hero = GetComponent<Animator>();
        moveyes = true;
        attack = false;
        hero.SetInteger("Vector", 1);
    }
    void FixedUpdate()
    { 
        if (Input.GetKey(KeyCode.Space))
        {
            AttackButt();
        }

        if (hero.GetBool("Died"))
        {
            hero.speed = 1;
            return;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || JStick.Vertical != 0) && moveyes == true && JStick.Horizontal == 0)//Вертикальное передвижение
        {
            if (Input.GetKey(KeyCode.W) || JStick.Vertical == 1)
            {
                hero.speed = 1;
                hero.SetInteger("Vector", 2);
            }
            if (Input.GetKey(KeyCode.S) || JStick.Vertical == -1)
            {
                hero.speed = 1;
                hero.SetInteger("Vector", 4);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                hero.speed = 0;
            }
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || JStick.Horizontal != 0) && moveyes == true && JStick.Vertical == 0)//Горизонтальное передвижение
        {
            if (Input.GetKey(KeyCode.D) || JStick.Horizontal == 1) // Проверяем условие нажатия кнопки D
            {
                hero.speed = 1;
                hero.SetInteger("Vector", 3);
            }
            if (Input.GetKey(KeyCode.A) || JStick.Horizontal == -1) // Проверяем условие нажатия кнопки A
            {
                hero.speed = 1;
                hero.SetInteger("Vector", 1);
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //Костыль от Кости
            {
                hero.speed = 0;
            }
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            || JStick.Horizontal != 0 && JStick.Vertical != 0 && moveyes == true)
        {
            horizontalSpeed = Mathf.Sqrt(Mathf.Pow(Speed / 2, 2) * 2); //Скорость вертикальной ходьбы
            verticalSpeed = Mathf.Sqrt(Mathf.Pow(Speed / 2, 2) * 2);
            hero.speed = 1;
            if (JStick.Horizontal < 0 && JStick.Vertical > 0)
            {
                hero.SetInteger("Vector", 5);
            }
            else
            if (JStick.Horizontal > 0 && JStick.Vertical > 0)
            {
                hero.SetInteger("Vector", 6);
            }
            else
            if (JStick.Horizontal > 0 && JStick.Vertical < 0)
            {
                hero.SetInteger("Vector", 7);
            }
            else 
            if(JStick.Horizontal < 0 && JStick.Vertical < 0)
            {
                hero.SetInteger("Vector", 8);
            }
        }

        else
        {
            horizontalSpeed = Speed; //Скорость движения
            verticalSpeed = Speed;
        }
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || JStick.Horizontal != 0 || JStick.Vertical != 0) && moveyes == true &&
             !Input.GetKey(KeyCode.Space) && !attackButt)
        {
            hero.speed = 0; //Остановить анимацию если не идём
        }

        if (StopAttack == true)
        {
            hero.speed = 1;
            hero.SetBool("Hit", false);
            attackButt = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || attackButt)
        {
            attackButt = true;
            attack = true;
            hero.SetBool("Hit", true);
            hero.speed = 1;
            if (StopAttack)
            {
                hero.SetBool("Hit", false);
                attackButt = false;
            }
            return;
        }
        if (moveyes)
        {
        Rigi.velocity = new Vector2(JStick.Horizontal, JStick.Vertical).normalized * Speed;
        }
        else
        {
        Rigi.velocity = Vector2.zero;
        }

    }
    
    public static void enable(bool x)
    {
        hero.enabled = x;
    }
    // Для джойстика
    public void ActiveButt()
    {
        StartCoroutine(StopButt());
    }
    IEnumerator StopButt()
    {
        activate = true;
        transform.Translate(0,0.01f,0);//Лютый костыль просто жесть
        transform.Translate(0, -0.01f, 0);
        yield return new WaitForSeconds(0.5f);
        activate = false;
    }
    public void AttackButt()
    {
        attackButt = true; 
    }
}