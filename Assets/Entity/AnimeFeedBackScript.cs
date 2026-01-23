using System;
using UnityEngine;

public class AnimeFeedBackScript : MonoBehaviour
{
    private Animator animator;
    public string animName;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            
        }
    }
}
