using UnityEngine;
using System.Collections; // 코루틴 사용을 위해 필요

public class EndingUI : DaniTechUIBase
{
    private void OnEnable()
    {
        // UI가 켜지자마자 5초 카운트다운 시작
        StartCoroutine(WaitAndShowQuitPopup());
    }

    private IEnumerator WaitAndShowQuitPopup()
    {
        // 5초 대기
        yield return new WaitForSeconds(2f);

        // 5초 후 실행할 로직
        // 예시: DaniTechUIManager를 통해 QuitPopupUI를 호출
        Application.Quit();

    }

    private void OnDisable()
    {
        // 혹시 UI가 5초 전에 꺼질 경우를 대비해 코루틴을 중지
        StopAllCoroutines();
    }
}