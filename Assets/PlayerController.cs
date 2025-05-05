using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Alive;
    [SerializeField] private GameObject m_Dead;
    [SerializeField] private float m_NormalSpeed = 5f; 
    [SerializeField] private float m_AirSpeed = 5f; 
    [SerializeField] private float m_DashSpeed = 5f; 
    private float m_Speed;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    [SerializeField] private bool IsGrounded = false;
    [SerializeField] private bool IsDead = false;
    [SerializeField] private bool IsMove ;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Vector2 SpawPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private CinemachineCamera CinemaCamera;
    [SerializeField] private Animator anim;
    [SerializeField]
    private float Side;

    [SerializeField] private AudioClip[] Clips;
     private AudioSource audioSource;

    Quaternion rot;
    [SerializeField] private float turnSpeed;

    public Vector2 GetSpawPoint  => SpawPoint;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsDead = false;
        m_Speed = m_NormalSpeed;
        jumpForce = 14;
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.tag = "Player";
        anim.enabled = true;
        anim.SetBool("Exit", false);
       // StartCoroutine(WaitCamera());
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

        if (IsGrounded && moveHorizontal != 0)
        {

                if (!IsMove)
                {
                    IsMove = true;
                    audioSource.clip = Clips[0];
                    audioSource.Play();
                    StartCoroutine(WaitClip());
                }

   
        }
        else
        {
            audioSource.Stop();
        }

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
            audioSource.Stop();
            anim.SetBool("Exit", true);
            gameObject.tag = "Ground";
            audioSource.clip = Clips[3];
            audioSource.Play();
            CinemaCamera.Follow = Instantiate(Player, SpawPoint, Quaternion.identity).transform;
            anim.enabled = false;
            Alive.gameObject.SetActive(false);
            m_Dead.gameObject.SetActive(true);
            gameObject.GetComponent<PlayerController>().enabled = false;
        }

    }
    IEnumerator WaitCamera()
    {
        while (true)
        {
            IsDead = true;
            if (SingletonCamera.Instance != null)
            {
                CinemaCamera = SingletonCamera.Instance.GetComponent<CinemachineCamera>();
                IsDead = false;
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
    IEnumerator WaitClip()
    {
        while (audioSource.isPlaying)
        {
            yield return null; // ∆дЄм одну итерацию кадрового обновлени€
        }
        IsMove = false;
    }
}
