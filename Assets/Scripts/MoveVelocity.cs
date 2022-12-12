using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 velocityVector;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float maxSpeed = 7f;
    private PlayerController _controller;

    [Header("Physics")]
    public float linearDrag = 4f;

    public float gravity = 1;
    public float fallMultiplier = 2.5f;

    public bool FacingRight;

    
    private bool onColision;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    //[SerializeField] private PlayerController playerController;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        velocityVector = new Vector2( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        if (_controller.IsGrounded() && Input.GetButton("Jump"))
        {
            Debug.Log("J");
            Jump();
        }

        if (velocityVector.x != 0)
        {
            animator.SetBool("ifMove", true);
            if (velocityVector.x < 0 && FacingRight)
                Flip();
            if (velocityVector.x > 0 && !FacingRight)
                Flip();
        }
        if (velocityVector.x == 0)
            animator.SetBool("ifMove", false);

    }

    private void FixedUpdate()
    {
        ModifyPhysics();

        MoveCharacter(velocityVector.x);



    }

    private void MoveCharacter(float horizontal)
    {
        rigidbody2D.AddForce(Vector2.right * horizontal * moveSpeed);

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetVelocityX(float velocityVectorX)
    {
        this.velocityVector.x = velocityVectorX;
    }

    public void SetVelocityOnColision(Vector3 velocityVector, bool onColisison)
    {
        this.velocityVector = velocityVector;
        this.onColision = onColisison;
    }

    public Vector3 GetVelocityVector()
    {
        return velocityVector;
    }

    public void ModifyPhysics()
    {

        bool changingDirection = (velocityVector.x > 0 && rigidbody2D.velocity.x < 0) || (velocityVector.x < 0 && rigidbody2D.velocity.x > 0);
        if (_controller.IsGrounded())
        {
            if (Mathf.Abs(velocityVector.x) < 0.4f || changingDirection)
            {
                rigidbody2D.drag = linearDrag;

            }
            else
            {
                rigidbody2D.drag = 0;

            }

            rigidbody2D.gravityScale = 0;

        }
        else
        {
            rigidbody2D.gravityScale = gravity;
            rigidbody2D.drag = linearDrag * 0.15f;
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.gravityScale = gravity * fallMultiplier;
            }
            else if (rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rigidbody2D.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

   

    public void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        Debug.Log("Jump");
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public float GetJumpVelocity()
    {
        return jumpVelocity;
    }

    public float GetFallMultiplier()
    {
        return fallMultiplier;
    }
}