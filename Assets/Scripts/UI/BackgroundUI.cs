using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : DaniTechUIBase
{
    [SerializeField] private Image Image_Background;

    private void OnEnable()
    {
        DialogueManager.Instance._OnChangeBackgroundImage += ChangeBackgroundImage;
    }

    private void ChangeBackgroundImage(Sprite sprite)
    {
        Image_Background.sprite = sprite;
    }
}
