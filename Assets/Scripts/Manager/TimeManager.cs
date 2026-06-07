using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private const int MINUTES_IN_DAY = 1440;
    private const int DAY_START_MINUTES = 480;

    public int DayCount { get; private set; } = 0;
    public int CurrentMinutes { get; private set; } = 480;

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
        if (IsPaused) return;

        timer += Time.deltaTime;
        if (timer >= secondsPer10Minutes)
        {
            AddTime(10);
            timer = 0f;
        }
        
    }

    // [추가] 엔딩 체크 로직을 중앙화
    private void CheckEndingTrigger()
    {
        if (DayCount >= 3)
        {
            GameManager.Instance.EnterEndingScene();
        }
    }
    private void CheckSleepEvent()
    {
        // 예: 밤 10시(22시 = 1320분)가 되었을 때
        if (CurrentMinutes >= 1380 || CurrentMinutes < 480) // 10분 단위 업데이트 기준
        {
            DaniTechUIManager.Instance.OpenBigPopupUI("날이 어두워 졌네요! 1시가 지나면,집으로 들어가 취침하십쇼!");
            hasShownNightPopup = true;
        }
    }

    public void SkipToTime(int targetMinutes)
    {
        if (CurrentMinutes >= targetMinutes)
        {
            DayCount++;
        }

        CurrentMinutes = targetMinutes;
        activityCounts.Clear();

        // 엔딩 체크 추가
        CheckEndingTrigger();
        UpdateUI();
        CheckSleepEvent();
    }

    public void AddTime(int minutesToAdd)
    {
        int previousMinutes = CurrentMinutes;
        CurrentMinutes += minutesToAdd;

        if (previousMinutes < DAY_START_MINUTES && CurrentMinutes >= DAY_START_MINUTES)
        {
            activityCounts.Clear();
        }

        if (CurrentMinutes >= MINUTES_IN_DAY)
        {
            CurrentMinutes -= MINUTES_IN_DAY;
            DayCount++;

            // 엔딩 체크 추가
            CheckEndingTrigger();
        }
        UpdateUI();
        CheckSleepEvent();

    }

    public bool IsLateNight()
    {
        return (CurrentMinutes >= 60 && CurrentMinutes < 480);
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
        return "4월 " + (7 + DayCount) + "일";
    }

    public string GetFormattedDay()
    {
        return (DayCount + 1) + "일차";
    }

    public string GetFormattedTime()
    {
        int hour = (CurrentMinutes / 60) % 24;
        int min = CurrentMinutes % 60;
        return string.Format("{0:D2}시 {1:D2}분", hour, min);
    }
}