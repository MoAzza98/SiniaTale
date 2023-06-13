using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    [SerializeField] Vector3 moveSpeed = new Vector3(0, 75, 0);
    [SerializeField] float timeToFade = 1f;
    float timeElapsed;
    RectTransform textTransform;
    Color startColor;


    private TextMeshProUGUI text;
    private Canvas canvas;
    private Camera mainCamera;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        damageText = GetComponent<TextMeshProUGUI>();
        startColor = damageText.color;
    }



    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            damageText.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(transform.root.gameObject);
            // Destroy(transform.parent.transform.parent.gameObject);
        }
    }




}
