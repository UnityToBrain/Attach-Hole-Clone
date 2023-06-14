using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damage : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("ground") && !other.collider.CompareTag("ammo"))
        {
            if (other.transform.root.gameObject.TryGetComponent(out BossMananger boss))
            {
                if (!boss.BossBloodSlider.gameObject.activeSelf)
                {
                    boss.BossBloodSlider.gameObject.SetActive(true);
                }

                boss.BossBloodSlider.transform.GetChild(0).GetComponent<Slider>().value--;

                if (boss.BossBloodSlider.transform.GetChild(0).GetComponent<Slider>().value.Equals(0))
                {
                    boss.bossAnimator.enabled = false; // makes the ragdoll active

                    boss.Dead = true;
                    
                }
            }
            
            gameObject.SetActive(false);
        }
        
        
        
       
    }
}
