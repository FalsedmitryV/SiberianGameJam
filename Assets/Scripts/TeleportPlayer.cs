using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.transform.position = collision.GetComponent<PlayerController>().GetSpawPoint;
        }
    }
}
