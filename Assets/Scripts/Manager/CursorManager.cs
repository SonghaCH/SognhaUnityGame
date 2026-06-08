using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D idleCursor;   // 기본 커서
    public Texture2D clickCursor;  // 클릭 시 커서
    public Vector2 hotspot = new Vector2(16, 16); // 이미지 크기에 맞춰 조절하세요

    void Start()
    {
        // 게임 시작 시 기본 커서 적용
        Cursor.SetCursor(idleCursor, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 누르고 있는 동안
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
        }
        // 마우스 왼쪽 버튼을 뗐을 때
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(idleCursor, hotspot, CursorMode.Auto);
        }
    }
}