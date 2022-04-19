using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForcePowerup : BasePowerup // INHERITANCE
{
    [SerializeField] private float _jumpForce = 1000f;

    protected override IEnumerator ApplyEffect()    // POLYMORPHISM
    {
        // Temporary save player's jump force and apply the effect jump force
        float previousJumpForce = _playerController.JumpForce;
        _playerController.JumpForce = _jumpForce;
        
        // Wait for the effect duration and display the duration
        while (_duration >= 0)
        {
            _duration -= Time.deltaTime;
            UIPowerupTimer.Text.enabled = true;
            UIPowerupTimer.Display(_duration);
            yield return null;
        }
        UIPowerupTimer.Text.enabled = false;
        
        // Revert to the initial jump force
        _playerController.JumpForce = previousJumpForce;
        _playerController.IsPlayerUnderEffect = false;
        Destroy(gameObject);
    }
}
