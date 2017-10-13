/*====================================================================*
 *                                                                    *
 *        Simple Template by Manuel Rodriguez Matesanz                *     
 *                       13/10/2017                                   *      
 *                                                                    *
 * ===================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Space Game Enemy
/// </summary>
public class SG_Enemy : MonoBehaviour
{
    /// <summary>
    /// Energy that the energy has in this moment
    /// </summary>
    [SerializeField] private int m_energy = 100;
    /// <summary>
    /// Maximum energy that the enemy hax 
    /// </summary>
    [SerializeField] private int m_maxEnergy = 100;
    /// <summary>
    /// How many points this spaceshipt gives after dying
    /// </summary>
    [SerializeField] private int m_pointsAfterDeath = 100;

    /// <summary>
    /// Properties for our variables
    /// </summary>
    public int Energy { get { return m_energy; } set { m_energy = value; } }
    public int MaxEnergy { get { return m_maxEnergy; } set { m_maxEnergy = value; } }
    public int PointsAfterDeath { get { return m_pointsAfterDeath; } set { m_pointsAfterDeath = value; } }

    // Use this for initialization
    void Start ()
    {
        // We set the initial energy to max
        m_energy = m_maxEnergy;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// We collide with something so we add or remove energy
    /// </summary>
    /// <param name="damage"></param>
    public void Collide(int damage = 20)
    {
        Debug.Log("Colliding with damage:" + damage);
        m_energy -= damage;
        // If we have no energy left
        if (m_energy <= 0)
        {
            // We play sound, add score and destroy this object
            SG_AudioManager.Instance.PlaySoundByPath("Sounds/explosion", SG_AudioManager.AUDIO_TYPE.SFX);
            SG_ScoreManager.Instance.AddScore(m_pointsAfterDeath);
            SG_EnemyRespawner.Instance.EliminateEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
