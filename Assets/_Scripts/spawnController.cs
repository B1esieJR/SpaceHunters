using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class spawnController : MonoBehaviour
{

    static public spawnController _instance;
    [SerializeField] private GameObject[] prefabEnemies;
    private float enemySpawnPerSecond = 0.5f;
    private float enemyDefualtPadding = 1f;
    private BoundsCheck bndcheck;

    private void Awake()
    {
        _instance = this;
        bndcheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
    private void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        float enemyPadding = enemyDefualtPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().offsetRadius);
        }
        Vector3 pos = Vector3.zero;
        float xMin = -bndcheck.cameraWidth + enemyPadding;
        float xMax = bndcheck.cameraWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndcheck.cameraHeight + enemyPadding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float spawnHeroDelay)
    {
        Invoke("Restart", spawnHeroDelay);
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

   
}
