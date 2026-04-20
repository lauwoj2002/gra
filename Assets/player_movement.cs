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
    private float horizontalInput;
    private bool isGrounded;

    void Awake()
    {
        // Pobieramy komponent Rigidbody2D z obiektu gracza
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Pobieranie danych od gracza (A/D lub strzałki)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Skakanie
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 3. Obracanie postaci w stronę ruchu
        FlipSprite();
    }

    void FixedUpdate()
    {
        // Sprawdzanie czy gracz dotyka ziemi (fizyka najlepiej działa w FixedUpdate)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Nakładanie prędkości na oś X
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}