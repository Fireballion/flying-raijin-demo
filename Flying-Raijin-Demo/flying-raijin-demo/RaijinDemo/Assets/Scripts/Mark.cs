using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;
using System;
using UnityEngine.Rendering.PostProcessing;

public class Mark : MonoBehaviour
{
    private Animator anim;
    private MovementInput input;
    public CinemachineFreeLook cameraFreeLook;
    private CinemachineImpulseSource impulse;
    private PostProcessVolume postVolume;
    private PostProcessProfile postProfile;
    public Transform sword;
    public GameObject attachedSword;
    public Transform swordHand;
    private Vector3 swordOrigRot;
    private Vector3 swordOrigPos;
    private MeshRenderer swordMesh;
    public ParticleSystem swordParticle;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<MovementInput>();
        anim = GetComponent<Animator>();
        impulse = cameraFreeLook.GetComponent<CinemachineImpulseSource>();
        postVolume = Camera.main.GetComponent<PostProcessVolume>();
        postProfile = postVolume.profile;
        swordOrigRot = sword.localEulerAngles;
        swordOrigPos = sword.localPosition;
        swordMesh = sword.GetComponentInChildren<MeshRenderer>();
        swordMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            swordParticle.Play();
            swordMesh.enabled = true;
            anim.SetTrigger("mark");
            
        }
    }
}
