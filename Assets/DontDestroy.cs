using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Объект останется между сменами сцен
        }
    }
}
