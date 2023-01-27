using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager audioManager;

    [Header("EnemyFairies Sounds")]
    public AudioClip fairyHurtClip;
    public AudioClip fairyDeathClip;

    AudioSource fairySource;

    private void Awake() {
        audioManager = this;
        fairySource = gameObject.AddComponent<AudioSource>();
    }

    public static void FairyHurtAudio(Transform fairyTransform){
        audioManager.fairySource.clip = audioManager.fairyHurtClip;
        audioManager.fairySource.volume = 0.2f;
        AudioSource.PlayClipAtPoint(audioManager.fairySource.clip, fairyTransform.position);
    }
    public static void FairyDeathAudio(Transform fairyTransform){
        audioManager.fairySource.clip = audioManager.fairyDeathClip;
        audioManager.fairySource.volume = 0.1f;
        AudioSource.PlayClipAtPoint(audioManager.fairySource.clip, fairyTransform.position);
    }
}