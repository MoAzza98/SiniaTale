using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject LoadScreen;
    private float loadCurrent = 0f;
    private float loadTime = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadCurrent += Time.deltaTime;

        if(loadCurrent > loadTime && LoadScreen.activeSelf == true)
        {
            LoadScreen.SetActive(false);
        }
    }

}
