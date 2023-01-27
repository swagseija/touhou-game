using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] float weaponHoldTime;
    [SerializeField] float weaponCooldown;
    public bool hasUsedWeapon = false;

    public Collider2D weaponCollider;
    public Renderer weaponRender;
    
    
    void Start()
    {
        weaponCollider = this.GetComponent<Collider2D>();
        weaponCollider.enabled = false;
        weaponRender = this.GetComponent<Renderer>();
        weaponRender.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "enemy"){
            other.SendMessage("Hurt", 10);
        }
    }

    void CheckUseWeapon(){
        if(!hasUsedWeapon){
            hasUsedWeapon = true;
            StartCoroutine(UseWeapon(weaponHoldTime));
            StartCoroutine(WeaponCooldown(weaponCooldown));
        }
    }

    IEnumerator UseWeapon(float usesecond){
        weaponCollider.enabled = true;
        weaponRender.enabled = true;
        yield return new WaitForSeconds(usesecond);
        weaponCollider.enabled = false;
        weaponRender.enabled = false;
    }
    IEnumerator WeaponCooldown(float cooldown){
        hasUsedWeapon = true;
        yield return new WaitForSeconds(cooldown);
        hasUsedWeapon = false;
    }
}