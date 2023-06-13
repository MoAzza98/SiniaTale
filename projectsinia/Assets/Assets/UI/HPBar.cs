using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth = 100f;

    [SerializeField] RectTransform topBar;
    [SerializeField] RectTransform bottomBar;


    [SerializeField] float animationSpeed = 10f;

    float fullWidth;
    float targetWidth => currentHealth * fullWidth / maxHealth;

    Coroutine adjustBarWidthCoroutine;

    private void Awake()
    {
        fullWidth = topBar.rect.width;
    }

    public void SetCurrentHealth(float amount) // change in health, - for damage + for heal.
    {
        // Debug.Log("setcurrenthealth called : " + amount);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (adjustBarWidthCoroutine != null)
        {
            StopCoroutine(adjustBarWidthCoroutine);
        }
        adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }

    IEnumerator AdjustBarWidth(float amount)
    {
        var suddenChangeBar = amount >= 0 ? bottomBar : topBar;
        var slowChangeBar = amount >= 0 ? topBar : bottomBar;

        suddenChangeBar.SetWidth(targetWidth);

        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, targetWidth, Time.deltaTime * animationSpeed));
            yield return null;
        }
        slowChangeBar.SetWidth(targetWidth);
    }

    public void SetMaxHealth(float maxHealthValue)
    {
        // Debug.Log("Setting max health to: " + maxHealthValue);
        maxHealth = maxHealthValue;
        currentHealth = maxHealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
