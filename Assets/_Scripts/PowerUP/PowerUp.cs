using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Vector2 rotMinMax = new Vector2(15, 90);
    private Vector2 driftMinMax = new Vector2(.25f, 2);
    private Vector3 rotPerSecond;
    private float birthTime;
    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Renderer cubeRend;
    private float lifeTime = 6f;
    private float fadeTime = 4f;
    private GameObject cube;
    private TextMesh letter;
    public WeaponType _type;
   

    private void Awake()
    {
        cube = transform.Find("Cube").gameObject;
        letter = GetComponent<TextMesh>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeRend = cube.GetComponent<Renderer>();
        Vector3 vel = Random.onUnitSphere;
        vel.z = 0;
        vel.Normalize();
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;
        transform.rotation = Quaternion.identity;
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y), Random.Range(rotMinMax.x, rotMinMax.y), Random.Range(rotMinMax.x, rotMinMax.y));
        birthTime = Time.time;
    }
    private void Update()
    {
        colorSwitch();
    }
    private void colorSwitch()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }
        if (u > 0)
        {
            Color color = cubeRend.material.color;
            color.a = 1f - u;
            cubeRend.material.color = color;
            color = letter.color;
            color.a = 1f - (u * 0.5f);
            letter.color = color;
        }
        if (!bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
        
    }
    public void AbsorbedBy(GameObject target)
    {
        Destroy(this.gameObject);
    }

   


}
