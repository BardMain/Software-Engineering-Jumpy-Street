using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectHandler : MonoBehaviour
{

    public GameObject waterSplash;
    public GameObject bloodSplatter;

    AudioSource splashAudio;
    AudioSource splatAudio;

    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) //place in player script?
    {
        if (collision.gameObject.tag.Equals("Water"))
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);
            splashAudio.Play();
        }

        if (collision.gameObject.tag.Equals("Vehicle"))
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
            splatAudio.Play();
        }


    }
}
