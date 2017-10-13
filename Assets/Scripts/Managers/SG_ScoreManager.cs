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
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Method that adds score to the variable score and modify the text so we can see that add
    /// </summary>
    /// <param name="m_score"></param>
    public void AddScore(int _score = 100)
    {
        m_score += _score;
        m_scoreText.text = "Score: " + m_score + " pts";
    }
}
