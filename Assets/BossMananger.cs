using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMananger : MonoBehaviour
{
    public Transform BossBloodSlider;
    [SerializeField] private Transform target; // which is the hole 
    public  Animator bossAnimator;
    public  bool Dead;
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {

            if (BossBloodSlider.gameObject.activeSelf)
            {
                BossBloodSlider.position = new Vector3(BossBloodSlider.position.x,BossBloodSlider.position.y,transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,transform.position.y,target.position.z),
                    Time.deltaTime * 1.5f);
            }
       
        }
        else
        {
            BossBloodSlider.gameObject.SetActive(false);
        }
    }
}
