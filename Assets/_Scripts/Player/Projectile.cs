using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _boundsoffset;
    private BoundsCheck _bndCheck;
    private Renderer _rend;
    public Rigidbody _rigid;
    [SerializeField] private WeaponType _type;
    public WeaponType type
    {
        get { return _type; }
        set { SetType(value); }
    }
    private void Awake()
    {
        _boundsoffset = GetComponent<BoundsCheck>().offsetRadius = -1;
        _bndCheck = GetComponent<BoundsCheck>();
        _rend = GetComponent<Renderer>();
        _rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_bndCheck.offUp)
            Destroy(gameObject);
    }
    public void SetType(WeaponType type)
    {
        _type = type;
        WeaponDefinition def = spawnController.GetWeaponDefinition(_type);
        _rend.material.color = def.projectileColor;
    }
}
