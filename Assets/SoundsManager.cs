using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    public AudioSource jumpAudio;
    public AudioSource takeCoinAudio;
    public AudioSource deathAudio;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
