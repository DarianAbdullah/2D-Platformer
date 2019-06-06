using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hero.Command;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float JumpStrength = 5.0f;
    private float PrevAxis = 0.0f;
    private bool ButtonPressedSim = false;

    [SerializeField] private float AttackDuration = 0.25f;
    [SerializeField] private AnimationCurve AttackCurve;
    private float AttackTimer = 0.0f;

    [SerializeField] private float DecayDuration = 0.25f;
    [SerializeField] private AnimationCurve DecayCurve;
    private float DecayTimer = 0.0f;

    [SerializeField] private AnimationCurve SustainCurve;

    [SerializeField] private float ReleaseDuration = 0.25f;
    [SerializeField] private AnimationCurve ReleaseCurve;

    private HeroController heroController;
    private Rigidbody2D heroRigidBody;

    public bool IsJumping;
    public bool IsMoving;

    private float ReleaseTimer = 0.0f;

    private float InputDirection = 0.0f;

    private enum Phase { Attack, Decay, Sustain, Release, None };

    private Phase CurrentPhase = Phase.None;

    // Darian's changes
    public Animator animator;

    void Start()
    {
        this.heroController = this.gameObject.GetComponent<HeroController>();
        this.heroRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        float currAxis = Input.GetAxis("Horizontal");
        if ((PrevAxis == 0 && currAxis != 0) ||
            (PrevAxis < 0 && currAxis > 0) ||
            (PrevAxis > 0 && currAxis < 0))
        {
            ButtonPressedSim = true;
        }
        else
        {
            ButtonPressedSim = false;
        }

        PrevAxis = currAxis;
        //Debug.Log("GetButton: " + Input.GetButton("Horizontal"));
        //Debug.Log("Axis: " + (Input.GetAxis("Horizontal") != 0));

        if (ButtonPressedSim && Input.GetAxis("Horizontal") > 0)
        {
            // Darian's change
            animator.SetFloat("Speed", 1);
            IsMoving = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = 1.0f;
        }
        else if (ButtonPressedSim && Input.GetAxis("Horizontal") < 0)
        {
            // Darian's change
            animator.SetFloat("Speed", 1);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            IsMoving = true;
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            this.InputDirection = -1.0f;
        }

        if (ButtonPressedSim && Input.GetAxis("Horizontal") > 0)
        {
            // Darian's change
            animator.SetFloat("Speed", 1);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            IsMoving = true;
            this.InputDirection = 1.0f;
        }
        else if (ButtonPressedSim && Input.GetAxis("Horizontal") < 0)
        {
            // Darian's change
            animator.SetFloat("Speed", 1);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            IsMoving = true;
            this.InputDirection = -1.0f;
        }

        if (ButtonPressedSim && Input.GetAxis("Horizontal") == 0)
        {
            this.InputDirection = 0f;
        }

        //Debug.Log(this.CurrentPhase);

        if (this.CurrentPhase == Phase.Sustain && Input.GetAxis("Horizontal") == 0)
        {
            this.CurrentPhase = Phase.Release;
        }

        if (this.CurrentPhase != Phase.None)
        {
            var position = this.gameObject.transform.position;
            position.x += this.InputDirection * this.Speed * this.ADSREnvelope() * Time.deltaTime;
            this.gameObject.transform.position = position;
        }
        else
        {
            IsMoving = false;
        }
        if (heroController.ground)
        {
            IsJumping = false;
            if (Input.GetButtonDown("Jump"))
            {

                heroRigidBody.velocity += Vector2.up * this.JumpStrength;
            }
        }
        else
        {
            IsJumping = true;
        }
    }

    public float GetSpeed()
    {
        return this.Speed * animator.GetFloat("Speed");
    }

    public bool GetXDirection()
    {
        return gameObject.GetComponent<SpriteRenderer>().flipX;
    }

    float ADSREnvelope()
    {
        float value = 0.0f;

        if (Phase.Attack == this.CurrentPhase)
        {
            value = this.AttackCurve.Evaluate(this.AttackTimer / this.AttackDuration);
            this.AttackTimer += Time.deltaTime;
            if (this.AttackTimer > this.AttackDuration)
            {
                this.CurrentPhase = Phase.Decay;
            }
        }
        else if (Phase.Decay == this.CurrentPhase)
        {
            value = this.DecayCurve.Evaluate(this.DecayTimer / this.DecayDuration);
            this.DecayTimer += Time.deltaTime;
            if (this.DecayTimer > this.DecayDuration)
            {
                this.CurrentPhase = Phase.Sustain;
            }
        }
        else if (Phase.Sustain == this.CurrentPhase)
        {
            value = 1.0f;
        }
        else if (Phase.Release == this.CurrentPhase)
        {
            value = this.ReleaseCurve.Evaluate(this.ReleaseTimer / this.ReleaseDuration);
            this.ReleaseTimer += Time.deltaTime;
            if (this.ReleaseTimer > this.ReleaseDuration)
            {
                // Darian's change
                animator.SetFloat("Speed", 0);

                this.CurrentPhase = Phase.None;
            }
        }

        return value;
    }

    private void ResetTimers()
    {
        this.AttackTimer = 0.0f;
        this.DecayTimer = 0.0f;
        this.ReleaseTimer = 0.0f;
    }
}