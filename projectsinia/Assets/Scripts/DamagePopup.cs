using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    private TextMeshPro textMesh;
    private float moveYSpeed = 10f;
    private float disappearTimer;
    public Color textColor;
    public Color critColor;

    private const float DISAPPEAR_TIMER_MAX = 1f;
    public float destroyTime;
    private Vector3 moveVector;

    //Create a damage popup
    public static DamagePopup Create(Vector2 position, int damageamount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.damagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageamount, isCriticalHit);

        return damagePopup;
    }

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            textMesh.fontSize = 12f;
            textColor = textColor;
        }
        else
        {
            textMesh.fontSize = 14f;
            textColor = critColor;
        }
        textMesh.color = textColor;
        destroyTime = DISAPPEAR_TIMER_MAX;

        float x = Random.Range(0.5f, 2f);
        int y = Random.Range(-2, 3);

        int n = Random.Range(0, 2);

        if (n == 0)
        {
            x = -Mathf.Abs(x);
        }

        moveVector = new Vector3(x, y) * 3f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float moveYSpeed = 10f;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 3f * Time.deltaTime;

        if (destroyTime > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
