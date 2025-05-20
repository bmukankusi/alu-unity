using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float fallThreshold = -30f;

    // Audio variables
    public AudioSource grassFootsteps;
    public AudioSource rockFootsteps;
    public AudioSource grassLanding;
    public AudioSource rockLanding;
    public AudioSource backgroundMusic;
    public AudioSource victorySound;
    [SerializeField] private string currentGroundType = "grass";

    private Rigidbody rb;
    private Animator animator;
    private Vector3 startPosition;
    private bool isGrounded;
    private bool wasFalling;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    private void Update()
    {
        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        bool isMoving = moveDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Handle footstep sounds
        if (isMoving && isGrounded)
        {
            PlayFootsteps();
        }
        else
        {
            StopFootsteps();
        }

        // Jump
        isGrounded = IsGrounded();
        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            animator.SetTrigger("jump");
            StopFootsteps(); // Stop footsteps when jumping
        }

        // Landing detection
        if (isGrounded && wasFalling)
        {
            PlayLandingSound();
        }
        wasFalling = !isGrounded;

        // Reset if player falls
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void PlayFootsteps()
    {
        if (currentGroundType == "grass")
        {
            if (!grassFootsteps.isPlaying) grassFootsteps.Play();
            rockFootsteps.Stop();
        }
        else
        {
            if (!rockFootsteps.isPlaying) rockFootsteps.Play();
            grassFootsteps.Stop();
        }
    }

    private void StopFootsteps()
    {
        grassFootsteps.Stop();
        rockFootsteps.Stop();
    }

    private void PlayLandingSound()
    {
        if (currentGroundType == "grass")
        {
            grassLanding.Play();
        }
        else
        {
            rockLanding.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flag"))
        {
            backgroundMusic.Stop();
            victorySound.Play();
            //Stops the timer
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StopTimer();
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detect ground type
        if (collision.gameObject.CompareTag("Grass"))
        {
            currentGroundType = "grass";
        }
        else if (collision.gameObject.CompareTag("Stone"))
        {
            currentGroundType = "rock";
        }
    }

    private void Respawn()
    {
        Debug.Log("Player fell! Respawning...");
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
    }

    public void AnimationFootstepEvent()
    {
        if (isGrounded && isMoving)
        {
            PlayFootsteps();
        }
    }
}