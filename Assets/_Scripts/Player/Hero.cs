using UnityEngine;
public class Hero : MonoBehaviour
{
    [SerializeField] private float _shieldLevel = 1;
    [SerializeField] private GameObject _projectilePrefab;
    private float _speed = 30;
    private GameObject lastTriggerEnemy = null;
    private float rollMult = -45;
    private float pitchMult = 30;
    private float gameRestartDelay = 1.5f;
    private float projectileSpeed = 40;
    [SerializeField] private Weapon[] weapons;
    static public Hero _instance;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    public float Shield
    {
        get => _shieldLevel;
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                Destroy(this.gameObject);
                spawnController._instance.DelayedRestart(gameRestartDelay);
            }  
        }
    }
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
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
            fireDelegate();

    }
    private void TempFire()
    {
        GameObject projectGo = Instantiate<GameObject>(_projectilePrefab);
        projectGo.transform.position = transform.position;
        Rigidbody rigidB = projectGo.GetComponent<Rigidbody>();
        Projectile proj = projectGo.GetComponent<Projectile>();
        proj.type = WeaponType.blaster;
        float tSpeed = spawnController.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
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
            print(Shield);
            Destroy(go);
        }
        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered by non-Enemy:" + go.name);
        }
    }
    private void AbsorbPowerUp(GameObject go)
    {
        PowerUp power = go.GetComponent<PowerUp>();
        switch (power._type)
        {
            
        }
        power.AbsorbedBy(this.gameObject);
    }
    private Weapon GetEmptyWeaponSlot()
    {
        for (var i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return weapons[i];
            }

        }
        return null;
    }
    private void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}
