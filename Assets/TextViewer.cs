using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TextViewer : MonoBehaviour
{
    public TMP_Text textField; // ������� ���� Text Mesh Pro �������
    public string fullText;    // ������ �����, ������� ����� ����������
    public float delayBetweenChars = 0.05f; // �������� ����� ���������
    private bool IsTextWritting;
    [SerializeField] private LevelsManager levelManager;

    void Awake()
    {
        IsTextWritting = true;
        StartCoroutine(TypeText(fullText));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsTextWritting)
            {
                IsTextWritting = false;
                StopAllCoroutines();
                textField.text = fullText;
            }
            else SceneManager.LoadScene(levelManager.GetIndexLevel);
        }
    }

    IEnumerator TypeText(string text)
    {
        foreach (char c in text)
        {
            textField.text += c.ToString(); // �������� ������
            yield return new WaitForSeconds(delayBetweenChars); // ����� ����� ���������
        }
    }
}
