using System;
using System.Collections;
using System.Collections.Generic;
using ECM2;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Character _character;
    public Animator _animator;
    private void Awake()
    {
        _character = this.GetComponentInParent<Character>();
        if (_character == null)
        {
            Debug.Log("dont find parent character!!!!");
        }
    }

    public void InitAnimation()
    {
        _character = this.GetComponentInParent<Character>();
        if (_character == null)
        {
            Debug.Log("dont find parent character!!!!");
        }
    }

    void Start()
    {
        _animator = _character.GetAnimator();
        
    }

    private float speed = 0;
    
    void Update()
    {
        speed = _character.GetSpeed() / _character.GetMaxSpeed();
        if (_animator!=null)
        {
            _animator.SetFloat("Speed",speed);
        }
    }
}
