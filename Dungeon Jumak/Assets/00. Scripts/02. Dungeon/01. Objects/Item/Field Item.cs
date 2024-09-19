// Engine
using UnityEngine;

// Ect
using Data.Object;

public class FieldItem : MonoBehaviour
{
    [SerializeField]
    private FieldItemData_Base m_data;

    // 아이템 정보 확인
    public void WatchItemInfo()
    {
        Debug.Log("필드 아이템 이름: " + m_data.Name);
    }

    // 플레이어가 아이템을 획득했을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
    }
}