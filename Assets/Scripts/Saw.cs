using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotateSpeed = 100f; // Скорость вращения
    public List<GameObject> waypoints; // Список точек пути
    public float moveSpeed = 5f; // Скорость движения
    private int currentWaypointIndex = 0; // Текущий waypoint
    private void Awake()
    {
        // Если точек нет, ничего не делаем
        if (waypoints.Count == 0)
            return;

        // Первая точка маршрута
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }
    void Update()
    {
        // Вращаем объект вокруг оси Z (основа для 2D)
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // Если достигли конца маршрута, прекращаем выполнение
        if (currentWaypointIndex >= waypoints.Count)
        {
            // Возвращаемся к началу маршрута
            currentWaypointIndex = 0;
        }

        // Получаем следующую точку
        GameObject nextPoint = waypoints[currentWaypointIndex];

        // Перемещаемся к следующей точке
        transform.position = Vector3.MoveTowards(transform.position, nextPoint.transform.position, moveSpeed * Time.deltaTime);

        // Проверяем достижение точки
        if ((transform.position - nextPoint.transform.position).magnitude < 0.1f)
        {
            // Переходим к следующему waypoint'у
            currentWaypointIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Dead();
            moveSpeed = 0;
        }
    }
}
