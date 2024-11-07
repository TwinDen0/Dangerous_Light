using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Hero : MonoBehaviour {
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 1.0f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private States State {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate() {
        CheckGround();
    }

    private void Update() {

        if (isGrounded)
            State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButton("Jump"))
            Jump();
    }

    private void Run() {
        if (isGrounded)
            State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
    
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump() {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        isGrounded = collider.Length > 1;
        if (!isGrounded)
            State = States.jump;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

    public enum States {
        idle,
        run,
        jump
    }
}
