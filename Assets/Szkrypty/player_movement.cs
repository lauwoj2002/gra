using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parametry Ruchu")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Weryfikacja Podłoża")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private float horizontalInput;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Pobieranie danych od gracza
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Skakanie
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 3. Obracanie postaci
        FlipSprite();

        // 4. Obsługa animacji ruchu
        bool isMoving = horizontalInput != 0;
        anim.SetBool("isRunning", isMoving);

        // 5. Obsługa animacji skoku (NOWE)
        // Jeśli isGrounded jest fałszywe (czyli postać NIE dotyka ziemi), isJumping będzie prawdziwe.
        anim.SetBool("isJumping", !isGrounded);
    }

    void FixedUpdate()
    {
        // Sprawdzanie czy gracz dotyka ziemi
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Nakładanie prędkości na oś X
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        Vector3 currentScale = transform.localScale;

        if (horizontalInput > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else if (horizontalInput < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }

        transform.localScale = currentScale;
    }
}