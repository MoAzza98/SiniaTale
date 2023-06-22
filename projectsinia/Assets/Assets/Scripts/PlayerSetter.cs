using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSetter : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Awake()
    {
        //Get the virtual cam
        vcam = GetComponent<CinemachineVirtualCamera>();
        //SetPlayerCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set the virtual cams Follow and LookAt targets to the correct targets in scene by using Find()
    public void SetPlayerCam()
    {
        vcam.Follow = GameObject.Find("Player").transform;
        vcam.LookAt = GameObject.Find("Player/LookAt").transform;
    }
}
