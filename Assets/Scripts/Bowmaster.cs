using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowmaster : MonoBehaviour
{
    //public ParticleSystem dust;

    [Header("Health")]
    private float health;
    [SerializeField] private float maxHealth;

    public AudioSource footSteps;

    public Animator animatior;

    public float moveSpeed;

    public Rigidbody2D rb;
    [SerializeField] Transform hand;

    Vector2 movement;

    void Update()
    {
        MovementInput();
        RotateHand();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    void RotateHand()
    {
        float angle = Utility.AngleTowardsMouse(hand.position);
        hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    void MovementInput()
    {
        animatior.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        movement = new Vector2(mx, my).normalized;

        //if(movement != Vector2.zero)
        //{
        //    if (!footSteps.isPlaying)
        //    {
        //        footSteps.Play();
        //    }
        //    else
        //    {
        //        footSteps.Stop();
        //    }
        //}
    }

    //void CreateDust()
    //{
    //    dust.Play();
    //}

    public void Dead()
    {
        rb.velocity = Vector2.zero;

        this.enabled = false;
    }
}
