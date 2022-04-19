using System.Collections;
using UnityEngine;

public abstract class BasePowerup : MonoBehaviour   // INHERITANCE
{
    [SerializeField] protected float _duration = 5f;
    
    protected PlayerController _playerController;
    private MeshRenderer _renderer;
    private Collider _collider;

    private void Awake()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    // Method used by the children before applying the effect
    public void StartEffect()   // ABSTRACTION
    {
        _playerController.IsPlayerUnderEffect = true;
        _renderer.enabled = false;
        _collider.enabled = false;
        StartCoroutine(ApplyEffect());
    }

    // Method used by the children to apply the powerup effects
    protected abstract IEnumerator ApplyEffect();   // POLYMORPHISM
}
