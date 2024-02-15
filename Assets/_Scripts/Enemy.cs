using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _speed = 15f;
    protected float fireRate = 0.3f;
    protected float health = 10;
    private float boundsoffset=-2.5f;
    protected int score = 100;
    protected BoundsCheck bndCheck;
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
   private void Update()
    {
        Move();
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }
        else
            print("hh");
    }
    public virtual void Move()
    {
        Vector3 tempPos = enemyPos;
        tempPos.y -= _speed * Time.deltaTime;
        enemyPos = tempPos;
        if (bndCheck != null && !bndCheck.offDown)

            if (enemyPos.y < -bndCheck.cameraHeight + bndCheck.offsetRadius)
            {
                Destroy(gameObject);
            }
    }
}
