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

/// <summary>
/// Space Game Score Manager
/// </summary>
public class SG_ScoreManager : MonoBehaviour
{

#region Variables
    /// <summary>
    /// Singleton
    /// </summary>
    private static SG_ScoreManager instance;
    public static SG_ScoreManager Instance { get { return instance; } }
    /// <summary>
    /// Scene reference to the score text
    /// </summary>
    [SerializeField] private Text m_scoreText;
    /// <summary>
    /// Score value
    /// </summary>
    [SerializeField] private int m_score = 0;
    /// <summary>
    /// Highest score ever obtained
    /// </summary>
    private int m_highestScore = 0;
    /// <summary>
    /// Property for score
    /// </summary>
    public int Score { get { return m_score; } }
    /// <summary>
    /// Property for highest score
    /// </summary>
    public int HighestScore { get { return m_highestScore; } }
    #endregion

#region MonoStuff
    // This runs before Start
    private void Awake()
    {
        // if there's no previous SG_ScoreManager instance we use this
        if (instance == null)
        {
            instance = this;
        }
        else // There's is a SG_ScoreManager already in Scene, we need to avoid that
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization 
    void Start () {

        // Score starts on zero
        m_score = 0;
        // We set the text for score
        m_scoreText.text = "Score: " + m_score + " pts";

        m_highestScore = PlayerPrefs.GetInt("highestScore");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion

#region ScoreStuff
    /// <summary>
    /// Method that adds score to the variable score and modify the text so we can see that add
    /// </summary>
    /// <param name="m_score"></param>
    public void AddScore(int _score = 100)
    {
        m_score += _score;
        m_scoreText.text = "Score: " + m_score + " pts";
    }
    /// <summary>
    /// MEthod that sets to zero the score
    /// </summary>
    public void ResetScore()
    {
        m_score = 0;
        m_scoreText.text = "Score: " + m_score + " pts";
    }
    /// <summary>
    /// We save the score in game data if it's better than other score
    /// </summary>
    public void CheckHighestScore()
    {
        if (m_score > m_highestScore)
        {
            m_highestScore = m_score;
            PlayerPrefs.SetInt("highestScore", m_score);
        }
    }
#endregion
}
