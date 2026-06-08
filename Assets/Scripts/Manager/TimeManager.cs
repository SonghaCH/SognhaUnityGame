using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private const int MINUTES_IN_DAY = 1440;
    private const int DAY_START_MINUTES = 480;

    // 프로퍼티를 사용하지 않고 변수로 관리하는 방식을 유지합니다.
    private int dayCount = 0;
    public int DayCount { get { return dayCount; } }

    private int currentMinutes = 480;
    public int CurrentMinutes { get { return currentMinutes; } }

    [Header("자동 시간 설정")]
    [SerializeField] private float secondsPer10Minutes = 5.0f;
    private float timer = 0f;
    private bool hasShownNightPopup = false;
    public bool IsPaused { get; set; } = true;

    private Dictionary<string, int> activityCounts = new Dictionary<string, int>();
    private const int MAX_DAILY_LIMIT = 2;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //// [디버그 기능] K 키를 누르면 즉시 3일차로 이동하여 엔딩 테스트
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    ForceJumpToEnd();
        //}

        if (IsPaused) return;

        timer += Time.deltaTime;
        if (timer >= secondsPer10Minutes)
        {
            AddTime(10);
            timer = 0f;
        }
    }

    ////[추가] 엔딩 테스트용 강제 이동 메서드
    //public void ForceJumpToEnd()
    //{
    //    dayCount = 3;
    //    currentMinutes = 480;

    //    Debug.Log("디버그: 3일차로 강제 이동 및 엔딩 트리거 실행");

    //    CheckEndingTrigger();
    //    UpdateUI();
    //    CheckSleepEvent();
    //}

    // 엔딩 체크 로직 중앙화
    private void CheckEndingTrigger()
    {
        if (dayCount >= 3)
        {
            GameManager.Instance.EnterEndingScene();
        }
    }

    private void CheckSleepEvent()
    {
        if (hasShownNightPopup) return;

        // 밤 10시(22시 = 1320분)가 되었을 때 팝업
        if (currentMinutes >= 900 && currentMinutes < 1440)
        {
            DaniTechUIManager.Instance.OpenBigPopupUI("Tip: 1시가 지나면 집으로 들어가 취침하십쇼!");
            hasShownNightPopup = true;
        }
    }

    public void SkipToTime(int targetMinutes)
    {
        if (currentMinutes >= targetMinutes)
        {
            dayCount++;
        }

        currentMinutes = targetMinutes;
        activityCounts.Clear();

        CheckEndingTrigger();
        UpdateUI();
        CheckSleepEvent();
    }

    public void AddTime(int minutesToAdd)
    {
        int previousMinutes = currentMinutes;
        currentMinutes += minutesToAdd;

        // 새로운 날이 되었을 때 액티비티 제한 초기화
        if (previousMinutes < DAY_START_MINUTES && currentMinutes >= DAY_START_MINUTES)
        {
            activityCounts.Clear();
            hasShownNightPopup = false; // 날이 바뀌면 팝업 상태도 초기화
        }

        if (currentMinutes >= MINUTES_IN_DAY)
        {
            currentMinutes -= MINUTES_IN_DAY;
            dayCount++;

            CheckEndingTrigger();
        }
        UpdateUI();
        CheckSleepEvent();
    }

    public bool IsLateNight()
    {
        return (currentMinutes >= 60 && currentMinutes < 480);
    }

    public bool CanDoActivity(string activityId)
    {
        int count = 0;
        if (activityCounts.ContainsKey(activityId))
        {
            count = activityCounts[activityId];
        }
        return count < MAX_DAILY_LIMIT;
    }

    public void AddActivityCount(string activityId)
    {
        if (activityCounts.ContainsKey(activityId))
        {
            activityCounts[activityId]++;
        }
        else
        {
            activityCounts[activityId] = 1;
        }
    }

    public void UpdateUI()
    {
        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }
    }

    public string GetFormattedDate()
    {
        return "4월 " + (7 + dayCount) + "일";
    }

    public string GetFormattedDay()
    {
        return (dayCount + 1) + "일차";
    }

    public string GetFormattedTime()
    {
        int hour = (currentMinutes / 60) % 24;
        int min = currentMinutes % 60;
        return string.Format("{0:D2}시 {1:D2}분", hour, min);
    }
}