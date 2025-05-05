using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotateSpeed = 100f; // �������� ��������
    public List<GameObject> waypoints; // ������ ����� ����
    public float moveSpeed = 5f; // �������� ��������
    private int currentWaypointIndex = 0; // ������� waypoint
    private void Awake()
    {
        // ���� ����� ���, ������ �� ������
        if (waypoints.Count == 0)
            return;

        // ������ ����� ��������
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }
    void Update()
    {
        // ������� ������ ������ ��� Z (������ ��� 2D)
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // ���� �������� ����� ��������, ���������� ����������
        if (currentWaypointIndex >= waypoints.Count)
        {
            // ������������ � ������ ��������
            currentWaypointIndex = 0;
        }

        // �������� ��������� �����
        GameObject nextPoint = waypoints[currentWaypointIndex];

        // ������������ � ��������� �����
        transform.position = Vector3.MoveTowards(transform.position, nextPoint.transform.position, moveSpeed * Time.deltaTime);

        // ��������� ���������� �����
        if ((transform.position - nextPoint.transform.position).magnitude < 0.1f)
        {
            // ��������� � ���������� waypoint'�
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
