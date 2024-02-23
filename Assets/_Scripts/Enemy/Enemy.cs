using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _speed = 15f;
    protected float fireRate = 0.3f;
    protected float health = 10;
    private float boundsoffset = -2.5f;
    protected int score = 100;
    protected BoundsCheck bndCheck;
    protected float showDamageDuration = 0.1f;
    protected Color[] originalColors;
    protected Material[] materials;
    protected bool showingDamage = false;
    protected float damageDoneTime;
    protected bool notifiedofDestruction = false;

    public Vector3 enemyPos
    {
        get => this.transform.position;
        set => this.transform.position = value;
    }
    private void Awake()
    {
        boundsoffset = GetComponent<BoundsCheck>().offsetRadius = -2.5f;
        bndCheck = GetComponent<BoundsCheck>();
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (var i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }
    private void Update()
    {
        Move();
        print(damageDoneTime);
        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile projectile = otherGO.GetComponent<Projectile>();
                ShowDamage();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }
                health -= spawnController.GetWeaponDefinition(projectile.type).damageOnHit;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                Destroy(otherGO);
                break;
            default:
                print("Enemy hit by non-ProjectileHero:" + otherGO.name);
                break;
        }
    }
    private void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }
    private void UnShowDamage()
    {
        for (var i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
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
