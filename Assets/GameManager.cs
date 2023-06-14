using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform holeGameObj;
    [SerializeField] private GameObject LookCamera;
    [SerializeField] private GameObject[] BulletsBtn;
    [SerializeField] private Transform firePlace;
    [SerializeField] private Transform Boss;
    [SerializeField] private GameObject[] bullets;
    IEnumerator Start()
    {
        
        PlayerPrefs.SetInt("ShotgunBullet", 100);
        PlayerPrefs.SetInt("RegularBullet", 100);
        
        if (PlayerPrefs.HasKey("size"))
        {
            string GetJson = PlayerPrefs.GetString("size");

            holeGameObj.localScale = JsonUtility.FromJson<HoleManager.HoleSizeClass>(GetJson).size;
        }
        
        yield return new WaitForSecondsRealtime(0.5f);
        LookCamera.SetActive(true);
        
        if (PlayerPrefs.HasKey("ShotgunBullet") && PlayerPrefs.HasKey("RegularBullet"))
        {
            for (int i = 0; i < BulletsBtn.Length; i++)
                BulletsBtn[i].SetActive(true);
            
            BulletsBtn[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                PlayerPrefs.GetInt("RegularBullet").ToString();
            
            BulletsBtn[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                PlayerPrefs.GetInt("ShotgunBullet").ToString();

        }
        else
        {
            if (PlayerPrefs.HasKey("RegularBullet"))
            {
                BulletsBtn[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    PlayerPrefs.GetInt("RegularBullet").ToString();
            
            
                BulletsBtn[0].SetActive(true);
           
            }
        
            if (PlayerPrefs.HasKey("ShotgunBullet"))
            {
                BulletsBtn[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    PlayerPrefs.GetInt("ShotgunBullet").ToString();
            
                BulletsBtn[1].SetActive(true);
            
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (holeGameObj.position.z < 1.5f)
        {
            holeGameObj.position = Vector3.MoveTowards(holeGameObj.position, holeGameObj.position + new Vector3(0f,0f,1f),
                Time.deltaTime * 2f); 
        }
        
    }


    private void FireTheAmmo(GameObject ammoType, float minRandomX, float maxRandomX, float force,
        float minRandomVelocity, float maxRandomVelocity)
    {
        GameObject newBullet = Instantiate(ammoType, new Vector3(
            firePlace.position.x + Random.Range(minRandomX, maxRandomX),
            firePlace.position.y, firePlace.position.z),quaternion.identity);

        Rigidbody ammoRb = newBullet.GetComponent<Rigidbody>();
        
        ammoRb.AddForce(transform.forward * force);
        
        ammoRb.velocity = new Vector3(0f,Random.Range(minRandomVelocity,maxRandomVelocity),0f);

        if (!Boss.GetComponent<Animator>().GetBool("move"))
        {
            Boss.GetComponent<Animator>().SetBool("move",true);
        }
        
    }


    public void Regular_btn()
    {
        if (PlayerPrefs.GetInt("RegularBullets") % 5 == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                FireTheAmmo(bullets[0], -0.35f, 0.35f, 500f, 6f, 8f);
        
                PlayerPrefs.SetInt("RegularBullets",PlayerPrefs.GetInt("RegularBullets") - 1);
        
                BulletsBtn[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    PlayerPrefs.GetInt("RegularBullets").ToString();
            }
        }
        else
        {
            FireTheAmmo(bullets[0], -0.35f, 0.35f, 500f, 6f, 8f);
        
            PlayerPrefs.SetInt("RegularBullets",PlayerPrefs.GetInt("RegularBullets") - 1);
        
            BulletsBtn[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                PlayerPrefs.GetInt("RegularBullets").ToString();
        }
       
        
    }
    
    public void ShotGun_btn()
    {

        if (PlayerPrefs.GetInt("ShotgunBullet") % 5 == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                FireTheAmmo(bullets[1], -0.2f, 0.2f, 450f, 6f, 8f);  
        
                PlayerPrefs.SetInt("ShotgunBullet",PlayerPrefs.GetInt("ShotgunBullet") - 1);
        
                BulletsBtn[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    PlayerPrefs.GetInt("ShotgunBullet").ToString();
            }
        }
        else
        {
            FireTheAmmo(bullets[1], -0.2f, 0.2f, 450f, 6f, 8f);  
        
            PlayerPrefs.SetInt("ShotgunBullet",PlayerPrefs.GetInt("ShotgunBullet") - 1);
        
            BulletsBtn[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                PlayerPrefs.GetInt("ShotgunBullet").ToString();
        }
    
    }
}
