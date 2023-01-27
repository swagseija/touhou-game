using UnityEngine;

public class MainCam : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Player player;
    void Awake() {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void FixedUpdate(){
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }    
}