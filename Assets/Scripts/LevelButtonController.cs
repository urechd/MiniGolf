using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private Sprite _unlockedSprite;
    [SerializeField] private Sprite _lockedSprite;

    [SerializeField] private int _levelNumber;

    void Start()
    {
        Image image = GetComponent<Image>();
        Button button = GetComponent<Button>();

        if (GameState.IsLevelComplete(_levelNumber - 1) || _levelNumber == 1)
        {
            image.sprite = _unlockedSprite;
            button.enabled = true;
        }
        else
        {
            image.sprite = _lockedSprite;
            button.enabled = false;
        }
    }
}
