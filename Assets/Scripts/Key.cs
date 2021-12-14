using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
   
   private void OnCollisionEnter2D(Collision2D collision)
   {
       if(collision.gameObject.GetComponent<movement>() != null)
       {
           movement move = collision.gameObject.GetComponent<movement>();
            move.pickUpKey();
            Destroy(gameObject);
           
       }

   }
}
