using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    private bool markSet;
    private Vector3 playerOrigin;
    private Vector3 playerCurrentPos;
    [SerializeField] private GameObject marker;
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentPos = transform.position;
        HandleWarp();
    }

    void HandleWarp()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!markSet)
            {
                MarkPosition();
                marker.transform.position = transform.position;
                obj = Instantiate(marker, new Vector3(transform.position.x, transform.position.y + 0.5f), transform.rotation);
            }
            else if (markSet)
            {
                ReturnToOrigin();
                Destroy(obj);
            }
        }
    }

    void MarkPosition()
    {
        playerOrigin = transform.position;
        markSet = true;
    }

    void ReturnToOrigin()
    {
        transform.position = playerOrigin;
        markSet = false;
    }
}
