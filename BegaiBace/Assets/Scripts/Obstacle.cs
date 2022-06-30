using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerBehaviour PlayerBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        PlayerBehaviour = GameObject.FindObjectOfType<PlayerBehaviour>();
       // Debug.Log("Hello");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  //  public void OnCollisionEnter(Collision collision)
   // {
    //    if(collision.collider.gameObject.name == "Player")
     //   {
     //       Debug.Log("Hello");
     //   }
  //  }
}
