using System.Collections;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private LevelsManager m_LevelsManager;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            source.clip = clip;
            source.Play();
            anim.SetBool("IsOpen", true);
            StartCoroutine(WaitAnim());
        }
    
    }

    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("IsOpen", false);
        yield return new WaitForSeconds(1.0f);
        m_LevelsManager.LoadLevel();
    }
}
