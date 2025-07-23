using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    readonly private float coyoteTime = 0.2f;
    readonly private float maxFallVelocity = -20;
    public float MaxMoveSpeed = 8;
    public float Acceleration = 90;
    public float JumpForce = 13;
    public ContactFilter2D ContactFilter;
    public Animator Anim;
    public bool IsDead = false;
    private Rigidbody2D rb;
    private Collider2D c;
    private InputAction move;
    private InputAction jump;
    private bool hasJumped = false;
    private float currentCoyoteTime = 0.0f;
    private bool canCoyoteJump = false;
    private GameObject gameManager;

    void Start()
    {
        c = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        rb.gravityScale = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            // prevent the player from falling too fast
            if (rb.linearVelocityY < maxFallVelocity)
            {
                rb.linearVelocityY = maxFallVelocity;
            }
            float move_value = move.ReadValue<float>();
            // Increase speed by Acceleration until max speed
            if (Mathf.Abs(rb.linearVelocityX) < MaxMoveSpeed)
            {
                rb.linearVelocityX += Acceleration * move_value * Time.deltaTime;
            }
            rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -MaxMoveSpeed, MaxMoveSpeed);
            if (move_value == 0)
            {
                rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, 0, Acceleration * Time.deltaTime);
            }


            // coyote jump
            if (IsGrounded())
            {
                canCoyoteJump = true;
                currentCoyoteTime = 0;
            }
            else if (!jump.IsPressed())
            {
                currentCoyoteTime += Time.deltaTime;
                if (currentCoyoteTime >= coyoteTime)
                {
                    canCoyoteJump = false;
                }
            }

            // Jumping with variable height
            if (jump.IsPressed())
            {
                if (IsGrounded() || canCoyoteJump)
                {
                    rb.linearVelocityY = JumpForce;
                    canCoyoteJump = false;
                    hasJumped = true;
                }
            }
            if (hasJumped && jump.WasReleasedThisFrame())
            {
                hasJumped = false;
                rb.linearVelocityY *= 0.5f;
            }


            // Flip model
            if (move.ReadValue<float>() != 0)
            {
                transform.localScale = new Vector3(move_value, 1, 1);
            }
            // Animation(cần có trị tuyệt đối do đk là lớn hơn 0.1 thì player run)
            Anim.SetFloat("move", Mathf.Abs(move_value));

            //  (chưa xong)
            Anim.SetBool("jump", !IsGrounded());
        }
    }

    private bool IsGrounded()
    {
        return rb.IsTouching(ContactFilter);
    }

    public void Bounce()
    {
        rb.linearVelocityY = JumpForce;
    }

    public IEnumerator Die()
    {
        IsDead = true;
        Destroy(c);
        rb.linearVelocityY = JumpForce;

        var gm = FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            gm.GameOver();
        }

        yield return new WaitForSeconds(4);
        Destroy(gameObject);
        gameManager.GetComponent<GameManager>().GameOver();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
