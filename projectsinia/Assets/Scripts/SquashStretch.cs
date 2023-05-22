using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretch : MonoBehaviour
{
    public float currentLerp { get; private set; }
    public float targetLerp { get; set; }
    public SpriteRenderer sr;
    public float speed;
    public AnimationCurve curve;
    public Vector2 jumpStretch, landSquash;
    // Start is called before the first frame update
    void Start()
    {
        targetLerp = 1;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SquashStretchEffect(jumpStretch);
    }

    public void SquashStretchEffect(Vector2 target)
    {
        currentLerp = Mathf.MoveTowards(currentLerp, targetLerp, speed * Time.deltaTime);
        transform.localScale = Vector2.Lerp(Vector2.one, target, curve.Evaluate(Mathf.PingPong(currentLerp, 0.5f)));
    }

    public void SetTargetLerp(float a)
    {
        if(!(a == 0) || !(a == 1)) { return; }
        targetLerp = a;
    }

    public float GetTargetLerp()
    {
        return targetLerp;
    }

}
