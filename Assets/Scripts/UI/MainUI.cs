using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets; // 어드레서블 사용
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainUI : DaniTechUIBase
{
    // 다른 매니저에서 이 UI를 찾기 위한 싱글톤 인스턴스
    public static MainUI Instance { get; private set; }

    [SerializeField] private Image Image_Time;
    [SerializeField] private Text Text_Date;
    [SerializeField] private Text Text_Day;
    [SerializeField] private Text Text_Time;

    [SerializeField] private DaniTechUIButton Button_Map;
    [SerializeField] private DaniTechUIButton Button_Exit;
    [SerializeField] private DaniTechUIButton Button_Guide;

    [SerializeField] private Text Text_Health;
    [SerializeField] private Text Text_Int;
    [SerializeField] private Text Text_Charm;
    [SerializeField] private Text Text_Money;
    [SerializeField] private Text Text_Stress;

    // [추가] 상태 표시용 이미지 컴포넌트
    [SerializeField] private Image Image_CharacterState;

    [SerializeField] private string cityBgmName = "BGM_City";

    private string lastAppliedKey = "State_Normal";

    private void Awake()
    {
        // 인스턴스 등록
        Instance = this;
    }

    private void OnEnable()
    {
        Button_Map.BindOnClickButtonEvent(Onclick_Map);
        Button_Exit.BindOnClickButtonEvent(Onclick_Exit);
        Button_Guide.BindOnClickButtonEvent(Onclick_Guide);

        SoundManager.Instance.PlayBGM(cityBgmName, saveHistory: true, fadeDuration: 1.0f);

        // UI가 켜질 때 현재 데이터를 한번 반영
        RefreshUI();
    }
    private void OnDisable()
    {
        // [전문 포인트] 메인 화면을 나갈 때 음악 복구
        SoundManager.Instance.StopBGMAndRestore(fadeDuration: 1.0f);
    }
    // UI 갱신을 담당하는 통합 메서드
    public void RefreshUI()
    {
        // 시간 정보 갱신
        if (TimeManager.Instance != null)
        {
            Text_Date.text = TimeManager.Instance.GetFormattedDate();
            Text_Day.text = TimeManager.Instance.GetFormattedDay();
            Text_Time.text = TimeManager.Instance.GetFormattedTime();
        }

        // StatManager 데이터 연동
        if (StatManager.Instance != null)
        {
            Text_Health.text = $"{StatManager.Instance.Health:F0} / {StatManager.MAX_STAT:F0}";
            Text_Int.text = $"{StatManager.Instance.Intel:F0} / {StatManager.MAX_STAT:F0}";
            Text_Charm.text = $"{StatManager.Instance.Charm:F0} / {StatManager.MAX_STAT:F0}";
            Text_Stress.text = $"{StatManager.Instance.Stress:F0} / {StatManager.MAX_STAT:F0}";

            Text_Money.text = StatManager.Instance.Money.ToString("N0") + "원";
        }

        // [추가] 상태 이미지 갱신
        UpdateCharacterImage();
    }

    // 스탯 기반 이미지 업데이트
    private void UpdateCharacterImage()
    {
        if (StatManager.Instance == null) return;

        // 우선순위에 따른 상태 키 결정
        string assetKey = GetAssetKeyByStats();

        // 어드레서블로 이미지 로드
        Addressables.LoadAssetAsync<Sprite>(assetKey).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Image_CharacterState.sprite = handle.Result;
            }
            else
            {
                Debug.LogWarning($"이미지를 로드할 수 없습니다: {assetKey}");
            }
        };
    }

    // 스탯에 따른 이미지 키 반환 로직
    private string GetAssetKeyByStats()
    {
        StatManager stats = StatManager.Instance;
        string nextKey = "State_Normal";

        // 1. 상태 키 결정 (로직만 수행)
        if (lastAppliedKey == "State_Elite")
            nextKey = "State_Elite";


        else if (stats.Intel >= 100f && stats.Charm >= 90f && stats.Health >= 100f && stats.Money >= 400000)
            nextKey = "State_Elite";
        else if (stats.Charm >= 90f && stats.Money >= 300000)
            nextKey = "State_Charm";

        // 2. [핵심] 키가 바뀌었을 때만 팝업 실행!
        if (nextKey != lastAppliedKey)
        {
            lastAppliedKey = nextKey;
            DaniTechUIManager.Instance.OpenBigPopupUI("스탯 변동으로 인해 초상화가 변경되었습니다!");
            // 여기서 이미지를 갱신할 명령을 내림
            UpdateCharacterImage();
        }

        return nextKey;
    }

    private void Onclick_Map()
    {
        DaniTechUIManager.Instance.OpenMapPopupUI();
    }
    private void Onclick_Exit()
    {
        DaniTechUIManager.Instance.OpenQuitPopupUI();
    }
    private void Onclick_Guide()
    {
        DaniTechUIManager.Instance.OpenGameHelpGuidePopupUI();
    }
}