using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Space Game Enemy Respawner
/// </summary>
public class SG_EnemyRespawner : MonoBehaviour
{
#region Variables
    /// <summary>
    /// Singleton
    /// </summary>
    private static SG_EnemyRespawner instance;
    public static SG_EnemyRespawner Instance { get { return instance; } }
    /// <summary>
    /// Arraylist for enemies
    /// </summary>
    private List <GameObject> m_enemyArrayList = new List<GameObject>();
    /// <summary>
    /// Number of enemies that respawn
    /// </summary>
    [SerializeField] private int m_numberOfEnemiesToRespawn = 1;
    /// <summary>
    /// Enemies prefabs
    /// </summary>
    [SerializeField] private GameObject[] m_EnemyPrefabs;
    /// <summary>
    /// Time to respawn without enemies!
    /// </summary>
    [SerializeField] private float m_timeToRespawnNoEnemies = 20f;
    /// <summary>
    /// Time to spawn
    /// </summary>
    [SerializeField] private float m_timeToRespawn = 100f;
    /// <summary>
    /// Timer for respawning
    /// </summary>
    private float m_timer = 0f;
    #endregion

#region MonoStuff
    // This runs before Start
    private void Awake()
    {
        // if there's no previous SG_ScoreManager instance we use this
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else // There's is a SG_ScoreManager already in Scene, we need to avoid that
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (m_numberOfEnemiesToRespawn < 0)
            m_numberOfEnemiesToRespawn = 1;

        m_timer = 0;
        RespawnEnemy();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // If we don't have any enemy we respwn them
	    if (m_enemyArrayList.Count == 0 && m_EnemyPrefabs.Length > 0)
        {
            if (m_timer < m_timeToRespawnNoEnemies)
                m_timer += Time.deltaTime;
            else
            {
                m_timer = 0;
                RespawnEnemy();
            }
        }
        else
        {
            if (m_timer < m_timeToRespawn)
                m_timer += Time.deltaTime;
            else
            {
                m_timer = 0;
                RespawnEnemy();
            }
        }
	}
    #endregion

#region EnemyStuff
    /// <summary>
    /// Method that deletes an item from the list
    /// </summary>
    /// <param name="enemy"></param>
    public void EliminateEnemy(GameObject enemy)
    {
        m_enemyArrayList.Remove(enemy);
        if (m_enemyArrayList.Count == 0)
            m_timer = 0;
    }

    /// <summary>
    /// Method that Spawn enemies
    /// </summary>
    void RespawnEnemy()
    {
        for (int i = 0; i < m_numberOfEnemiesToRespawn; i++)
        {
            m_enemyArrayList.Add(Instantiate(m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)], new Vector3(Random.Range(40,60), Random.Range(20, -20), 20),Quaternion.Euler(-90,180,0)));
        }
    }
#endregion
}
