﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHP;
    public float currentHP;

    public Animator animator;

    public Slider health;

    public Image healthFill;

    public Gradient healthbargradient;

    public ParticleSystem enemyHit1;

    public bool completed = false;

    void Start()
    {
        currentHP = enemyHP;
    }

    void Update()
    {
        if(currentHP <= 0)
        {
            if (gameObject.tag == "FlyingEnemy")
            {
                Instantiate(enemyHit1, gameObject.transform.position, Quaternion.identity);
                animator.SetBool("Destroyed", true);
                Destroy(gameObject, 1);
            }
            else if(gameObject.tag == "CrushHurter")
            {
                animator.SetBool("Destroyed", true);
                Destroy(transform.parent.gameObject);
            }
            else if (gameObject.tag == "BlobShield")
            {
                gameObject.SetActive(false);
                animator.SetBool("ShieldDestroyed", true);
                BlobScript.instance.ShieldOn = false;
            }
            else if(gameObject.tag == "FlyingEnemyShield")
            {
                gameObject.SetActive(false);
                animator.SetBool("ShieldDestroyed", true);
                FlyingEnemy.instance.ShieldOn = false;
            }
            else if (gameObject.tag == "CanonStone")
            {
                animator.SetBool("Break", true);
            }
            else if (gameObject.tag == "Canon")
            {
                animator.SetBool("Destroyed", true);
                Destroy(gameObject, 1);
            }
            else if (gameObject.tag == "Boss")
            {
                Instantiate(enemyHit1, gameObject.transform.position, Quaternion.identity);
                animator.SetBool("Dead", true);
                Destroy(gameObject, 1);
                GameManager.instance.Winagme();
            }
            if(completed == false)
            {
                FindObjectOfType<AudioManager>().Play("Blast");
                completed = true;
            }
            else
            {
                completed = true;
            }
        }

        health.value = currentHP;
        healthFill.color = healthbargradient.Evaluate(health.normalizedValue);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    public float getHP()
    {
        return currentHP;
    }

    public void setHP(float HP)
    {
        currentHP = HP;
    }
}
