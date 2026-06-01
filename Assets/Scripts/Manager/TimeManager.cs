using System;
using System.Collections.Generic; // Dictionary 사용을 위해 필수
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    // 시작 날짜 및 현재 상태 데이터
    private int startMonth = 4;
    private int startDay = 7;
    public int DayCount { get; private set; } = 0; // 0부터 시작 (1일차)
    public int CurrentMinutes { get; private set; } = 480; // 08시 00분 = 480분

    private const int DAY_START_MINUTES = 480;  // 08:00
    private const int DAY_END_MINUTES = 1500;   // 01:00 (다음날)

    // 행동 횟수 관리용 딕셔너리
    private Dictionary<string, int> activityCounts = new Dictionary<string, int>();
    private const int MAX_DAILY_LIMIT = 2; // 모든 활동 2회 제한

    private void Awake() => Instance = this;

    // --- 행동 횟수 관리 메서드들 ---

    // 특정 활동을 수행할 수 있는지 확인
    public bool CanDoActivity(string activityId)
    {
        int count = activityCounts.ContainsKey(activityId) ? activityCounts[activityId] : 0;
        return count < MAX_DAILY_LIMIT;
    }

    // 활동 수행 횟수 증가
    public void AddActivityCount(string activityId)
    {
        if (activityCounts.ContainsKey(activityId))
            activityCounts[activityId]++;
        else
            activityCounts[activityId] = 1;
    }

    // --- 시간 관리 메서드들 ---

    // 행동 실행 후 시간을 더함
    public void AddTime(int minutesToAdd)
    {
        CurrentMinutes += minutesToAdd;

        // 하루가 끝났는지 체크
        if (CurrentMinutes >= DAY_END_MINUTES)
        {
            EndDay();
        }

        UpdateUI();
    }

    private void EndDay()
    {
        DayCount++;
        CurrentMinutes = DAY_START_MINUTES; // 다음 날 08:00로 초기화

        // 하루가 끝날 때 활동 횟수 초기화
        activityCounts.Clear();

        Debug.Log($"{DayCount + 1}일차가 되었습니다. 모든 활동 횟수가 초기화됩니다.");
    }

    private void UpdateUI()
    {
        // MainUI가 존재할 때만 갱신
        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }
    }

    // 포맷팅 함수들
    public string GetFormattedDate() => $"{startMonth}월 {startDay + DayCount:D2}일";
    public string GetFormattedDay() => $"{DayCount + 1}일차";
    public string GetFormattedTime()
    {
        int hour = (CurrentMinutes / 60) % 24;
        int min = CurrentMinutes % 60;
        return $"{hour:D2}시 {min:D2}분";
    }
}