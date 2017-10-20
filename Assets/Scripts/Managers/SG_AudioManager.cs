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
/// Space game audio manager
/// </summary>
public class SG_AudioManager : MonoBehaviour
{
#region Variables
    /// <summary>
    /// Singleton
    /// </summary>
    private static SG_AudioManager instance;
    public static SG_AudioManager Instance { get { return instance; } }

    /// <summary>
    /// AudioSources (First = BGM, Second SFX 1, Sound SFX 2). More can be added from inspector
    /// </summary>
    [SerializeField] private AudioSource[] m_audioSources;

    /// <summary>
    /// Audio types (BGM or sound effect)
    /// </summary>
    public enum AUDIO_TYPE { BGM, SFX}
    #endregion

#region Mono Stuff
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
        // We set every audioSource component to the array
        m_audioSources = GetComponents<AudioSource>();
        PlaySoundByPath("Sounds/bgm", AUDIO_TYPE.BGM);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion

#region Sound Methods
    /// <summary>
    /// We play a sound loaded in real time
    /// </summary>
    /// <param name="name"> name of the sound we want to play. Must be placed in Assets/Resources/</param>
    public void PlaySoundByPath(string path, AUDIO_TYPE type)
    {
        AudioClip audio = (AudioClip) Resources.Load(path);

        switch(type)
        {
            case AUDIO_TYPE.BGM:
                m_audioSources[0].clip = audio;
                m_audioSources[0].Play();
                break;
            case AUDIO_TYPE.SFX:
                m_audioSources[1].PlayOneShot(audio);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// We play a sound previously loaded
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="type"></param>
    public void PlaySound (AudioClip audio, AUDIO_TYPE type)
    {
        switch (type)
        {
            case AUDIO_TYPE.BGM:
                m_audioSources[0].clip = audio;
                m_audioSources[0].Play();
                break;
            case AUDIO_TYPE.SFX:
                m_audioSources[1].PlayOneShot(audio);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// We stop every audiosource when pausing
    /// </summary>
    public void StopForPause()
    {
        foreach (AudioSource a in m_audioSources)
        {
            a.Pause();
        }
    }
    /// <summary>
    /// We resume every audiosource
    /// </summary>
    public void ResumeAudios()
    {
        foreach (AudioSource a in m_audioSources)
        {
            a.Play();
           
        }
    }
    /// <summary>
    /// Mute all audiosources
    /// </summary>
    public void MuteAudios()
    {
        foreach (AudioSource a in m_audioSources)
        {
            a.mute = !a.mute;

        }
    }
#endregion
}
