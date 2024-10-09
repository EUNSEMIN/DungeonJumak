// Engine
using UnityEngine;

// Ect
using Data.Object;

public class Skill : MonoBehaviour
{
    #region Variables

    public SkillDataSO data; // SO

    [Header("던전 플레이어")]
    [SerializeField] private DunjeonPlayer dPlayer; // 플레이어 스캐너

    //-- 컨트롤러 --//
    private SkillController controller;

    //-- 컴포넌트 --//
    private Transform transform;

    #endregion

    private void Awake()
    {
        controller = new SkillController(this, transform, data, dPlayer);
    }
}
