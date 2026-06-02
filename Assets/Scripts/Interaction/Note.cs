using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 1000f;

    void Update()
    {
        RectTransform rect = GetComponent<RectTransform>();
        // 아래로 이동
        rect.localPosition += Vector3.down * speed * Time.deltaTime;

        // 화면 밖으로 나가면 삭제
        if (rect.localPosition.y < -800)
        {
            Destroy(gameObject);
        }
    }
}