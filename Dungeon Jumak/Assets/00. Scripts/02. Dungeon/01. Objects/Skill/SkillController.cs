//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;
using UnityEngine.UI;

using Data.Object;
using System.Runtime.ConstrainedExecution;
using UnityEditor.EditorTools;

public class SkillController : MonoBehaviour
{
    [Header("SO")]
    public SkillDataSO dataSO;

    [Header("프리팹")]
    [SerializeField] private Skill prefab;

    [Header("스킬 사용 가능 여부")]
    public bool canSkill;

    [Header("스캐너")]
    [SerializeField] private Scanner scanner;

    [Header("스킬 하이드 이미지")]
    [SerializeField] private Image hideImage;

    [Header("풀 생성 개수")]
    [SerializeField] private int maxSpawnCount = 5;

    [Header("스킬 대기 시간 (진행 중)")]
    public float timer;

    private float currentDuration = 0;

    private PoolManager<Skill> poolManager;

    private void Start()
    {
        // 풀 매니저 초기화
        poolManager = new PoolManager<Skill>(transform);
        poolManager.CreatePool(prefab, maxSpawnCount);
    }

    private void Update()
    {
        if (hideImage.gameObject.activeSelf)
        {
            hideImage.fillAmount = timer / dataSO.coolTime;
        }

        switch (dataSO.skillId)
        {
            //Fire Ball
            case 0:
                break;
            //Fire Shield
            case 1:
                break;
            //Fire Flooring
            case 2:
                break;

            default:
                break;
        }

        CoolTime();
    }

    #region Common Method

    //Common Method : Init
    public void Init()
    {
        switch (dataSO.skillId)
        {
            //Fire Ball
            case 0:
                break;

            //Fire Shield
            case 1:
                break;

            //Fire Flooring
            case 3:
                break;

            default:
                break;
        }
    }

    //!Common Method : CoolTime
    private void CoolTime()
    {
        if (!canSkill && hideImage.gameObject.activeSelf)
        {
            timer += Time.deltaTime;

            if (timer > dataSO.coolTime)
            {
                canSkill = true;

                hideImage.gameObject.SetActive(false);

                timer = 0f;
            }
        }
    }

    #endregion

    #region Fire Ball

    //Fire Ball : Fire Ball Casting Method
    public void FireBall()
    {
        //if can't search target
        if (!scanner.nearestTarget)
        {
            Debug.Log("주변에 적이 없다!");
            return;
        }

        if (canSkill)
        {
            canSkill = false;

            //Hide Skill Image
            hideImage.gameObject.SetActive(true);

            //Get Tartget Position
            Vector3 targetPos = scanner.nearestTarget.position;

            //Get Direction
            Vector3 direction = targetPos - transform.position;

            //Nomalized
            direction = direction.normalized;

            //pooling fire ball
            Transform fireball = poolManager.GetFromPool(prefab).transform;

            //reposition
            fireball.position = transform.position;

            //rotation
            fireball.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            //Init
            fireball.GetComponent<Skill>().Init(direction);

            hideImage.gameObject.SetActive(true);
        }
    }

    #endregion Fire Ball
}
