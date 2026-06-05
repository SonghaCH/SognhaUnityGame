using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDialogueUI : DaniTechUIBase
{
    [SerializeField] private GameObject Layout_CharacterName;
    [SerializeField] private Text Text_Character;
    [SerializeField] private Text Text_Description;
    [SerializeField] private DaniTechUIButton Btn_Next;
    [SerializeField] private Image Image_Color;

    private string _currentDialogueId;

    private void OnEnable()
    {
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    public void OnClick_Next()
    {
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    public void SetupDialogue(string dialogueId, string description, string characterId)
    {
        _currentDialogueId = dialogueId;
        Text_Description.text = description;
        SetCharacterName(characterId);
    }

    private void SetCharacterName(string characterDataId)
    {
        bool isActive = !string.IsNullOrEmpty(characterDataId);
        Layout_CharacterName.SetActive(isActive);

        if (isActive)
        {
            var characterData = GameDataManager.Instance.GetSHCharacterData(characterDataId);
            if (characterData != null)
            {
                // 1. 이름 텍스트 설정
                Text_Character.text = characterData.Name;
                // 2. 이름 텍스트는 무조건 검은색 고정
                Text_Character.color = Color.black;

                // 3. 이미지 색상만 구분하여 적용
                if (Image_Color != null)
                {
                    if (characterDataId == "character_Player_01")
                    {
                        Image_Color.color = Color.cyan; // 주인공: 하늘색 이미지
                    }
                    else
                    {
                        Image_Color.color = Color.yellow; // 상대방: 노란색 이미지
                    }
                }
            }
        }
    }
}