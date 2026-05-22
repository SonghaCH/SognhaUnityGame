using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDialogueUI : DaniTechUIBase
{
    [SerializeField] private GameObject Layout_CharacterName;
    [SerializeField] private Text Text_Character;
    [SerializeField] private Text Text_Description;
    [SerializeField] private DaniTechUIButton Btn_Next;

    private string _currentDialogueId;

    

    private void OnEnable()
    {
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    /// <summary>
    /// 다이얼로그에서 Next 버튼이 눌러질 때 호출된다
    /// </summary>
    public void OnClick_Next()
    {
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    /// <summary>
    /// [매니저 연동 함수] DialogueManager가 이 프리팹 UI를 켜면서 데이터를 심어줄 때 호출하는 함수
    /// </summary>
    public void SetupDialogue(string dialogueId, string description, string characterId)
    {
        _currentDialogueId = dialogueId;
        Text_Description.text = description;
        SetCharacterName(characterId);
    }

    

    /// <summary>
    /// 캐릭터 정보가 있다면 말하는 이의 이름을 표기해주는 함수
    /// </summary>
    private void SetCharacterName(string characterDataId)
    {
        bool isActive = (string.IsNullOrEmpty(characterDataId) == false);
        Layout_CharacterName.SetActive(isActive);

        if (isActive)
        {
            var characterData = GameDataManager.Instance.GetSHCharacterData(characterDataId);
            if (characterData != null)
            {
                Text_Character.text = characterData.Name;
            }
        }
    }

   
}