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
    [SerializeField] private Animator anim;
    [SerializeField]
    private float Side;

    Quaternion rot;
    [SerializeField] private float turnSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsDead = false;
        m_Speed = m_NormalSpeed;
        jumpForce = 14;
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.tag = "Player";
        anim.SetBool("Exit", false);
        StartCoroutine(WaitCamera());
    }

    void Update()
    {
        if (!IsDead)
        { 
            moveHorizontal = Input.GetAxis("Horizontal");
            Move();
            Vector2 move = new Vector2(moveHorizontal * m_NormalSpeed, rb.linearVelocityY);
            rb.linearVelocity = move;
            if (IsGrounded && Input.GetKeyDown(KeyCode.Space)) 
            {
                Jump();
            }
            
        }
    }
    [SerializeField] private float moveHorizontal;
    [SerializeField] private bool jump;

    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal"); 

        transform.rotation = Quaternion.Euler(0, 180, 0);


        if (moveHorizontal < 0)
            rot = Quaternion.Euler(0, 180, 0);
        else if (moveHorizontal > 0)
            rot = Quaternion.Euler(0, 0, 0);

        transform.rotation = rot;

        Vector2 movement = new Vector2(moveHorizontal, 0);                                            

        if (moveHorizontal != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            transform.rotation = rot;
            anim.SetBool("IsMoving", false);
        }
    }
    private void FixedUpdate()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
            jump = false;
        }
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
        Vector2 move = new Vector2(rb.linearVelocityX, jumpForce);
        rb.linearVelocity = move;
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
            anim.SetBool("Exit", true);
            CinemaCamera.Follow = Instantiate(Player, SpawPoint, Quaternion.identity).transform;
            gameObject.GetComponent<PlayerController>().enabled = false;
            anim.enabled = false;
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

    protected IEnumerator WaitForAnimation(string animation)
    {
        yield return new WaitForEndOfFrame();
        anim.SetBool(animation, false);
    }
}
