﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    public bool Damaged = false;
    public SpriteRenderer MonsterPic;

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("PlayerBullet"))
        {
            Damaged = true;
            StopAllCoroutines();
            MonsterPic.color = new Color(1, 1, 1, 1f);
            StartCoroutine(DamagedWait());
            Destroy(Col.gameObject);
        }
        if (Col.CompareTag("PlayerBullet2"))
        {
            Damaged = true;
            MonsterPic.color = new Color(1, 1, 1, 1f);
            StopAllCoroutines();
            StartCoroutine(DamagedWait());
            Destroy(Col.gameObject);
        }
    }

    IEnumerator DamagedWait()
    {
        MonsterPic.color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.6f);
        MonsterPic.color = new Color(1, 1, 1, 1f);
    }

    private void OnDisable()
    {
        MonsterPic.color = new Color(1, 1, 1, 1f);
        Damaged = false;
    }
}
