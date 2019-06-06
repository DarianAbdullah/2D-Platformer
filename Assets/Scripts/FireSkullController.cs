using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkullController : MonoBehaviour
{
    private float WaitTime = 0.5f;
    private float Counter = 0;
    private int Health = 1;
    private bool Dead = false;
    [SerializeField] GameObject player;
    public AudioSource[] audioSources;
    private AudioSource ExistAudio;
    private AudioSource DeathAudio;

    //Darian's changes
    public bool IsDead;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        ExistAudio = audioSources[0];
        DeathAudio = audioSources[1];

        // Darian's changes
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (Health <= 0)
        {
            gameObject.layer = 10;
            SkullDeath();
        }
    }

    void SkullDeath()
    {
        if (!Dead)
        {
            DeathAudio.Play(0);
            Dead = true;
            IsDead = true;
            animator.SetBool("IsDead", IsDead);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (!DeathAudio.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    public void SkullHit(string weapon)
    {
        SkullDeath();
        Health = 0;
    }
}
