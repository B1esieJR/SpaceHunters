using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _speed = 10f;
    protected float fireRate = 0.3f;
    protected float health = 10;
    private float boundsoffset=-2.5f;
    protected int score = 100;
    private BoundsCheck bndCheck;
    public Vector3 enemyPos
    {
        get=>this.transform.position;
        set => this.transform.position = value;
    }
    private void Awake()
    {
        boundsoffset = GetComponent<BoundsCheck>().offsetRadius=-2.5f;
        bndCheck = GetComponent<BoundsCheck>();
      

       
    }
    void Update()
    {
        Move();
        if (bndCheck != null && !bndCheck.offDown)
           
            if (enemyPos.y < -bndCheck.cameraHeight + bndCheck.offsetRadius)
            {
                Destroy(gameObject);
            }
    }
    public virtual void Move()
    {
        Vector3 tempPos = enemyPos;
        tempPos.y -= _speed * Time.deltaTime;
        enemyPos = tempPos;
    }
}
