using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Parametry Życia")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Niewrażliwość (I-frames)")]
    public float invincibilityDuration = 1.5f;
    private bool isInvincible = false;

    [Header("Efekty Wizualne")]
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    private Color originalColor;

    [Header("Zdarzenia (Events)")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;

    void Start()
    {
        // Ustawienie pełnego zdrowia na start
        currentHealth = maxHealth;

        // Pobranie komponentu odpowiedzialnego za wyświetlanie grafiki
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Funkcja wywoływana z zewnątrz (np. przez kolce lub pocisk), aby zadać obrażenia
    public void TakeDamage(int damageAmount)
    {
        // Ignoruj obrażenia, jeśli obiekt jest już martwy lub aktualnie niewrażliwy
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damageAmount;

        // Wywołanie wszystkich podpiętych w edytorze akcji (np. dźwięk uderzenia)
        OnTakeDamage.Invoke();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            // Uruchomienie licznika czasu niewrażliwości, jeśli postać przeżyła
            StartCoroutine(InvincibilityRoutine());
        }
    }

    // Funkcja do leczenia (np. po zebraniu apteczki)
    public void Heal(int healAmount)
    {
        if (currentHealth <= 0) return;

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Korutyna zarządzająca czasem niewrażliwości i efektem wizualnym
    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        // Błysk na wybrany kolor (np. czerwony)
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = originalColor;
        }

        // Odczekanie reszty czasu niewrażliwości
        yield return new WaitForSeconds(invincibilityDuration - 0.15f);

        isInvincible = false;
    }

    private void Die()
    {
        // Wywołanie akcji śmierci (np. odtworzenie animacji wybuchu)
        OnDie.Invoke();

        // Domyślne zniszczenie obiektu z gry
        Destroy(gameObject);
    }
}