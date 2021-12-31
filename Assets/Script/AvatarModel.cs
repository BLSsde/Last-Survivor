using UnityEngine;

public class AvatarModel : MonoBehaviour
{
    [SerializeField] private Sprite[] playerModels;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        ChosePlayerModel(SaveGameData.instance.currentAvatar);

    }

    private void ChosePlayerModel(int _index)
    {
        _sprite.sprite = playerModels[_index];
        
    }
}