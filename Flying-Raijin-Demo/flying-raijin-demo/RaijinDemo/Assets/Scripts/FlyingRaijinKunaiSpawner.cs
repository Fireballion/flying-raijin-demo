using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRaijinKunaiSpawner : MonoBehaviour
{
    public GameObject flyingRaijinKunai;
    
    public MovementInput input;
    public GameObject player;
    public float shootForce;
    public GameObject currentFlyingRaijinKunai;

    // Start is called before the first frame update
    void Start()
    {
        input = player.GetComponent<MovementInput>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Throw()
    {
        
        
        {

            currentFlyingRaijinKunai = Instantiate(flyingRaijinKunai, transform.position, Quaternion.Euler(player.transform.localEulerAngles.x - 90, player.transform.localEulerAngles.y, player.transform.localEulerAngles.z));
            
            currentFlyingRaijinKunai.GetComponent<MeshRenderer>().enabled = true;
            currentFlyingRaijinKunai.GetComponent<Rigidbody>().AddForce(player.transform.forward * shootForce, ForceMode.Impulse);
            
        }
        input.canMove = true;
        
    }
}
