using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MindDialogueUI : DaniTechUIBase
{
    [SerializeField] private Text Text_Description;
    [SerializeField] private DaniTechUIButton Btn_Next;

    private string _currentDialogueId;

    // 타이핑 효과를 위한 변수들
    private Coroutine _typingCoroutine;
    private string _fullText;

    private void OnEnable()
    {
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    /// <summary>
    /// 내면 다이얼로그에서 Next 버튼이 눌러질 때 호출된다
    /// </summary>
    public void OnClick_Next()
    {
        // 1. 타이핑 중이면 스킵(전체 출력) 수행
        if (TrySkipTyping()) return;

        // 2. 타이핑 완료 시 다음 대사 요청
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);

        // 특정 종료 조건 체크
        if (_currentDialogueId == "mindDialogue_Opening_1_1_307_2(2)")
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// [매니저 연동 함수] 데이터를 주입받고 타이핑 효과를 시작합니다.
    /// </summary>
    public void SetupDialogue(string dialogueId, string description)
    {
        _currentDialogueId = dialogueId;
        _fullText = description;

        // 타이핑 시작
        StartTyping(description);
    }

    // --- 타이핑 효과 로직 시작 ---
    private void StartTyping(string text)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypingRoutine(text));
    }

    private IEnumerator TypingRoutine(string text)
    {
        Text_Description.text = "";
        foreach (char c in text)
        {
            Text_Description.text += c;
            int soundCounter = 0;
           
            if (c != ' ' && soundCounter % 5 == 0)
            {
                SoundManager.Instance.PlaySFX("Sound_Typing", 0.05f);
            }
            yield return new WaitForSeconds(0.07f); // 타이핑 속도
        }
        _typingCoroutine = null;
    }

    private bool TrySkipTyping()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
            Text_Description.text = _fullText; // 즉시 전체 출력
            return true;
        }
        return false;
    }
    // --- 타이핑 효과 로직 끝 ---
}