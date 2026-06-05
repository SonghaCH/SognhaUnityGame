using UnityEngine;
using UnityEngine.UI;

public class TimeLight : MonoBehaviour
{
    private Image overlayImage;

    [System.Serializable]
    public struct TimeColor
    {
        public string label; // 구분용 이름
        public int startMinutes; // 시작 분 (예: 08:00 = 480)
        public Color color;
    }

    [Header("시간별 색상 설정 (분 단위)")]
    public TimeColor[] timeColors = new TimeColor[]
    {
        new TimeColor { label = "깜깜한 밤", startMinutes = 1380 }, // 23:00 (1380분)
        new TimeColor { label = "일출", startMinutes = 240 },      // 04:00 (240분)
        new TimeColor { label = "밝은 노랑", startMinutes = 480 },  // 08:00 (480분)
        new TimeColor { label = "희미한 노랑", startMinutes = 780 }, // 13:00 (780분)
        new TimeColor { label = "노을", startMinutes = 1020 },     // 17:00 (1020분)
        new TimeColor { label = "저녁하늘", startMinutes = 1140 }   // 19:00 (1140분)
    };

    private void Awake() => overlayImage = GetComponent<Image>();

    private void Update()
    {
        if (TimeManager.Instance == null) return;

        // 하루가 1440분(24시간)이므로 1440으로 나눈 나머지를 사용해 순환시킴
        int currentMinutesInDay = TimeManager.Instance.CurrentMinutes % 1440;

        Color targetColor = GetTargetColor(currentMinutesInDay);
        overlayImage.color = Color.Lerp(overlayImage.color, targetColor, Time.deltaTime * 0.5f);
    }

    private Color GetTargetColor(int currentMinutes)
    {
        // 현재 시간보다 작거나 같은 시간대 중 가장 마지막 항목을 찾음
        Color target = timeColors[0].color;
        for (int i = 0; i < timeColors.Length; i++)
        {
            if (currentMinutes >= timeColors[i].startMinutes)
                target = timeColors[i].color;
        }
        return target;
    }
}