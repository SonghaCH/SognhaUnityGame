using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    // 날짜 관리
    private int startMonth = 4;
    private int startDay = 7;
    public int DayCount { get; private set; } = 0; // 0부터 시작 (1일차)

    // 시간 관리 (480 = 08:00, 1500 = 01:00 다음날)
    public int CurrentMinutes { get; private set; } = 480;

    private const int DAY_START_MINUTES = 480;   // 08:00
    private const int LIMIT_TIME_MINUTES = 1500;  // 01:00 (활동 제한 시간)

    // 활동 횟수 관리
    private Dictionary<string, int> activityCounts = new Dictionary<string, int>();
    private const int MAX_DAILY_LIMIT = 2;

    private void Awake() => Instance = this;

    // --- 행동 횟수 및 시간 제한 체크 ---
    public bool CanDoActivity(string activityId)
    {
        // 1. 시간 제한 체크 (새벽 1시 이후 활동 불가)
        if (CurrentMinutes >= LIMIT_TIME_MINUTES)
        {
            Debug.Log("새벽 1시가 넘어 활동할 수 없습니다.");
            DaniTechUIManager.Instance.OpenBigiPopupUI("새벽 1시가 넘어 활동할 수 없습니다./n집으로 돌아가 취침하십시오.");

            return false;
        }

        // 2. 횟수 제한 체크
        int count = activityCounts.ContainsKey(activityId) ? activityCounts[activityId] : 0;
        return count < MAX_DAILY_LIMIT;
    }

    public void AddActivityCount(string activityId)
    {
        if (activityCounts.ContainsKey(activityId))
            activityCounts[activityId]++;
        else
            activityCounts[activityId] = 1;
    }

    // --- 시간 관리 메서드들 ---

    // 시간이 흐르게 하는 핵심 함수
    public void AddTime(int minutesToAdd)
    {
        CurrentMinutes += minutesToAdd;
        UpdateUI();
    }

    // 하루를 마감하고 초기화
    public void EndDay()
    {
        DayCount++;
        CurrentMinutes = DAY_START_MINUTES;
        activityCounts.Clear(); // 활동 횟수 초기화

        Debug.Log($"{DayCount + 1}일차가 되었습니다.");
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (MainUI.Instance != null)
            MainUI.Instance.RefreshUI();
    }

    // --- UI 표기용 메서드들 ---
    public string GetFormattedDate() => $"{startMonth}월 {startDay + DayCount:D2}일";

    public string GetFormattedDay() => $"{DayCount + 1}일차";

    public string GetFormattedTime()
    {
        int hour = (CurrentMinutes / 60) % 24;
        int min = CurrentMinutes % 60;
        return $"{hour:D2}시 {min:D2}분";
    }
}