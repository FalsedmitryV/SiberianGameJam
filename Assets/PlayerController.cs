using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_NormalSpeed = 5f; 
    [SerializeField] private float m_AirSpeed = 5f; 
    [SerializeField] private float m_DashSpeed = 5f; 
    private float m_Speed;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    [SerializeField] private bool IsGrounded = false;
    [SerializeField] private bool IsDead = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Vector2 SpawPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private CinemachineCamera CinemaCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsDead = false;
        m_Speed = m_NormalSpeed;
        jumpForce = 14;
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.tag = "Player";
        StartCoroutine(WaitCamera());
    }

    void Update()
    {
        if (!IsDead)
        {
            if (IsGrounded)
            {
                m_Speed = m_NormalSpeed;
            }
            else
            {
                m_Speed = m_AirSpeed;
            }
            if (IsGrounded && Input.GetKeyDown(KeyCode.Space)) // Проверка на прыжок, только если игрок на земле
            {
                Debug.Log("ad");
                Jump();
            }
            Move();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Получаем горизонтальное движение (A/D или Left/Right Arrow)
                                                            // float moveVertical = Input.GetAxis("Vertical");     // Получаем вертикальное движение (W/S или Up/Down Arrow)

        if (moveHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (moveHorizontal < 0)
            spriteRenderer.flipX = true;


        Vector2 movement = new Vector2(moveHorizontal, 0);

   
        rb.AddForce(movement * m_Speed, ForceMode2D.Force);

    }
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            IsGrounded = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Прыгаем вверх
    }

    public void Dead()
    {
        if (!IsDead)
        {
            IsDead = true;
            m_Speed = 0;
            jumpForce = 0;
            rb.bodyType = RigidbodyType2D.Static;
            gameObject.tag = "Ground";
            CinemaCamera.Follow = Instantiate(Player, SpawPoint, Quaternion.identity).transform;
        }

    }
    IEnumerator WaitCamera()
    {
        while (true)
        {
            if (SingletonCamera.Instance != null)
            {
                CinemaCamera = SingletonCamera.Instance.GetComponent<CinemachineCamera>();
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
