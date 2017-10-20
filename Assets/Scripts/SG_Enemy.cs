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
public class SG_Enemy : SG_Character
{
    #region variables
    /// <summary>
    /// States for the state machine. Each state has it's own stuff
    /// </summary>
    enum ENEMY_STATES { PATROL, ANGRY, FLEE }
    /// <summary>
    /// My current state
    /// </summary>
    [SerializeField] private ENEMY_STATES m_enemyStates = ENEMY_STATES.PATROL;
    /// <summary>
    /// How many points this spaceshipt gives after dying
    /// </summary>
    [SerializeField] private int m_pointsAfterDeath = 100;
    /// <summary>
    /// How many points to get angry
    /// </summary>
    [SerializeField] private int m_energyToGetAngry = 70;
    /// <summary>
    /// How many points to flee
    /// </summary>
    [SerializeField] private int m_energyToFlee = 10;
    /// <summary>
    /// Reference to the animator
    /// </summary>
    [SerializeField] private Animator m_animator;
    /// <summary>
    /// Bullet prefab Referecence
    /// </summary>
    [SerializeField] private GameObject m_bulletPrefab;
    /// <summary>
    /// Properties for our variables
    /// </summary>
    public int Energy { get { return m_energy; } set { m_energy = value; } }
    public int MaxEnergy { get { return m_maxEnergy; } set { m_maxEnergy = value; } }
    public int PointsAfterDeath { get { return m_pointsAfterDeath; } set { m_pointsAfterDeath = value; } }
    #endregion

    #region Mono Stuff
    // Use this for initialization
    void Start ()
    {
        // We set the initial energy to max
        m_energy = m_maxEnergy;

        // if we don't set the animator by inspector, we get it from component
        if (!m_animator)
        {
            m_animator = GetComponent<Animator>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
#endregion

    #region Behaviour State Machine
    private void FixedUpdate()
    {
        // Finit state machine
        switch (m_enemyStates)
        {
            case ENEMY_STATES.PATROL:
                Patrol();   /// We are patroling
                break;
            case ENEMY_STATES.ANGRY:
                Angry();    /// We are upset!
                break;
            case ENEMY_STATES.FLEE:
                Flee();     /// We want to scape
                break;
        }
    }

    /// <summary>
    /// Patrol behaviour
    /// </summary>
    void Patrol()
    {
        // Firstly we check if we need to change the state
        if (m_energy<=m_energyToGetAngry)
        {
            Debug.Log("Change to angry");
            m_enemyStates = ENEMY_STATES.ANGRY;
        }
        else if (m_energy <= m_energyToFlee)
        {
            Debug.Log("Change to flee");
            m_enemyStates = ENEMY_STATES.FLEE;
        }

        // We set the animation for patrol
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Patrol"))
        {
            m_animator.SetTrigger("Patrol");
        }
    }

    /// <summary>
    /// Angry behaviour
    /// </summary>
    void Angry()
    {
        if (m_energy > m_energyToGetAngry)
        {
            Debug.Log("Change to angry");
            m_enemyStates = ENEMY_STATES.PATROL;
        }
        else if (m_energy <= m_energyToFlee)
        {
            Debug.Log("Change to flee");
            m_enemyStates = ENEMY_STATES.FLEE;
        }

        // We set the animation for Angry
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Angry"))
        {
            m_animator.SetTrigger("Angry");
        }

        // Left because it's rotated 
        Debug.DrawRay(transform.position, Vector3.left * 100f, Color.cyan);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.left, out hit, 100.0f))
        {
            // if we can shoot to the player, we do so
            if (hit.transform.gameObject.tag == "Player")
            {
                Fire();
            }
        }

        // Enemy gain health in this 
        m_energy += 1;
    }
    /// <summary>
    /// Flee behaviour
    /// </summary>
    void Flee()
    {
        if (m_energy > m_energyToGetAngry)
        {
            Debug.Log("Change to angry");
            m_enemyStates = ENEMY_STATES.PATROL;
        }
        else if (m_energy > m_energyToFlee)
        {
            Debug.Log("Change to flee");
            m_enemyStates = ENEMY_STATES.ANGRY;
        }

        // We set the animation for Angry
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Flee"))
        {
            m_animator.SetTrigger("Flee");
        }

        // Enemy gain health in this 
        m_energy += 1;
    }
    #endregion

    #region Character Methods
    /// <summary>
    /// Fire method
    /// </summary>
    public override void Fire()
    {
        base.Fire();
        Debug.Log("Enemy Fires");
    }

    /// <summary>
    /// We collide with something so we add or remove energy
    /// </summary>
    /// <param name="damage"></param>
    public override void Collide(int damage = 20)
    {
        Debug.Log("Colliding on enemy with damage:" + damage);
        base.Collide(damage);
        // If enemy have no energy left
        if (m_energy <= 0)
        {
            // We play sound, add score and destroy this object
            SG_AudioManager.Instance.PlaySoundByPath("Sounds/explosion", SG_AudioManager.AUDIO_TYPE.SFX);
            SG_ScoreManager.Instance.AddScore(m_pointsAfterDeath);
            SG_EnemyRespawner.Instance.EliminateEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
    }
#endregion
}
