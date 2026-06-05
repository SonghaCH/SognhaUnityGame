using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BigPopupUI : DaniTechUIBase
{
    [SerializeField] private Text Text_Message; // 인스펙터에서 텍스트 컴포넌트 연결
    [SerializeField] private float displayDuration = 3.0f; // 사라지는 시간

    // 팝업을 띄울 때 메시지를 변경하고 타이머를 시작함
    public void SetMessage(string message)
    {
        Text_Message.text = message;

        // 이전에 돌던 타이머가 있다면 멈추고 새로 시작 (안전장치)
        StopAllCoroutines();
        StartCoroutine(AutoCloseRoutine());
    }

    private IEnumerator AutoCloseRoutine()
    {
        yield return new WaitForSeconds(displayDuration);

        // DaniTechUIManager를 통해 팝업 닫기
        DaniTechUIManager.Instance.CloseBigPopupUI();
    }
}