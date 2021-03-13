using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalMove;
    private bool facingRight = true;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float runSpeed;
    private Animator animator;

    [SerializeField] private bool jumping;
    private bool doubleJumping;
    [SerializeField] private float jumpForce;
    public int extraJumps;
    [SerializeField] private int extraJumpsValue;
    private bool falling;
    private float slidePower = 1;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private bool _onGround;
    [SerializeField] private bool _onWall;
    [SerializeField] private bool onLeftWall;
    [SerializeField] private bool onRightWall;
    [SerializeField] private bool wallSlide;

    public bool onGround { get { return _onGround; } }
    public bool onWall { get { return _onWall; } }

    [SerializeField] private Vector2 groundOffset;
    [SerializeField] private Vector2 leftOffset;
    [SerializeField] private Vector2 rightOffset;

    [SerializeField] private Vector2 sizeCapsule;
    [SerializeField] private float angleCapsule;
    [SerializeField] private float collisionRadius;

    [SerializeField] private ParticleSystem particleDust;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube((Vector2)transform.position + groundOffset, sizeCapsule);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
    }

    // Update is called once per frame
    void Update()
    {
        _onGround =
            Physics2D.OverlapCapsule((Vector2)transform.position + groundOffset, sizeCapsule, CapsuleDirection2D.Horizontal, angleCapsule, groundLayer)
            || Physics2D.OverlapCapsule((Vector2)transform.position + groundOffset, sizeCapsule, CapsuleDirection2D.Horizontal, angleCapsule, platformLayer);
        _onWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);

        horizontalMove = Input.GetAxis("Horizontal");

        falling = !_onGround && rigidbody2D.velocity.y < 0;

        if (falling && !jumping)
            doubleJumping = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            extraJumps--;
            jumping = true;

            if (extraJumps <= 0 && !wallSlide)
                doubleJumping = true;
        }

        if (_onGround && !jumping)
        {
            extraJumps = extraJumpsValue;
        }

        if (_onWall && !_onGround && falling)
        {
            wallSlide = true;
        }

        if ((_onWall && _onGround) || !_onWall)
        {
            wallSlide = false;
        }

        animator.SetFloat("running", _onWall ? 0 : horizontalMove);
        animator.SetBool("jumping", jumping);
        animator.SetBool("falling", falling);
        animator.SetBool("wallSlide", wallSlide);
        animator.SetBool("doubleJumping", doubleJumping);

        if (_onWall && extraJumps <= 0)
        {
            extraJumps = 1;
        }
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontalMove * runSpeed, rigidbody2D.velocity.y);
        
        //Wall Slide
        if (wallSlide)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -slidePower);
            spriteRenderer.flipX = onRightWall ? false : true;
            facingRight = onRightWall ? true : false;
        }

        //Jumping
        if (jumping)
        {
            particleDust.Play();
            rigidbody2D.velocity = Vector2.up * jumpForce;
            jumping = false;

            if (_onWall && !_onGround)
            {
                if (onRightWall) 
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x - 35, rigidbody2D.velocity.y);
                else if (onLeftWall)
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + 35, rigidbody2D.velocity.y);                
            }
        }

        //Fliping
        if ((horizontalMove > 0 && !facingRight) || (horizontalMove < 0 && facingRight) && !_onWall)
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = facingRight ? false : true;
            particleDust.Play();
        }
    }

}
