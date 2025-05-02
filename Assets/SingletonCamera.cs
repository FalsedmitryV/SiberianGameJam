using UnityEngine;

public class SingletonCamera : MonoBehaviour
{
    public static SingletonCamera Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Попытка создать дубликат PlayerController. Игнорируем.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
