using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-90, 0, 0);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 5, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(-90, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //  GroundTile.totalBottles += 1;
            //Debug.Log(GroundTile.totalBottles);
            GameManager.bottleScore += 1;
            Destroy(gameObject);
        }
    }
}
