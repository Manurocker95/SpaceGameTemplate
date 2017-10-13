/*====================================================================*
 *                                                                    *
 *        Simple Template by Manuel Rodriguez Matesanz                *     
 *                       13/10/2017                                   *      
 *                                                                    *
 * ===================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Space Game menu manager
/// </summary>
public class SG_MenuManager : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    private static SG_MenuManager instance;
    public static SG_MenuManager Instance { get { return instance; } }
    /// <summary>
    /// Reference to audio manager prefab saved in Assets
    /// </summary>
    [SerializeField] private GameObject m_audioManagerPrefab;
    /// <summary>
    /// Reference to buttons Play, Options and Exit
    /// </summary>
    [SerializeField] private GameObject m_mainMenuButtons;
    /// <summary>
    /// Reference to buttons Resume, Options, Exit
    /// </summary>
    [SerializeField] private GameObject m_pauseMenuButtons;
    /// <summary>
    /// Reference to loading bg, text, etc
    /// </summary>
    [SerializeField] private GameObject m_loadingGroup;
    /// <summary>
    /// Reference to a bar that fills when loading
    /// </summary>
    [SerializeField] private Image m_loadingBar;
    /// <summary>
    /// If we've paused the game or not
    /// </summary>
    [SerializeField] private bool m_pause = false;
    /// <summary>
    /// Tipical button to pause the game
    /// </summary>
    [SerializeField] private GameObject m_pauseIngame;

    public bool isPaused { get { return m_pause; } }

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
        // We need an audio manager, so if it doesn't exists, we instantiate it
		if (SG_AudioManager.Instance == null && m_audioManagerPrefab != null)
        {
            Instantiate(m_audioManagerPrefab);
        }
        
        m_loadingGroup.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Method called when pressing play button, it calls a corroutine
    /// </summary>
    public void LoadScene(int sceneIndex = 1)
    {
        StartCoroutine(LoadingScene(sceneIndex));
    }
    /// <summary>
    /// Corroutine that loads a scene 
    /// </summary>
    /// <param name="sceneIndex">Index to the scene we want to load</param>
    /// <param name="delay">Delay after loading</param>
    /// <returns></returns>
    IEnumerator LoadingScene(int sceneIndex, float delay = 5f)
    {
        m_loadingBar.fillAmount = 0f;
        m_loadingGroup.SetActive(true);
        m_mainMenuButtons.SetActive(false);

        AsyncOperation ls = SceneManager.LoadSceneAsync(sceneIndex);

        while (!ls.isDone)
        {
            m_loadingBar.fillAmount = ls.progress;
            yield return null;
        }

        yield return delay;

        // We disable loading images
        m_loadingGroup.SetActive(false);

        // We activate main menu stuff in main menu
        if (sceneIndex == 0)
            m_mainMenuButtons.SetActive(true);
        else
            m_pauseIngame.SetActive(true);
    }

    /// <summary>
    /// Method when pressing Exit button
    /// </summary>
    public void ExitGame()
    {
        // This only exits on build, NOT on editor
        Application.Quit();
    }
    /// <summary>
    /// Method that pauses the game and resume it
    /// </summary>
    public void PauseGame(bool _pause)
    {
        m_pause = _pause;
        m_pauseMenuButtons.SetActive(_pause);
        m_pauseIngame.SetActive(!_pause);
        if (m_pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
