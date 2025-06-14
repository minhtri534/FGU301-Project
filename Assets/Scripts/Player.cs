using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float max_move_speed = 6;
    public float acceleration = 25;
    public float jump_force = 9;
    public Animator anim;
    private Rigidbody2D rb;
    public ContactFilter2D ContactFilter;
    //private bool IsGrounded => rb.IsTouching(ContactFilter);
    private InputAction move;
    private InputAction jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        float move_value = move.ReadValue<float>();
        // Increase speed by acceleration until max speed
        if (Mathf.Abs(rb.linearVelocityX) < max_move_speed)
        {
            rb.linearVelocityX += acceleration * move_value * Time.deltaTime;
        }
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -max_move_speed, max_move_speed);
        if (move_value == 0)
        {
            rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, 0, acceleration * Time.deltaTime);
        }
        

        // Jumping
        if (jump.WasPressedThisFrame() && IsGrounded())
        {
            rb.linearVelocityY = jump_force;
        }

        // Flip model
        if (move.ReadValue<float>() != 0)
        {
            transform.localScale = new Vector3(move_value, 1, 1);
        }
        // Animation(cần có trị tuyệt đối do đk là lớn hơn 0.1 thì player run)
        anim.SetFloat("move", Mathf.Abs(move_value));

        //  (chưa xong)
        anim.SetBool("jump", !IsGrounded());
    }

    private bool IsGrounded()
    {
        return rb.IsTouching(ContactFilter);
    }
}
