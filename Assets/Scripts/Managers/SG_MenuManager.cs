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
#region Variables
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
    /// Reference to End stuff
    /// </summary>
    [SerializeField] private GameObject m_EndMenuButtons;
    /// <summary>
    /// Reference to options
    /// </summary>
    [SerializeField] private GameObject m_options;
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
    [SerializeField] private Text m_scoreText;

    public bool isPaused { get { return m_pause; } }

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
        m_loadingGroup.SetActive(false);
        m_mainMenuButtons.SetActive(false);
        m_pauseMenuButtons.SetActive(false);
        m_EndMenuButtons.SetActive(false);
        m_pauseIngame.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            m_mainMenuButtons.SetActive(true);
        }
        else
        {
            m_pauseIngame.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion

#region Load Scene
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
        SG_AudioManager.Instance.StopForPause();
        m_loadingBar.fillAmount = 0f;
        m_loadingGroup.SetActive(true);
        m_mainMenuButtons.SetActive(false);
        m_pauseMenuButtons.SetActive(false);
        m_EndMenuButtons.SetActive(false);
        m_options.SetActive(false);

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
        {
            m_pauseIngame.SetActive(true);
            m_pause = false;
            Time.timeScale = 1f;
            if (SG_ScoreManager.Instance != null)
                SG_ScoreManager.Instance.ResetScore();
        }
            

        SG_AudioManager.Instance.ResumeAudios();
    }
    #endregion

#region MenuThings
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
        {
            Time.timeScale = 0f;
            SG_AudioManager.Instance.StopForPause();     
        }        
        else
        {
            Time.timeScale = 1f;
            SG_AudioManager.Instance.ResumeAudios();
        }
    }
    /// <summary>
    /// Method that shows the endscreen
    /// </summary>
    public void EndGame()
    {
        m_pause = true;
        Time.timeScale = 0f;
        SG_AudioManager.Instance.StopForPause();
        m_EndMenuButtons.SetActive(true);
        SG_ScoreManager.Instance.CheckHighestScore();
        m_scoreText.text = "Your current score is: " + SG_ScoreManager.Instance.Score + " and your highest score was: " + SG_ScoreManager.Instance.HighestScore;
    }
    /// <summary>
    /// Method that shows or hides options
    /// </summary>
    /// <param name="show"></param>
    public void ShowHideOptions(bool show)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            m_mainMenuButtons.SetActive(!show);
        }
        else
        {
            m_pauseMenuButtons.SetActive(!show);
        }

        m_options.SetActive(show);
    }
#endregion
}
