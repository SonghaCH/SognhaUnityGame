using System.Collections;
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
    private string _currentCharacterId; // 캐릭터 ID 추적용 변수

    private Coroutine _typingCoroutine;
    private string _fullText;

    private void OnEnable()
    {
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    public void OnClick_Next()
    {
        if (TrySkipTyping()) return;
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    public void SetupDialogue(string dialogueId, string description, string characterId)
    {
        _currentDialogueId = dialogueId;
        _fullText = description;
        _currentCharacterId = characterId; // 대화 시작 시 ID 저장

        SetCharacterName(characterId);
        StartTyping(description);
    }

    private void StartTyping(string text)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypingRoutine(text));
    }

    private IEnumerator TypingRoutine(string text)
    {
        Text_Description.text = "";
        int soundCounter = 0; // 효과음 빈도 조절용

        foreach (char c in text)
        {
            Text_Description.text += c;

            // [사운드 로직] 공백이 아니고, 2글자당 1번씩 사운드 호출
            if (c != ' ' && soundCounter % 2 == 0)
            {
                // 캐릭터 ID를 결합하여 "Typing_캐릭터ID" 파일 호출
                SoundManager.Instance.PlaySFX("Typing_" + _currentCharacterId, 0.15f);
            }

            soundCounter++;
            yield return new WaitForSeconds(0.03f);
        }
        _typingCoroutine = null;
    }

    private bool TrySkipTyping()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
            Text_Description.text = _fullText;
            return true;
        }
        return false;
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
                Text_Character.text = characterData.Name;
                Text_Character.color = Color.black;

                if (Image_Color != null)
                {
                    Image_Color.color = (characterDataId == "character_Player_01") ? Color.cyan : Color.yellow;
                }
            }
        }
    }
}