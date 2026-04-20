using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Parametry Ruchu")]
    public float speed = 3f;

    [Header("Punkty Patrolowe")]
    // Tablica przechowująca wszystkie punkty, między którymi będzie chodził przeciwnik
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Jeśli nie przypisano żadnych punktów w inspektorze, przerywamy działanie, żeby uniknąć błędów
        if (waypoints.Length == 0) return;

        // Cel, do którego aktualnie zmierzamy
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 1. Przesuwanie przeciwnika
        // Vector2.MoveTowards płynnie przesuwa obiekt z punktu A do punktu B z określoną prędkością
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // 2. Sprawdzanie, czy dotarliśmy do celu
        // Jeśli odległość między przeciwnikiem a punktem jest bardzo mała (np. mniejsza niż 0.1)
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Zmieniamy indeks na następny. 
            // Operator modulo (%) sprawia, że po dotarciu do ostatniego punktu, wracamy do pierwszego (indeks 0).
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // 3. Obracanie przeciwnika w stronę nowego celu
            FlipSprite(waypoints[currentWaypointIndex].position);
        }
    }

    void FlipSprite(Vector3 nextTargetPos)
    {
        // Jeśli następny punkt jest po prawej stronie, patrzymy w prawo (skala 1)
        if (nextTargetPos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Jeśli jest po lewej, patrzymy w lewo (skala -1)
        else if (nextTargetPos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}