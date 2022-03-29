using UnityEngine.UI;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    private Text livesText;
    private void Awake()
    {
        livesText = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        livesText.text = GameManager.Remaining_Lives.ToString();
    }
}