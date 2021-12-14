using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
     public Animator animator;
   public Transform FirePoint;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();  
            animator.SetBool("gun",true);  
        }
        else
        {
            animator.SetBool("gun",false);
        }

    }
    void Shoot()
    {
        RaycastHit2D  hitInfo = Physics2D.Raycast(FirePoint.position,FirePoint.right);
        if(hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                Debug.Log("Killlll!!");
            }
        }
    }
}
