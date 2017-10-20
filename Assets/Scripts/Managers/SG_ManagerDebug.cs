using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Space Game debug stuff - just instantiate if menu doesn't exists
/// </summary>
public class SG_ManagerDebug : MonoBehaviour
{
    /// <summary>
    /// Reference to AudioManagerPrefab
    /// </summary>
    [SerializeField] private GameObject m_AudioManagerPrefab;
    /// <summary>
    /// Reference to menu manager prefab
    /// </summary>
    [SerializeField] private GameObject m_MenuManagerPrefab;

    // Use this for initialization
    void Start()
    {
        // We need an audio manager, so if it doesn't exists, we instantiate it
        if (SG_AudioManager.Instance == null && m_AudioManagerPrefab != null)
        {
            Instantiate(m_AudioManagerPrefab);
        }
        // We need an audio manager, so if it doesn't exists, we instantiate it
        if (SG_MenuManager.Instance == null && m_MenuManagerPrefab != null)
        {
            Instantiate(m_MenuManagerPrefab);
        }
    }
}
