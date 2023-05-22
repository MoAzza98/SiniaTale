using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretch : MonoBehaviour
{
    public SpriteRenderer sr;
    public Vector3 startScale;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LandSquash()
    {
    }

    public void JumpStretch()
    {

    }
}
