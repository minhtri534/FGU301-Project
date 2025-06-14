using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float move_speed = 6;
    public float jump_force = 6;
    public Animator anim;
    private Rigidbody2D rb;
    public ContactFilter2D ContactFilter;
    //private bool IsGrounded => rb.IsTouching(ContactFilter);
    private InputAction move;
    private InputAction jump;

    void Start()
    {
        move_speed = 6;
        jump_force = 9;
        rb = GetComponent<Rigidbody2D>();
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = move_speed * move.ReadValue<float>();
        // Jumping
        if (jump.WasPressedThisFrame() && IsGrounded())
        {
            rb.linearVelocityY = jump_force;
        }
        // Flip model
        if (move.ReadValue<float>() != 0)
        {
            transform.localScale = new Vector3(10 * move.ReadValue<float>(), 10, 1);
        }
        // Animation(cần có trị tuyệt đối do đk là lớn hơn 0.1 thì player run)
        anim.SetFloat("move", Mathf.Abs(move.ReadValue<float>()));

        //  (chưa xong)
        anim.SetBool("jump", !IsGrounded());
    }

    private bool IsGrounded()
    {
        return rb.IsTouching(ContactFilter);
    }
}
