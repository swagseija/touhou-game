using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseFairy : MonoBehaviour
{
    int cfairyHealth = 30;
    Transform cFairyTransform;
    public Rigidbody2D cfairy;
    [SerializeField] Animator animator;
    [SerializeField] Transform player;
    public float cfairySpeed = 3f;
    Vector2 movedirection;
    bool isFacingRight = true;
    private float distance;
    
    void Start()
    {
        cFairyTransform = this.GetComponent<Transform>();
        cfairy = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        movedirection = direction;
    }
    void FixedUpdate(){
        moveChaseFairy();
        if(cfairyHealth <= 0){
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "player"){
            cfairySpeed = 1f;
        }    
    }

    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "player"){
            cfairySpeed = 3f;
        }    
    }

    void moveChaseFairy(){
        cfairy.velocity = new Vector2(movedirection.x, movedirection.y) * cfairySpeed;
        if(cfairy.velocity.x > 0 && !isFacingRight) Flip(0);
        if(cfairy.velocity.x < 0 && isFacingRight) Flip(180f);
        animator.SetFloat("Speed", Mathf.Abs(cfairy.velocity.x));
    }
    void Hurt(int damage){
        AudioManager.FairyHurtAudio(cFairyTransform);
        cfairyHealth -= damage;
    }

    void Death(){
        AudioManager.FairyDeathAudio(cFairyTransform);
        Destroy(this.gameObject);
    }

    public void Flip(float degree){
        transform.rotation = Quaternion.Euler(0f, degree, 0f);
        isFacingRight = !isFacingRight;
    }
}