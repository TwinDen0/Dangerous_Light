using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Hero : MonoBehaviour {
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravityScale = 2f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private const float maxJumpTime = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask PlayerMask;

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
            if(!isJumping)
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
        // Вычисляем начальную скорость для достижения желаемой высоты
        float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y) * gravityScale);
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        isJumping = true;
    }

    private void CheckGround() {
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.4f, PlayerMask);

        if (!isGrounded) {
            State = States.jump;
        } else {
            isJumping = false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, 0.4f);
    }

    public enum States {
        idle,
        run,
        jump
    }
}
