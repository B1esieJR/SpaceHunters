using UnityEngine;

public enum WeaponType
{
    none,
    blaster,
    spread,
    shield
}
[System.Serializable]
public class WeaponDefinition 
{
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float continuonsDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;
    public float damageOnHit = 1;
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;
   [SerializeField] private WeaponType _type = WeaponType.none;
    private WeaponDefinition _definition;
    [SerializeField] private GameObject _collar;
    private float _lastShotTime;
    private Renderer _collarRend;
    public WeaponType type
    {
        get { return _type; }
        set { SetType(value); }
    }
    private void Start()
    {
        _collar = transform.Find("Collar").gameObject;
        _collarRend = _collar.GetComponent<Renderer>();
        SetType(_type);
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
            GameObject rootGO = transform.root.gameObject;
            if (rootGO.GetComponent<Hero>() != null)
                rootGO.GetComponent<Hero>().fireDelegate += Fire;
        }

    }
    public void SetType(WeaponType weaponType)
    {
        _type = weaponType;
        if (_type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        _definition = spawnController.GetWeaponDefinition(_type);
        _collarRend.material.color = _definition.color;
        _lastShotTime = 0;
    }
    public void Fire()
    {

        if (!gameObject.activeInHierarchy)
            return;
        if (Time.time - _lastShotTime < _definition.delayBetweenShots)
            return;
        Projectile projectile;
        Vector3 velocity = Vector3.up * _definition.velocity;
        if (transform.up.y < 0)
            velocity.y = -velocity.y;
        switch (_type)
        {
            case WeaponType.blaster:
                print("b");
                projectile = MakeProjectile();
                projectile._rigid.velocity = velocity;
                break;
            case WeaponType.spread:
                print("spread");
                projectile = MakeProjectile();
                projectile._rigid.velocity = velocity;
                projectile = MakeProjectile();
                projectile.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                projectile._rigid.velocity = projectile.transform.rotation * velocity;
                projectile = MakeProjectile();
                projectile.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                projectile._rigid.velocity = projectile.transform.rotation * velocity;
                break;
        }
        
    }
    private Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(_definition.projectilePrefab);
        if (transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else 
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = _collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.type = type;
        _lastShotTime = Time.time;
        return projectile;
    }
}
