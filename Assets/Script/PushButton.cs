using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material _hoverColor;
    [SerializeField] private Animator _movingPlatformAnimator;

    private MeshRenderer _renderer;
    private Material _defaultColor;

    public UnityEvent _onPush;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColor = _renderer.material;
    }

    public void IsActiveAnimationTrue()
    {
        _movingPlatformAnimator.SetBool("isActive", true);
    }

    public void IsActiveAnimationFalse()
    {
        _movingPlatformAnimator.SetBool("isActive", false);
    }

    public void OnHoverEnter()
    {
        _renderer.material = _hoverColor;
    }

    public void OnHoverExit()
    {
        _renderer.material = _defaultColor;
    }

    public void OnSelect() {
        // turn on animation bool
        Debug.Log("Button pushed");
        _onPush?.Invoke();
    }

}
