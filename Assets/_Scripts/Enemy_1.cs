using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
   [SerializeField] private float waveFrequence = 2f;
    private float waveWidth = 20;
    private float waveRotY = 45;
    private float startXpos;
    private float birthTime;
    
   private void Start()
    {
        startXpos = enemyPos.x;
        birthTime = Time.time;
    }

    private void Update()
    {
        Move();
    }
    public override sealed void Move()
    {
        Vector3 tempPos = enemyPos;
        float age = Time.time - birthTime;
       
        float theta = Mathf.PI * 2 * age / waveFrequence;
        float sin = Mathf.Sin(theta);
            tempPos.x = startXpos + waveWidth * sin;
            enemyPos = tempPos;
            Vector3 rot = new Vector3(0, sin * waveRotY, 0);
            this.transform.rotation = Quaternion.Euler(rot);
            base.Move();  
    }
}
