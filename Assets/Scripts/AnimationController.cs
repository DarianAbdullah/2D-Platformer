using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Animator animator;
    private MoveCharacter movement;
    private HeroController hero;
    [HideInInspector]

    public SpriteRenderer sprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<MoveCharacter>();
        sprite = GetComponent<SpriteRenderer>();
        hero = GetComponentInParent<HeroController>();
    }

    void Update()
    {
        animator.SetBool("IsJumping", movement.IsJumping);
        animator.SetBool("IsMoving", movement.IsMoving);
        animator.SetBool("IsAttacking", hero.IsAttacking);
        animator.SetBool("IsHurt", hero.IsHurt);
    }
}
