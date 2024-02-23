using UnityEngine;

public class Shield : MonoBehaviour
{
    private float rotationsPerSecond = 0.1f;
    private int levelShown = 0;
    private Material shieldMaterial;
    void Start()
    {
        shieldMaterial = GetComponent<Renderer>().material;
    }


    void Update()
    {
        int currentLevelShield = Mathf.FloorToInt(Hero._instance.Shield);
        if (levelShown != currentLevelShield)
            levelShown = currentLevelShield;
        shieldMaterial.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        float rZ = -(rotationsPerSecond * Time.deltaTime * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
