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
/// Space Game Bullet
/// </summary>
public class SG_Bullet : MonoBehaviour
{
    /// <summary>
    /// Speed applied to the bullet when it moves
    /// </summary>
    [SerializeField] private float m_bulletSpeed = 50f;
    /// <summary>
    /// Life time of the bullet
    /// </summary>
    [SerializeField] private float m_lifeTime = 2.0f;
    /// <summary>
    /// Damage that the bullet does
    /// </summary>
    [SerializeField] private int m_damage = 10;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject,m_lifeTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * m_bulletSpeed * Time.deltaTime;
	}

    /// <summary>
    /// On trigger enter
    /// </summary>
    /// <param name="other">Collider of the other object</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            other.gameObject.SendMessage("Collide", m_damage);
            Destroy(this.gameObject);
        }
        
    }
}
