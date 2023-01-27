using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] float playerSpeed = 0.1f; 可於右邊欄位調整的private
    [Header("Player Stats")]
    public int playerHealth;
    public float playerSpeed;   
    public bool isFacingRight = true;
    public bool isDead = false;

    private Vector2 movement;
    private Rigidbody2D playerRigidBody;
    private PlayerWeapon playerWeapon;

    [Header("Link by Myself")]
    [SerializeField] Animator animator;
    [SerializeField] HealthManager healthManager;
        

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = this.GetComponent<Rigidbody2D>();
        playerWeapon = GameObject.Find("PlayerWeapon").GetComponent<PlayerWeapon>();
        //healthManager = GameObject.Find("HealthManager").GetComponent<>()
        //playerImage = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update(){
        /*if(Input.GetKey(KeyCode.P)){
            if(Time.timeScale == 1) Time.timeScale = 0; //不會影響到Update，會繼續執行
            else Time.timeScale = 1;
        }*/
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate(){
        if(!isDead){
            moveCharacter(movement);
            if(playerHealth <= 0){
                Death();
            }
            if(Input.GetKey(KeyCode.Z)){
                playerWeapon.SendMessage("CheckUseWeapon");
            }
        }
        if(isDead){
            playerRigidBody.velocity = Vector2.zero;
        }
        animator.SetBool("isUsingWeapon", GameObject.Find("PlayerWeapon").GetComponent<Collider2D>().enabled); //if isUsingWeapon == true or false
        animator.SetBool("isDead", isDead);
        
    }

    void moveCharacter(Vector2 direction){
        //playerRigidBody.MovePosition((Vector2)transform.position + (direction * playerSpeed * Time.deltaTime));
        playerRigidBody.velocity = direction * playerSpeed;
        if(movement.x > 0 && !isFacingRight){ //facing left
            Flip(0);
        }
        if(movement.x < 0 && isFacingRight){ //facing right
            Flip(180f);
        }
        animator.SetFloat("SpeedX", Mathf.Abs(movement.x));
        animator.SetFloat("SpeedY", movement.y);
    }
    
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "enemy" && !isDead){
            PlayerHurt();
        }
    }

    void PlayerHeal(){
        healthManager.SendMessage("HealthIndicatePlus", 1);
        playerHealth += 1;
    }
    void PlayerHurt(){
        playerHealth -= 1;
        healthManager.SendMessage("HealthIndicateMinus", 1);
    }
    void Death(){
        isDead = true;
    }

    public void Flip(float degree){
        transform.rotation = Quaternion.Euler(0f, degree, 0f);
        isFacingRight = !isFacingRight;
    }
}