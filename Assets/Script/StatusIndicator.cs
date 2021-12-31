using UnityEngine;
using UnityEngine.UI;
public class StatusIndicator : MonoBehaviour
{
    
    [SerializeField] private RectTransform healthBarRect; 
    [SerializeField] private Text healthText;
    

    public void SetHealth( int Curr, int Max)
    {
        float value = (float)Curr / Max;
        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = Curr + "/" + Max + " HP";
    }

}
