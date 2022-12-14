// this could so easily be one script but im lazy and want to release this

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    public bool playerOne;

    [HideInInspector]
    public float horizInput;
    [HideInInspector]
    public float horizInput2;

    private Rigidbody2D rb;
    private Rigidbody2D rb2;

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
    public bool toJump2;
    [HideInInspector]
    public int jumps;
    public int jumpCount;
    private int jumpBuffer;
    private int jumpBuffer2;
    public float jumpLength;
    [Range(0.0f, 1.0f)]
    public float secondJumpHeight;
    [HideInInspector]
    public float jumpTimer;
    [HideInInspector]
    public float jumpTimer2;
    public float jumpForce;

    public float speed;
    [Range(0.0f, 1.0f)]
    public float drag;
    private float pGravity;
    public bool isGrounded;
    private int coyoteTime;

    private AudioManager am;

    public bool jumpUnlocked;

    public Sprite p1Sprite;

    void Awake()
    {
        am = FindObjectOfType<AudioManager>();

        rb = GetComponent<Rigidbody2D>();
        pCollider = GetComponent<BoxCollider2D>();

        toJump = false;
        pGravity = rb.gravityScale;

        if (playerOne)
        {
            GetComponent<SpriteRenderer>().sprite = p1Sprite;
        }

    }

    // Update is called once per frame
    void Update()
    {
         GetInput();
    }

    private void FixedUpdate()
    {
        // ensure that each player only controls their character
        
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

    void GetInput()
    {
        horizInput = 0;
        if (playerOne)
        {
            // put the restart here so it doesn't double restart or something
            if (Input.GetKey("r"))
                FindObjectOfType<LevelLoader>().ReloadLevel();
            if (Input.GetKey("left")) horizInput -= 1;
            if (Input.GetKey("right")) horizInput += 1;

            if (Input.GetKeyDown("up"))
            {
                toJump = true;
                jumpBuffer = BUFFER_FRAMES;
            }
            if (Input.GetKeyUp("up"))
            {
                toJump = false;
                jumpTimer = 0;
            }
        }
        else
        {
            if (Input.GetKey("a")) horizInput -= 1;
            if (Input.GetKey("d")) horizInput += 1;
            if (Input.GetKeyDown("w"))
            {
                toJump = true;
                jumpBuffer = BUFFER_FRAMES;
            }
            if (Input.GetKeyUp("w"))
            {
                toJump = false;
                jumpTimer = 0;
            }
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
        if(am)
            am.Play("jump");
        jumpTimer = length;
        toJump = false;
        //if second jump, play other sound and play particle effect
        if (!jump1)
        {
            jumps--;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }



}
