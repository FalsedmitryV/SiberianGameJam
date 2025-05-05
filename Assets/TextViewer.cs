using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TextViewer : MonoBehaviour
{
    public TMP_Text textField; // Привяжи сюда Text Mesh Pro элемент
    public string fullText;    // Полный текст, который будет выводиться
    public float delayBetweenChars = 0.05f; // Задержка между символами
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
            textField.text += c.ToString(); // Печатаем символ
            yield return new WaitForSeconds(delayBetweenChars); // Пауза между символами
        }
    }
}
