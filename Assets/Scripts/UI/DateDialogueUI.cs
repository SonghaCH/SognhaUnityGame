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
        int soundCounter = 0; // 소리 빈도 조절용 카운터

        foreach (char c in text)
        {
            Text_Description.text += c;

            // 공백이 아니고, 2글자마다 한 번씩 효과음 재생
            if (c != ' ' && soundCounter % 1 == 0)
            {
                // SFX 폴더의 "Typing" 파일을 볼륨 0.2로 재생
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