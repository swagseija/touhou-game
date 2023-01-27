using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWanderFairy : MonoBehaviour
{
    Transform wFairyTransform;
    public Rigidbody2D wfairy;
    [SerializeField] Animator animator;
    int wfairyHealth = 20;
    public float wfairySpeed = 3f;
    public bool isFacingRight = true;
    float wfairyWalkCooldown = 3f;
    
    float timer = 0;
    void Start()
    {   
        wFairyTransform = this.GetComponent<Transform>();
        wfairy = this.GetComponent<Rigidbody2D>();
        if(!Convert.ToBoolean(UnityEngine.Random.Range(0,2))){ //0 left, 1 right;
            wfairySpeed = (-wfairySpeed);
            isFacingRight = !isFacingRight;
        }
    }
    void Update(){
        timer += Time.deltaTime;
    }
    void FixedUpdate(){
        WFairyMove();
        if(wfairyHealth <= 0){
            Death();
        }
    }

    void WFairyMove(){
        if(timer <= wfairyWalkCooldown){
            wfairy.velocity = Vector2.zero;
        }
        else if(timer <= wfairyWalkCooldown * 2){
            wfairy.velocity = Vector2.right * wfairySpeed;
            if(wfairy.velocity.x > 0 && !isFacingRight) Flip(0);
            if(wfairy.velocity.x < 0 && isFacingRight) Flip(180f);
        }
        else if(timer <= wfairyWalkCooldown * 3){
            wfairy.velocity = Vector2.zero;
        }
        else if(timer <= wfairyWalkCooldown * 4){
            wfairy.velocity = Vector2.left * wfairySpeed;
            if(wfairy.velocity.x > 0 && !isFacingRight) Flip(0);
            if(wfairy.velocity.x < 0 && isFacingRight) Flip(180f);
        }
        else timer = 0;
        animator.SetFloat("Speed", Math.Abs(wfairy.velocity.x));
    }
    
    void Hurt(int damage){
        AudioManager.FairyHurtAudio(wFairyTransform);
        wfairyHealth -= damage;
    }

    void Death(){
        AudioManager.FairyDeathAudio(wFairyTransform);
        Destroy(this.gameObject);
    }

    public void Flip(float degree){
        transform.rotation = Quaternion.Euler(0f, degree, 0f);
        isFacingRight = !isFacingRight;
    }
}