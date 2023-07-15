using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    private void Start()
    {
        Button startButton = GetComponent<Button>();

        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
