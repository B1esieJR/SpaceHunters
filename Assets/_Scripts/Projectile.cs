using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float boundsoffset;
    private BoundsCheck bndCheck;
    private void Awake()
    {
        boundsoffset = GetComponent<BoundsCheck>().offsetRadius = -1;
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update()
    {
        if (bndCheck.offUp)
            Destroy(gameObject);
    }
}
