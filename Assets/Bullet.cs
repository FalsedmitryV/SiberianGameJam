using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private float m_DrstroyTimer;

    public Vector2 Direction { set { direction = value; } }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = direction;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Dead();
        }
        if(collision.tag != "Bullet")
        Destroy(gameObject);
    }
    
    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(m_DrstroyTimer);
        Destroy(gameObject);
    }
}
