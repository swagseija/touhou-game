using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpark : MonoBehaviour
{
    [SerializeField] Animator animator; 
    bool canDealDamage = true;
    private void Start() {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "enemy"){
            Debug.Log("123");
            other.gameObject.SendMessage("Hurt", 1);
        }
    }
    void DoneExpanding(){
        animator.SetBool("DoneExpanding", true);
    }

    IEnumerator DealDamage(Collider2D other){
        other.gameObject.SendMessage("Hurt", 3);
        Debug.Log("123");
        yield return new WaitForSeconds(0.5f);
    }
}
