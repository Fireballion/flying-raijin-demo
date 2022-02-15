using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiScript : MonoBehaviour
{
    //public FlyingRaijinKunaiSpawner flyingRaijinKunaiSpawner;
    Vector3 spawnLocation;
    public MovementInput input;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //flyingRaijinKunaiSpawner = player.GetComponent<FlyingRaijinKunaiSpawner>();
        input = player.GetComponent<MovementInput>();
        GetComponent<Rigidbody>().isKinematic = false;
        spawnLocation = transform.position;


    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.collider.tag);
    //    if (collision.collider.tag != "Player")
    //    {
    //        Debug.Log("nothittingPlayer");
    //        GetComponent<Rigidbody>().isKinematic=true;
           
    //    }
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(spawnLocation, transform.position) > 40)
        {
            //Debug.Log(input.listFlyingRaijinSeals.Count);
            
           // input.listFlyingRaijinSeals.Remove(gameObject);
            
            Destroy(gameObject);
            

        }
    }
}
