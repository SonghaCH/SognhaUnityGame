using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    // 날짜 및 시간 관리
    private int startMonth = 4;
    private int startDay = 7;
    public int DayCount { get; private set; } = 0;
    public int CurrentMinutes { get; private set; } = 480; // 08:00 시작

    private const int DAY_START_MINUTES = 480;
    private const int LIMIT_TIME_MINUTES = 1500; // 01:00

    // 자동 시간 흐름 설정
    [Header("자동 시간 설정")]
    [SerializeField] private float secondsPer10Minutes = 5.0f;
    private float timer = 0f;

    // GameManager와 연동되는 일시정지 상태
    public bool IsPaused { get; set; } = true;

    // 활동 횟수 관리
    private Dictionary<string, int> activityCounts = new Dictionary<string, int>();
    private const int MAX_DAILY_LIMIT = 2;

    private void Awake() => Instance = this;

    private void Update()
    {
        // GameManager에 의해 일시정지되었거나 1시가 넘으면 흐름 차단
        if (IsPaused || CurrentMinutes >= LIMIT_TIME_MINUTES) return;

        timer += Time.deltaTime;
        if (timer >= secondsPer10Minutes)
        {
            AddTime(10);
            timer = 0f;
        }
    }

    public void AddTime(int minutesToAdd)
    {
        CurrentMinutes += minutesToAdd;

        // 1시를 넘으면 날짜 전환
        if (CurrentMinutes >= LIMIT_TIME_MINUTES)
        {
            EndDay();
        }
        else
        {
            UpdateUI();
        }
    }

    public void EndDay()
    {
        DayCount++;
        CurrentMinutes = DAY_START_MINUTES;
        activityCounts.Clear();
        UpdateUI();
        Debug.Log($"[TimeManager] {DayCount + 1}일차 시작");
    }

    public bool CanDoActivity(string activityId)
    {
        if (CurrentMinutes >= LIMIT_TIME_MINUTES)
        {
            DaniTechUIManager.Instance.OpenBigPopupUI("새벽 1시가 넘어 활동할 수 없습니다.\n집으로 돌아가 취침하십시오.");
            return false;
        }

        int count = activityCounts.ContainsKey(activityId) ? activityCounts[activityId] : 0;
        return count < MAX_DAILY_LIMIT;
    }

    public void AddActivityCount(string activityId)
    {
        if (activityCounts.ContainsKey(activityId)) activityCounts[activityId]++;
        else activityCounts[activityId] = 1;
    }

    public void UpdateUI()
    {
        if (MainUI.Instance != null) MainUI.Instance.RefreshUI();
    }

    public string GetFormattedDate() => $"{startMonth}월 {startDay + DayCount}일";
    public string GetFormattedDay() => $"{DayCount + 1}일차";
    public string GetFormattedTime()
    {
        int hour = (CurrentMinutes / 60) % 24;
        int min = CurrentMinutes % 60;
        return $"{hour:D2}시 {min:D2}분";
    }
}