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
/// Space Game Player Controller
/// </summary>
public class SG_PlayerController : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Player gameobject Reference
    /// </summary>
    [SerializeField] private GameObject m_playerGO;
    /// <summary>
    /// Player movement speed
    /// </summary>
    [SerializeField] private float m_movementSpeed = 1f;
    /// <summary>
    /// Limit for vertical axis
    /// </summary>
    [SerializeField] private float m_verticalLimit = 10f;
    /// <summary>
    /// Limit for horizontal axis
    /// </summary>
    [SerializeField] private float m_horizontalLimit = 20f;
    /// <summary>
    /// Timer to fire when gets the time
    /// </summary>
    [SerializeField] private float m_timer = 0f;
    /// <summary>
    /// Time to fire
    /// </summary>
    [SerializeField] private float m_timeToFire = 0.4f;
    /// <summary>
    /// Player Reference
    /// </summary>
    [SerializeField] private SG_Player m_playerReference;
#endregion

    #region Mono Stuff
    // Use this for initialization
    void Start ()
    {
        /// If we don't have the player reference, we look for it
		if (m_playerGO == null)
        {
            m_playerGO = GameObject.Find("Player");
        }
        if (m_playerReference == null && m_playerGO)
        {
            m_playerReference = m_playerGO.GetComponent<SG_Player>();
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        /// if we press WASD/Arrows/joystick
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            /// We move the player
            m_playerGO.transform.position += (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * m_movementSpeed);
            m_playerGO.transform.position = new Vector3(Mathf.Clamp(m_playerGO.transform.position.x, -m_horizontalLimit, m_horizontalLimit), Mathf.Clamp(m_playerGO.transform.position.y, -m_verticalLimit, m_verticalLimit), m_playerGO.transform.position.z);
        }

        /// if we press spacebar and we can shoot
        if (Input.GetKey(KeyCode.Space) && m_timer>=m_timeToFire)
        {
            /// Fire
            m_playerReference.Fire();
            m_timer = 0;
        }
        else
        {
            /// Timer for allowing shooting
            if (m_timer < m_timeToFire)
                m_timer += Time.deltaTime;
        }
    }
#endregion
}
