using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1;

    // Funkcja wbudowana w Unity - odpala się przy zderzeniu dwóch obiektów z Colliderem
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Szukamy, czy obiekt z którym się zderzyliśmy ma nasz komponent HealthSystem
        HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();

        if (health != null)
        {
            // Jeśli ma, wywołujemy na nim funkcję TakeDamage z naszego głównego skryptu
            health.TakeDamage(damage);
        }
    }
}