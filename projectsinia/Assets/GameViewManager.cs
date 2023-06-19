using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameViewManager : MonoBehaviour
{
    [SerializeField] private Slider energySlider;

    [HideInInspector] public float energyScore;
    private float score;
    private float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentScore = Mathf.SmoothDamp(energySlider.value, energyScore, ref currentVelocity, 10 * Time.deltaTime);
        energySlider.value = currentScore;
    }

    void changeValue()
    {
        float currentScore = Mathf.SmoothDamp(energySlider.value, score, ref currentVelocity, 100 * Time.deltaTime);
    }
}
