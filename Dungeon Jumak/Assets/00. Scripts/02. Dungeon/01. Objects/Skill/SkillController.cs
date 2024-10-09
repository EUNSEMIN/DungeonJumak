//Engine
using UnityEngine;

//Ect
using Data.Object;
using UnityEditor.EditorTools;

public class SkillController 
{
    #region Vairables

    private SkillDataSO data; // SO
    private Transform transform;
    private DunjeonPlayer dPlayer; // 플레이어
    private Scanner scanner; // 스캐너
    private PoolManager<Skill> pool; // 풀
    private bool canSkill = false; // 스킬 사용 가능 여부

    //-- 풀매니저 관련 --//
    private Skill prefab;
    private float spawnDelay = 1.0f;
    private int maxCount = 5;

    #endregion

    public SkillController(Skill prefab, Transform _transform, SkillDataSO _data, DunjeonPlayer _player)
    {
        transform = _transform;
        dPlayer = _player;
    }

    private void Awake()
    {
        scanner = dPlayer.scanner;

        // 풀 매니저 초기화
        pool = new PoolManager<Skill>(transform);
        pool.CreatePool(prefab, 20);
    }

    public void Update()
    {
        switch (data.ID)
        {
            // [ID:1] 기본 공격
            case 0:
                FireBall();
                break;

            // [ID:2] 불꽃 고리
            case 1:
                break;

            // [ID:3] 바람 베기
            case 2:
                break;
        }
    }

    #region Fire Ball Method

    private void FireBall()
    {
        // 주변에 적이 없으면
        if (!scanner.nearestTarget)
        {
            Debug.Log("주변에 적이 없다!");
            return;
        }

        // 주변에 적이 있고, 스킬 사용 가능하면
        if (canSkill)
        {
            canSkill = false;

            // 타겟 오브젝트 포지션 할당
            Vector3 targetPos = scanner.nearestTarget.position;

            // 방향 계산
            Vector3 direction = targetPos - transform.position;

            // 노멀라이즈
            direction = direction.normalized;

            //pooling fire ball
            Transform fireball = pool.GetFromPool(prefab).transform;

            //reposition
            fireball.position = transform.position;

            //rotation
            fireball.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
    }

    #endregion 

}
