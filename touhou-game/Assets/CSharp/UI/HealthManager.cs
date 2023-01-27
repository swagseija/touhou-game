using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] UnityEngine.UI.Image[] image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HealthIndicatePlus(int byHowMuch){
        for(int i = 0; i < byHowMuch; i++){
            image[player.playerHealth + i].enabled = true;
        }
    }
    void HealthIndicateMinus(int byHowMuch){
        for(int i = 0; i < byHowMuch; i++){
            if(player.playerHealth + i >= 0){
                image[player.playerHealth + i].enabled = false;
            }
        }
    }
    
}
