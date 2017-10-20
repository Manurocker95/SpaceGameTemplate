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
/// Space Game Character
/// </summary>
public class SG_Character : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Bullet prefab Referecence
    /// </summary>
    public GameObject m_bulletPrefab;
    /// <summary>
    /// Positions where the player fire
    /// </summary>
    public Transform[] m_firePosition;
    /// <summary>
    /// Energy that the energy has in this moment
    /// </summary>
    [SerializeField] protected int m_energy = 100;
    /// <summary>
    /// Maximum energy that the enemy hax 
    /// </summary>
    [SerializeField] protected int m_maxEnergy = 100;
    #endregion

    #region methods that children will implement
    /// <summary>
    /// Fire method common for children 
    /// </summary>
    public virtual void Fire()
    {
        SG_AudioManager.Instance.PlaySoundByPath("Sounds/shoot", SG_AudioManager.AUDIO_TYPE.SFX);
        for (int i = 0; i < m_firePosition.Length; i++)
        {
            /// We instantiate the bullet so it can move
            Instantiate(m_bulletPrefab, m_firePosition[i].position, m_firePosition[i].rotation);
        }
    }
    /// <summary>
    /// We collide with something so we add or remove energy
    /// </summary>
    /// <param name="damage"></param>
    public virtual void Collide(int damage = 20)
    {
        m_energy -= damage;
    }
#endregion
}
