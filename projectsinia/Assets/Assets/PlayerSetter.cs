using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSetter : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        SetPlayerCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCam()
    {
        vcam.Follow = GameObject.Find("Player").transform;
        vcam.LookAt = GameObject.Find("Player/LookAt").transform;
    }
}
