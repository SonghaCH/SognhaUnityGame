using UnityEngine;
using UnityEngine.UI;

public class CUWorkingPopupUI : DaniTechUIBase
{
    [Header("Game Settings")]
    public GameObject[] itemPrefabs;
    public RectTransform[] spawnPoints;
    public RectTransform scanPointTransform;
    public float fallSpeed = 1000f; // 속도는 로직상 필요하니 유지

    [Header("UI")]
    public Text Text_CountNumber;

    // 헬스장 로직과 동일한 ID 및 변수 구조
    private const string ACTIVITY_ID = "activity_CUWork_01";
    private int successCount = 0;
    private int targetCount = 30; // 헬스장처럼 초기값 유지

    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        successCount = 0;
        UpdateUI();
        InvokeRepeating("SpawnItem", 1f, 0.8f);
    }

    void SpawnItem()
    {
        int lane = Random.Range(0, 4);
        GameObject item = Instantiate(itemPrefabs[lane], this.transform);
        RectTransform itemRect = item.GetComponent<RectTransform>();
        itemRect.localPosition = spawnPoints[lane].localPosition;

        Note note = item.GetComponent<Note>();
        if (note == null) note = item.AddComponent<Note>();
        note.speed = fallSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckScan();
        }
    }

    void CheckScan()
    {
        float scanY = scanPointTransform.localPosition.y;
        float threshold = 50f;

        foreach (Note note in GetComponentsInChildren<Note>())
        {
            float itemY = note.GetComponent<RectTransform>().localPosition.y;
            if (Mathf.Abs(itemY - scanY) < threshold)
            {
                successCount++;
                UpdateUI();
                Destroy(note.gameObject);

                if (successCount >= targetCount)
                {
                    FinishWorking();
                }
                return;
            }
        }
    }

    private void UpdateUI()
    {
        if (Text_CountNumber != null)
            Text_CountNumber.text = $"Count : {successCount} / {targetCount}";
    }

    // 헬스장의 FinishTraining과 동일한 구조의 마무리 로직
    private void FinishWorking()
    {
        CancelInvoke("SpawnItem");

        // 1. 데이터 매니저를 통해 엑셀 데이터 불러오기
        ActivityData data = GameDataManager.Instance.GetActivityData(ACTIVITY_ID);

        if (data != null)
        {
            // 2. 행동 횟수 증가
            TimeManager.Instance.AddActivityCount(ACTIVITY_ID);

            // 3. 엑셀 데이터의 스탯/돈 반영
            StatManager.Instance.ApplyActivityEffect(data);

            // 4. 데이터의 소요 시간 적용
            TimeManager.Instance.AddTime(data.TimeCost);

            Debug.Log($"{data.Name} 완료! 데이터 드리븐 적용 완료.");
        }

        // 아이템 정리 후 종료
        foreach (Note note in GetComponentsInChildren<Note>())
        {
            Destroy(note.gameObject);
        }

        DaniTechUIManager.Instance.OpenMiniPopupUI("알바 완료!");
        DaniTechUIManager.Instance.CloseCUWorkingPopupUI(); // 기존 헬스장처럼 팝업 닫기
    }
}