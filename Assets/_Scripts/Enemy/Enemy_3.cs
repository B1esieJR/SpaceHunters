using UnityEngine;

public class Enemy_3 : Enemy
{
    [SerializeField] float _lifeTime = 5;
    private Vector3[] _pointsPosition;
    private float birthTime;

    void Start()
    {
        GeneratePosition();
    }

    private void GeneratePosition()
    {
        _pointsPosition = new Vector3[3];
        _pointsPosition[0] = enemyPos;
        float xMin = -bndCheck.cameraWidth + bndCheck.offsetRadius;
        float xMax = bndCheck.cameraWidth - bndCheck.offsetRadius;
        Vector3 average;
        average = Vector3.zero;
        average.x = Random.Range(xMin, xMax);
        average.y = -bndCheck.cameraHeight * Random.Range(2.75f, 2);
        _pointsPosition[1] = average;
        average = Vector3.zero;
        average.y = enemyPos.y;
        average.x = Random.Range(xMin, xMax);
        _pointsPosition[2] = average;
        birthTime = Time.time;
    }
    public override void Move()
    {
        float u = (Time.time - birthTime) / _lifeTime;
        if (u > 1)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * _pointsPosition[0] + u * _pointsPosition[1];
        p12 = (1 - u) * _pointsPosition[1] + u * _pointsPosition[2];
        enemyPos = (1 - u) * p01 + u * p12;
    }
}
