
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    public float velocity = 9;
    [Space]

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;
	public CinemachineFreeLook cameraFreeLook;


	[Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;


    private float verticalVel;
    private Vector3 moveVector;
    public bool canMove;
	public GameObject flyingRaijinKunaiSpawnerObj;
	public FlyingRaijinKunaiSpawner flSpwner;
	public List<GameObject> listFlyingRaijinSeals;
	public List<GameObject> listFlyingRaijinPersonSeals;
	public List<GameObject> listFlyingRaijinNormalSeals;
	public bool objectInBox;
	public GameObject flyingRaijinSeal;
	public GameObject objectInBoxOBJ;
	public GameObject markCheck;
	public float markRange;
	public Transform markTransform;
	public GameObject radialMenu;
	public RadialMenu radialMenuClass;
	//public checkEnemyInBox checkEnemyInBox;



	// Use this for initialization
	void Start () {
		objectInBox = false;

		Debug.Log("start");
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		Cursor.visible = false;
		controller = this.GetComponent<CharacterController> ();
		flSpwner = flyingRaijinKunaiSpawnerObj.GetComponent<FlyingRaijinKunaiSpawner>();
		markTransform = markCheck.GetComponent<Transform>();
		radialMenuClass = radialMenu.GetComponent<RadialMenu>();
		

		//checkEnemyInBox = markCheck.GetComponent<checkEnemyInBox>();
	}
	RaycastHit hit;
	public GameObject markToTeleportTo;
	bool transport;
	public bool barrier;
	// Update is called once per frame
	void Update () {
		
		//radialMenuClass.isOpen = false;
		for (var i = listFlyingRaijinSeals.Count - 1; i > -1; i--)
		{
			if (listFlyingRaijinSeals[i] == null)
				listFlyingRaijinSeals.RemoveAt(i);
		}
		for (var i = listFlyingRaijinPersonSeals.Count - 1; i > -1; i--)
		{
			if (listFlyingRaijinPersonSeals[i] == null)
				listFlyingRaijinPersonSeals.RemoveAt(i);
		}
		for (var i = listFlyingRaijinNormalSeals.Count - 1; i > -1; i--)
		{
			if (listFlyingRaijinNormalSeals[i] == null)
				listFlyingRaijinNormalSeals.RemoveAt(i);
		}
		//for (var i = marksForTransportAndSummon.Count - 1; i > -1; i--)
		//{
		//	if (marksForTransportAndSummon[i] == null)
		//		marksForTransportAndSummon.RemoveAt(i);
		//}
		

		if (!canMove)
            return;
		anim.SetFloat("Blend", Speed);
		InputMagnitude ();
		

		if (Input.GetKeyDown(KeyCode.Alpha2) && listFlyingRaijinSeals.Count < 9)
		{
			//Debug.Log("Mark");
			
			if (Physics.Raycast(markCheck.transform.position, markCheck.transform.forward, out hit, markRange))
			{
				transform.rotation = Quaternion.Euler(transform.rotation.x, Camera.main.transform.localEulerAngles.y, transform.rotation.z);
				//Cursor.lockState = CursorLockMode.Locked;

				anim.SetTrigger("mark");
				cameraFreeLook.enabled = false;
				canMove = false;
			}


		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			if (flSpwner.currentFlyingRaijinKunai)
            {
				TeleportToKunai(flSpwner.currentFlyingRaijinKunai, gameObject, true);
			}
			else
            {
				transform.rotation = Quaternion.Euler(transform.rotation.x, Camera.main.transform.localEulerAngles.y, transform.rotation.z);

				anim.SetTrigger("slash");
				canMove = false;
			}
			
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

			if (radialMenuClass.entries.Count == 0)
            {
				if (listFlyingRaijinSeals.Count != 0)
				{
					Cursor.visible = true;
				}
				transport = true;
				radialMenuClass.SetList(listFlyingRaijinSeals, false);
				radialMenuClass.Open();
				
			} else
            {
				radialMenuClass.Close();
				transport = false;
				
				Cursor.visible = false;
			}
			
			
        }
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{

			if (radialMenuClass.entries.Count == 0)
			{
				if(listFlyingRaijinPersonSeals.Count != 0)
                {
					Cursor.visible = true;
				}
				
				radialMenuClass.SetList(listFlyingRaijinPersonSeals,true);
				radialMenuClass.Open();
			}
			else
			{
				radialMenuClass.Close();         
				Cursor.visible = false;
			}

			
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
            if (listFlyingRaijinPersonSeals.Count != 0)
            {
				TeleportToKunai(listFlyingRaijinPersonSeals[Random.Range(0, listFlyingRaijinPersonSeals.Count)], gameObject, true);
			}
			
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{

			if (radialMenuClass.entries.Count == 0)
			{
				Cursor.visible = true;
				radialMenuClass.SetList(listFlyingRaijinPersonSeals, false);
				radialMenuClass.Open();
				transport = false;				
			}
			else
			{
				radialMenuClass.Close();
				
				Cursor.visible = false;
			}


		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
        {
			barrier = true;
			print("down");
			
			
        }
		if (Input.GetKeyUp(KeyCode.Alpha7))
        {
			barrier = false;
			print("up");
			
        }

		//If you don't need the character grounded then get rid of this part.
		isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= .05f * Time.deltaTime;
        }
        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);

        //Updater
    }
	
    private void OnCollisionEnter(Collision collision)
    {
		//print(barrier);
        if (barrier && collision.collider.tag == "Target")
        {
			
			TeleportToKunai(listFlyingRaijinSeals[Random.Range(0, listFlyingRaijinSeals.Count)].transform.gameObject,collision.collider.gameObject, false);
        }
    }
    public List<GameObject> marksForTransportAndSummon;
	public void SetMark(GameObject setMark, bool shouldCastTwice)
    {
		var mark = setMark;
		marksForTransportAndSummon.Add(mark);
        if (shouldCastTwice)
        {
			radialMenuClass.SetList(listFlyingRaijinSeals, false);
			radialMenuClass.Open();
        } else
        {
			
			Cursor.visible = false;
			if (marksForTransportAndSummon.Count == 1)
            {
				
				if (transport)
				{
					TeleportToKunai(marksForTransportAndSummon[0], gameObject, true);
					transport = false;
				} else
                {
					TeleportToKunai(flyingRaijinKunaiSpawnerObj, marksForTransportAndSummon[0].transform.parent.gameObject, true);
				}
				
				
			} else
            {
				TeleportToKunai(marksForTransportAndSummon[1], marksForTransportAndSummon[0].transform.parent.gameObject, true);
				
			}
			marksForTransportAndSummon.Clear();

		}
		
    }
	public void Mark()
    {
        //checkEnemyInBox.MakeSeal(flyingRaijinSeal, objectInBoxOBJ, markRange);
        Debug.Log("Mark");
		
        //if (Physics.Raycast(transform.position, transform.forward, out hit, markRange))
        //{
        var newFlyingRaijinSeal = Instantiate(flyingRaijinSeal, hit.point, Quaternion.LookRotation(hit.normal));


		
		//newFlyingRaijinSeal.transform.forward = hit.normal;
		print(transform.rotation);
		var vec = transform.eulerAngles;
		vec.x = Mathf.Round(vec.x / 90) * 90;
		vec.y = Mathf.Round(vec.y / 90) * 90;
		vec.z = Mathf.Round(vec.z / 90) * 90;
		//newFlyingRaijinSeal.transform.rotation = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z);
		//newFlyingRaijinSeal.transform.rotation = Quaternion.Euler(newFlyingRaijinSeal.transform.eulerAngles.x, -90, newFlyingRaijinSeal.transform.eulerAngles.z);

		//newFlyingRaijinSeal.transform.rotation = Quaternion.Euler(0,-90,0);
		//newFlyingRaijinSeal.transform.rotation = Quaternion.Euler(0, Mathf.Abs(transform.rotation.y * 180 / Mathf.PI), 0);

		//newFlyingRaijinSeal.transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
		//newFlyingRaijinSeal.transform.localScale = new Vector3(newFlyingRaijinSeal.transform.localScale.x, newFlyingRaijinSeal.transform.localScale.x,newFlyingRaijinSeal.transform.localScale.x);
		if (hit.transform.gameObject.tag == "Target")
        {
			newFlyingRaijinSeal.transform.localScale = new Vector3(1, 1, 0.1f);
			newFlyingRaijinSeal.name = "PlayerSeal";
			newFlyingRaijinSeal.transform.parent = hit.transform;
			newFlyingRaijinSeal.transform.localPosition = new Vector3(0, 1, 0);
			
			listFlyingRaijinSeals.Add(newFlyingRaijinSeal);
			listFlyingRaijinPersonSeals.Add(newFlyingRaijinSeal);
        }
        else
        {
            newFlyingRaijinSeal.name = "NormalSeal";
			
			newFlyingRaijinSeal.transform.localScale = new Vector3(1, 1, 1f);
			
			newFlyingRaijinSeal.transform.parent = hit.transform;
			listFlyingRaijinSeals.Add(newFlyingRaijinSeal);
			listFlyingRaijinNormalSeals.Add(newFlyingRaijinSeal);
		}
        //newFlyingRaijinSeal.transform.position = new Vector3(0, 0, 0);

        //Debug.DrawRay(transform.position, transform.forward * markRange);
        //if (objectInBox)
        //{
        //    var newFlyingRaijinSeal = Instantiate(flyingRaijinSeal, (markCheck.transform.position), objectInBoxOBJ.transform.rotation);
        //    //newFlyingRaijinSeal.transform.position += newFlyingRaijinSeal.transform.TransformDirection(-0.5f*Vector3.forward);

        //    newFlyingRaijinSeal.transform.parent = objectInBoxOBJ.transform;
        //    if (objectInBoxOBJ.tag == "Target")
        //    {
        //        newFlyingRaijinSeal.name = "PlayerSeal";
        //        listFlyingRaijinSeals.Add(newFlyingRaijinSeal);
        //    }
        //    else
        //    {
        //        newFlyingRaijinSeal.name = "NormalSeal";
        //        listFlyingRaijinSeals.Add(newFlyingRaijinSeal);
        //    }
        //}
        cameraFreeLook.enabled = true;
		canMove = true;
    }
	public void Throw()
	{


		flSpwner.Throw();

	}

	void PlayerMoveAndRotation() {
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * velocity);
		}
	}

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    public void RotateTowards(Transform t)
    {
        transform.rotation = Quaternion.LookRotation(t.position - transform.position);

    }

    void InputMagnitude() {
		//swordR.GetComponent<MeshRenderer>().enabled = false;
		//Calculate Input Vectors
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

		//Physically move player
		if (Speed > allowPlayerRotation) {
			//anim.SetFloat ("InputMagnitude", Speed, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (Speed < allowPlayerRotation) {
			//anim.SetFloat ("InputMagnitude", Speed, StopAnimTime, Time.deltaTime);
		}
	}
	public void TeleportToKunai(GameObject g, GameObject objectToMove, bool needStop)
    {
		GetComponent<WarpController>().FlashToMark(g.transform, objectToMove, needStop);
		
    }

}
