using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DateDialogueUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_NextButton;
    [SerializeField] private Text Text_Description;

    private string _currentDialogueId;
    private Coroutine _typingCoroutine;
    private string _fullText;

    private void OnEnable()
    {
        Btn_NextButton.BindOnClickButtonEvent(Onclick_NextButton);
    }

    public void Onclick_NextButton()
    {
        if (TrySkipTyping()) return;
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    public void SetupDialogue(string dialogueId, string description)
    {
        _currentDialogueId = dialogueId;
        _fullText = description;

        // [★추가] 대화 ID를 분석하여 BGM 재생
        PlayBGMByDialogueId(dialogueId);

        StartTyping(description);
    }

    // 대화 ID에 따라 BGM을 결정하고 재생하는 함수
    private void PlayBGMByDialogueId(string dialogueId)
    {
        string bgmName = "";

        // 대화 ID에 포함된 단어를 기준으로 BGM 선택
        if (dialogueId.Contains("dateDialogue_Opening_1_1_100"))
        {
            bgmName = "BGM_Opening";
        }
        
        // BGM이 결정되었다면 재생
        if (!string.IsNullOrEmpty(bgmName))
        {
            SoundManager.Instance.PlayBGM(bgmName, saveHistory: true, fadeDuration: 1.0f);
        }
    }

    private void StartTyping(string text)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypingRoutine(text));
    }

    private IEnumerator TypingRoutine(string text)
    {
        Text_Description.text = "";
        int soundCounter = 0;

        foreach (char c in text)
        {
            Text_Description.text += c;

            if (c != ' ' && soundCounter % 1 == 0)
            {
                SoundManager.Instance.PlaySFX("Sound_Typing", 0.3f);
            }

            soundCounter++;
            yield return new WaitForSeconds(0.15f);
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
}