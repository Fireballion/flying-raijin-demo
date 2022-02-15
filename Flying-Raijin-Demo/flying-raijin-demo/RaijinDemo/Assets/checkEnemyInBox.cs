using UnityEngine;

public class checkEnemyInBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public MovementInput input;
    void Start()
    {
        input = player.GetComponent<MovementInput>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        input.objectInBox = true;
        input.objectInBoxOBJ = collision.collider.gameObject;
    }
    //public void MakeSeal(GameObject flyingRaijinSeal, GameObject objectInBoxOBJ, float markRange)
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, markRange))
    //    {
    //        var newFlyingRaijinSeal = Instantiate(flyingRaijinSeal, hit.point, transform.rotation);
    //        newFlyingRaijinSeal.transform.parent = objectInBoxOBJ.transform;
    //    }
    //    Debug.DrawRay(transform.position, transform.forward * markRange);
    //}
    private void OnCollisionExit(Collision collision)
    {
        input.objectInBox = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
