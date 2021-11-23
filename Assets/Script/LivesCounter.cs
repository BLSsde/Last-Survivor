using UnityEngine.UI;
using UnityEngine;

//[RequireComponent(typeof(Text))]
public class LivesCounter : MonoBehaviour
{
    private Text livesText;
    private void Awake()
    {
        livesText = GetComponent<Text>();
    }

    void Update()
    {
        livesText.text = "Lives: " + GameManager.Remaining_Lives.ToString();
    }
}
