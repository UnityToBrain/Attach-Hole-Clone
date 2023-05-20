using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleManager : MonoBehaviour
{
    private float circleCapacity;
    [SerializeField] private Image circleImg;
    [SerializeField] private Transform holeGameObj;
    void Start()
    {
        
    }


    private void ProgressBarCircle( int number)
    {
        circleCapacity = 1f / number;

        circleImg.fillAmount += circleCapacity;

        if (circleImg.fillAmount.Equals(1f))
        {
            holeGameObj.localScale += new Vector3(0.3f,0f,0.3f);

            circleImg.fillAmount = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammo"))
        {
            ProgressBarCircle(20);
            
            other.gameObject.SetActive(false);
        }
    }
}
