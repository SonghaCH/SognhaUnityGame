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

    public void SkipToTime(int targetMinutes)
    {
        // 8시(480분)를 넘기는 시점이라면 날짜를 하루 증가
        if (CurrentMinutes >= targetMinutes)
        {
            DayCount++;
        }

        CurrentMinutes = targetMinutes;

        // 8시가 되었으므로 하루 활동 횟수 초기화
        // (이전 로직에서 8시 체크를 이미 구현해두셨다면 유지해주시면 됩니다)
        activityCounts.Clear();

        UpdateUI();
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
        }
        UpdateUI();
    }

    public bool IsLateNight()
    {
        if (CurrentMinutes >= 60 && CurrentMinutes < 480)
        {
            return true;
        }
        return false;
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