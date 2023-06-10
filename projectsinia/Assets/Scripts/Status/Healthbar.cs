using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    Slider healthbarSlider;
    public RectTransform fillArea;
    float currHealth = 100f;
    private void Awake()
    {
        healthbarSlider = GetComponent<Slider>();

        healthbarSlider.minValue = 0f;
        healthbarSlider.maxValue = 100f;
        healthbarSlider.value = 100f;
        Debug.Log("in Awake Healthbar.cs now...");
        Debug.Log("Okay but what is healthbarSlider.value? " + healthbarSlider.value);
        Debug.Log("Okay but what is healthbarSlider.maxValue? " + healthbarSlider.maxValue);
    }

    private void Update()
    {
        // healthbarSlider.value = currHealth;
    }

    public void SetHealthValue(float currentHealth)
    {
        currHealth = currentHealth;
        Debug.Log("made it into sethealthvalue... logging the currenthealth: " + currentHealth);
        Debug.Log("NOW LOGGING HEALTHBARSLIDER.VALUE BEFORE CHANGE: " + healthbarSlider.value);
        healthbarSlider.value = currentHealth;
        healthbarSlider.fillRect.localScale = new Vector3(20f,1f,1f);
        Debug.Log("NOW LOGGING HEALTHBARSLIDER.VALUE AFTER CHANGE: " + healthbarSlider.value);


        // healthbarSlider.value = currentHealth;
        // Debug.Log("Okay but what is healthbarSlider.value? " + healthbarSlider.value);
        // Debug.Log("Okay but what is healthbarSlider.maxValue? " + healthbarSlider.maxValue);

        // float fillPercentage = healthbarSlider.value / healthbarSlider.maxValue;
        // healthbarSlider.fillRect.localScale = new Vector3(fillPercentage, 1f, 1f);
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthbarSlider.maxValue = maxHealth;
    }

    public float GetMaxHealthValue()
    {
        return healthbarSlider.maxValue;
    }

}
