using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Player : SG_Character
{
    #region Character Methods
    /// <summary>
    /// We collide with something so we add or remove energy
    /// </summary>
    /// <param name="damage"></param>
    public override void Collide(int damage = 20)
    {
        base.Collide(damage);
        Debug.Log("Colliding on player with damage:" + damage);
        // If enemy have no energy left
        if (m_energy <= 0)
        {
            SG_AudioManager.Instance.PlaySoundByPath("Sounds/explosion", SG_AudioManager.AUDIO_TYPE.SFX);
            SG_MenuManager.Instance.EndGame();
        }
    }
    /// <summary>
    /// Method that instantiate the prefab
    /// </summary>
    public override void Fire()
    {
        Debug.Log("Player Fires");
        base.Fire();
    }
#endregion
}
