using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    private float radius = 4f;
    public float cameraWidth;
    public float cameraHeight;
    public bool keepOnScreen = true;
    public bool isOnScreen = true;
    public bool offRight, offLeft, offUp, offDown;

    public float offsetRadius
    {
        get => radius;
        set => radius = value;
    }
    private void Awake()
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight* Camera.main.aspect;
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;
        if (pos.x > cameraWidth - radius)
        {
            pos.x = cameraWidth - radius;
            isOnScreen = false;
            offRight = true;
            print("rght");
        }
        if (pos.x < -cameraWidth + radius)
        {
            pos.x = -cameraWidth + radius;
            isOnScreen = false;
            offLeft = true;
            print("lft");
        }
        if (pos.y > cameraHeight - radius)
        {
            pos.y = -cameraHeight + radius;
            isOnScreen = false;
            offUp = true;
        }
        if (pos.y < -cameraHeight + radius)
        {
            pos.y = -cameraHeight + radius;
            isOnScreen = false;
            offDown = true;
        }
        isOnScreen = !(offDown || offRight || offLeft || offUp);
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(cameraWidth * 2, cameraHeight * 2, 0.1f);
        Gizmos.DrawCube(Vector3.zero, boundSize);
    }
}
