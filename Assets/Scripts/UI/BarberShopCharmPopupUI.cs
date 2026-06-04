using System.Collections;
using UnityEngine;

public class BarberShopCharmPopupUI : DaniTechUIBase
{

    [Header("Settings")]
    private const string ACTIVITY_ID = "activity_BarerShop_01"; // 엑셀의 학원 ID

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationDuration = 4.0f; // 애니메이션 길이에 맞춰 조정하세요

    private void OnEnable()
    {
        // 팝업이 켜지자마자 애니메이션 재생 및 종료 로직 시작
        StartCoroutine(PlayAnimationAndFinish());
    }

    private IEnumerator PlayAnimationAndFinish()
    {
        // 1. 애니메이션 재생 (애니메이터에 설정된 상태명 입력)
        if (animator != null)
        {
            animator.Play("BaberANM_0"); // 현재 애니메이터 이름과 일치시키세요
        }

        // 2. 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(animationDuration);

        // 3. 헬스장 로직과 동일한 데이터 처리
        ProcessActivityData();
    }

    private void ProcessActivityData()
    {
        ActivityData data = GameDataManager.Instance.GetActivityData(ACTIVITY_ID);

        if (data != null)
        {
            // 데이터 매니저를 통한 스탯/시간/돈 반영
            TimeManager.Instance.AddActivityCount(ACTIVITY_ID);
            StatManager.Instance.ApplyActivityEffect(data);
            TimeManager.Instance.AddTime(data.TimeCost);

            Debug.Log($"{data.Name} 완료! 데이터 드리븐 적용 완료.");
        }
        else
        {
            Debug.LogError($"데이터를 찾을 수 없습니다: {ACTIVITY_ID}");
        }

        // 완료 후 팝업 종료
        DaniTechUIManager.Instance.OpenMiniPopupUI("자기 관리 완료!");
        DaniTechUIManager.Instance.CloseBaberShopCharmPopupUI();
    }
}
