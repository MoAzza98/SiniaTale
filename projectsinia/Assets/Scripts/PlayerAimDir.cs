using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimDir : MonoBehaviour
{
    Vector2 mouse;
    public Vector2 mousePosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.right = (mousePosition - (Vector2)transform.position).normalized;
        transform.localScale = transform.parent.localScale;
    }
}
