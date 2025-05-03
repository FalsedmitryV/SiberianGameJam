using System.Collections;
using UnityEngine;

public class FireStation : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float delay;
    [SerializeField] private Vector2 direction;

    private void Awake()
    {
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        while (true)
        {
            Bullet.GetComponent<Bullet>().Direction = direction;
            Quaternion rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);
            Instantiate(Bullet, transform.position, rotation);
            
            yield return new WaitForSeconds(delay);
        }
        
    }
}
