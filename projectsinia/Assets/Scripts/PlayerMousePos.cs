using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMousePos : MonoBehaviour
{
    PlayerAimDir playerAimDir;
    Vector2 mousePoint;

    // Start is called before the first frame update
    void Start()
    {
        playerAimDir = GetComponentInChildren<PlayerAimDir>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mousePoint = GetPointerInput();
        playerAimDir.mousePosition = mousePoint;
    }

    public Vector2 GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
