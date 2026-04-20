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
        // Pobieramy obecną wielkość postaci (taką, jaką ustawiłeś w Inspektorze)
        Vector3 currentScale = transform.localScale;

        if (horizontalInput > 0)
        {
            // Ustawiamy X zawsze na wartość dodatnią (Mathf.Abs to wartość bezwzględna)
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else if (horizontalInput < 0)
        {
            // Ustawiamy X zawsze na wartość ujemną
            currentScale.x = -Mathf.Abs(currentScale.x);
        }

        // Przypisujemy zaktualizowaną skalę z powrotem do postaci
        transform.localScale = currentScale;
    }
}