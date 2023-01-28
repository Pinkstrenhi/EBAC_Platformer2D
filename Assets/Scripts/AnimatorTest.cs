using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;
    public KeyCode keyToPlay = KeyCode.A;
    public string boolToPlay = "Fly";

    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPlay))
        {
            animator.SetBool(boolToPlay, !animator.GetBool(boolToPlay));
        }
    }
}
