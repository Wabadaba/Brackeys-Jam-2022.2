using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MovementController : MonoBehaviour
{

    [HideInInspector]
    public float vertInput;
    [HideInInspector]
    public float horizInput;

    private Rigidbody2D rb;

    PhotonView pview;

    // Movement Variables
    const int GROUNDING_RAYS = 2;
    const float GROUNDING_DISTANCE = 0.05f;
    const float MIN_SPEED = 0.1f;
    const int BUFFER_FRAMES = 5;
    const float DEADZONE = 0.1f;
    const float COOLDOWN_LENGTH = 0.35f;
    const float WALL_RAY_LEN = 0.05f;
    const int COYOTE_FRAMES = 5;
    const float respawnTime = 0.5f;

    // for use in death
    public bool frozen;

    //public CheckpointHandler lastCP;
    public Vector2 lastCPPos;

    private BoxCollider2D pCollider;
    public LayerMask groundLayers;

    [HideInInspector]
    public bool toJump;
    [HideInInspector]
    public int jumps;
    public int jumpCount;
    private int jumpBuffer;
    public float jumpLength;
    [Range(0.0f, 1.0f)]
    public float secondJumpHeight;
    [HideInInspector]
    public float jumpTimer;
    public float jumpForce;

    public float speed;
    [Range(0.0f, 1.0f)]
    public float drag;
    private float pGravity;
    public bool isGrounded;
    private int coyoteTime;


    public bool jumpUnlocked;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        pview = GetComponent<PhotonView>();
        if (pview.IsMine)
        {
            rb = GetComponent<Rigidbody2D>();
            pCollider = GetComponent<BoxCollider2D>();

            toJump = false;
            pGravity = rb.gravityScale;
        }

        //attaches components to variables

    }

    // Update is called once per frame
    void Update()
    {
        if (pview.IsMine)
            GetInput();
    }

    private void FixedUpdate()
    {
        // ensure that each player only controls their character
        if (pview.IsMine)
        {
            //decrement jump buffer
            if (jumpBuffer > 0) jumpBuffer--;
            else if (jumpTimer == 0) toJump = false;

            if (coyoteTime > 0) coyoteTime--;//decrement
            else isGrounded = false;//if coyoteTime == 0 you are not grounded

            //while on ground
            if (CheckGrounded() != null)
            {
                coyoteTime = COYOTE_FRAMES;
                isGrounded = true;
                jumps = jumpCount - 1;
            }



            if (frozen)
            {
                jumpTimer = 0;
                rb.gravityScale = pGravity;
            }

            //move the character (if axis is out of DEADZONE)
            if (Mathf.Abs(horizInput) > DEADZONE)
            {
                rb.AddForce(new Vector2(horizInput * speed, rb.velocity.y));
            }
            //apply drag
            if (!frozen) rb.velocity = new Vector2(rb.velocity.x * drag, rb.velocity.y);
            //if speed is below MIN_SPEED,
            if (horizInput == 0 && Mathf.Abs(rb.velocity.x) < MIN_SPEED)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }



            //check to see if you can jump. If so, start Jump.
            if (toJump)
            {
                if (isGrounded)
                {
                    StartJump(jumpLength, true);
                }
                else if (jumpUnlocked && jumps > 0)
                {
                    StartJump(jumpLength * secondJumpHeight, false);
                }
            }
            ApplyJump(); //only does something if jumpTimer > 0
        }
    }

    void GetInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && !frozen)
        {
            toJump = true;
            jumpBuffer = BUFFER_FRAMES;
        }
        if (Input.GetButtonUp("Jump"))
        {
            toJump = false;
            jumpTimer = 0;
        }
    }

    public GameObject CheckGrounded()
    {
        RaycastHit2D hit;

        float castPosX = transform.position.x - (pCollider.size.x * transform.localScale.x / 2);
        float castPosY = transform.position.y + pCollider.offset.y * transform.localScale.y - (pCollider.size.y * transform.localScale.y / 2);
        float castDifference = pCollider.size.x * transform.localScale.x / GROUNDING_RAYS;
        for (int i = 0; i <= GROUNDING_RAYS; i++)
        {
            hit = Physics2D.Raycast(new Vector2(castPosX, castPosY), Vector2.down, GROUNDING_DISTANCE, groundLayers);
            Debug.DrawRay(new Vector2(castPosX, castPosY), Vector2.down * GROUNDING_DISTANCE, Color.green);
            if (hit == true)
            {
                return hit.transform.gameObject;
            }
            castPosX += castDifference;
        }
        return null;
    }

    public void ApplyJump()
    {
        //jump stuff
        //if jump bonks ceiling, cancel jump
        if (rb.velocity.y < 1) jumpTimer = 0;
        //while jump timer is above zero, apply jump force
        else if (jumpTimer > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer -= Time.fixedDeltaTime;
        }
    }

    private void StartJump(float length, bool jump1)
    {
        jumpTimer = length;
        toJump = false;
        //if second jump, play other sound and play particle effect
        if (!jump1)
        {
            jumps--;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Freeze(bool enableFreeze)
    {
        if (enableFreeze)
        {
            frozen = true;
            toJump = false;
            jumpTimer = 0;
        }
        else
        {
            frozen = false;
        }
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
