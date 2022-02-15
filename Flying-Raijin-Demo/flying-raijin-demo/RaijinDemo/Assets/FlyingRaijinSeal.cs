using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRaijinSeal : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    void Start()
    {
        timer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "PlayerSeal")
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {

                //Debug.Log(input.listFlyingRaijinSeals.Count);

                // input.listFlyingRaijinSeals.Remove(gameObject);

                Destroy(gameObject);

            }
        }
    }
}
