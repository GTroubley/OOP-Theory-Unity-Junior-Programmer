using System.Collections;
using UnityEngine;

public class SpeedPowerup : BasePowerup // INHERITANCE
{
    [SerializeField] private float _speed = 20f;
    
    protected override IEnumerator ApplyEffect()    // POLYMORPHISM
    {
        // Temporary save player's speed and apply the effect speed
        float previousSpeed = _playerController.Speed;
        _playerController.Speed = _speed;
        
        // Wait for the effect duration and display the duration
        while (_duration >= 0)
        {
            _duration -= Time.deltaTime;
            UIPowerupTimer.Text.enabled = true;
            UIPowerupTimer.Display(_duration);
            yield return null;
        }
        UIPowerupTimer.Text.enabled = false;
        
        // Revert to the initial speed
        _playerController.Speed = previousSpeed;
        _playerController.IsPlayerUnderEffect = false;
        Destroy(gameObject);
    }
}
