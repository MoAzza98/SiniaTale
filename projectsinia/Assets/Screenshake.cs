using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Screenshake : MonoBehaviour
{
    public CinemachineTransposer vcam;
    private List<float> xShake = new List<float>();
    private List<float> yShake = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GenerateShakePattern();
        }
    }

    void GenerateShakePattern()
    {
        xShake.Add(Random.Range(-0.5f, 0.5f));
        yShake.Add(Random.Range(-0.5f, 0.5f));
        xShake.Add(Random.Range(0.5f, 1.5f));
        yShake.Add(Random.Range(0.5f, 1.5f));

        for (int i = 0; i < 2; i++)
        {
            //vcam.m_TrackedObjectOffset.y = yShake[Random.Range(0, yShake.Count - 1)];
            //vcam.m_TrackedObjectOffset.x = xShake[Random.Range(0, yShake.Count - 1)];


            //vcam.m_TrackedObjectOffset.y = 0;
            //vcam.m_TrackedObjectOffset.x = 0;
        }
    }
}
