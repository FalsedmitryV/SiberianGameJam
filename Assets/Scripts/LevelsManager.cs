using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private int m_IndexLevel;

    public int GetIndexLevel  => m_IndexLevel;

    public void LoadLevel()
    {
        SceneManager.LoadScene(m_IndexLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
