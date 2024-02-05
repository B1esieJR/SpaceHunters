using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero _instance;
    private float _speed = 30;
    private float rollMult = -45;
    private float pitchMult = 30;
   [SerializeField] private float _shieldLevel = 1;
    private float gameRestartDelay = 1.5f;
    [SerializeField] private GameObject _projectilePrefab;
    private float projectileSpeed = 40;
    public float Shield
    {
        get => _shieldLevel;
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
                Destroy(this.gameObject);
            spawnController._instance.DelayedRestart(gameRestartDelay);
        }
    }
    private GameObject lastTriggerEnemy = null;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Hero.Awake()- Attempted to assign second Hero _instance!");
        }
    }
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        pos.x += xAxis * _speed * Time.deltaTime;
        pos.y += yAxis * _speed * Time.deltaTime;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
        if (Input.GetKeyDown(KeyCode.Space))
            TempFire();

    }
    private void TempFire()
    {
        GameObject projectGo = Instantiate<GameObject>(_projectilePrefab);
        projectGo.transform.position = transform.position;
        Rigidbody rigidB = projectGo.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("triggered:" + go.name);
        if(go==lastTriggerEnemy)
         return;
        lastTriggerEnemy = go;
        if (go.tag == "Enemy")
        {
            Shield--;
            Destroy(go);
        }
        else
            print("it`s not Enemy");
    }
}
