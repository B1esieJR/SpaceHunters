using UnityEngine;

public class Enemy_2 : Enemy
{
    [SerializeField] private float _sinEccentricity;
    [SerializeField] private float _lifeTime;
    private Vector3 _startPos;
    private Vector3 _finalPos;
    private float _birthTime;
    void Start()
    {
        GeneratePosition(); 
    }
    private void GeneratePosition()
    {
        _startPos = Vector3.zero;
        _startPos.x = -bndCheck.cameraWidth - bndCheck.offsetRadius;
        _startPos.y = Random.Range(-bndCheck.cameraHeight, bndCheck.cameraHeight);
        _finalPos = Vector3.zero;
        _finalPos.x = bndCheck.cameraWidth + bndCheck.offsetRadius;
        _finalPos.y = Random.Range(-bndCheck.cameraHeight, bndCheck.cameraHeight);
        if (Random.value > 0.5f)
        {
            _startPos *= -1;
            _finalPos *= -1;
        }
        _birthTime = Time.time;
    }
    public override void Move()
    {
        float u = (Time.time - _birthTime) / _lifeTime;
        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        u = u + _sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        enemyPos = (1 - u) * _startPos + u * _finalPos;
    }

}
