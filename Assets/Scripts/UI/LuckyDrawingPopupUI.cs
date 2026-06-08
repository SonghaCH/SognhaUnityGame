using UnityEngine;
using System.Collections; // 코루틴 사용을 위해 필수!

public class LuckyDrawingPopupUI : DaniTechUIBase
{
    [SerializeField] private float animationDuration = 2.0f; // 애니메이션 길이에 맞춰서 조절하세요!
    [SerializeField] private string openSoundName = "SFX_LuckyDrow";

    
    private void OnEnable()
    {
        Debug.Log("[LuckyDrawingPopupUI] 팝업 열림, 소리 재생 시도: " + openSoundName);
        SoundManager.Instance.PlaySFX(openSoundName, 0.2f);
        // 팝업이 켜지자마자 애니메이션 시간만큼 대기 시작
        StartCoroutine(WaitAndProcess());
       

    }

    private IEnumerator WaitAndProcess()
    {
        yield return new WaitForSeconds(animationDuration);

        // 횟수 체크를 한번 더 거친 뒤 로직 실행 (안전장치)
        if (TimeManager.Instance.CanDoActivity("activity_LuckyDraw_01"))
        {
            ActivityManager.Instance.ExecuteActivity("activity_LuckyDraw_01");
        }

        DaniTechUIManager.Instance.CloseLuckyDrawingPopupUI();
    }

    private void Onclick_Close()
    {
        // 닫기 버튼 누르면 코루틴도 멈춰야 안전합니다.
        StopAllCoroutines();
        DaniTechUIManager.Instance.CloseLuckyDrawingPopupUI();
    }
}