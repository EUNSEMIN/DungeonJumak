//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;
using UnityEngine.UI;

// Ect
using Data.Object;

namespace Skill.Controller
{
    public class SkillController : MonoBehaviour
    {
        [Header("SO")]
        public SkillDataSO dataSO;

        [Header("스킬 사용 가능 여부")]
        public bool canSkill;

        [Header("스캐너")]
        [SerializeField] private Scanner scanner;

        [Header("스킬 하이드 이미지")]
        [SerializeField] private Image hideImage;

        [Header("풀 생성용 프리팹")]
        [SerializeField] private Skill prefab;

        [Header("풀 생성 개수")]
        [SerializeField] private int maxSpawnCount = 5;

        private float timer; // 대기 시간
        private PoolManager<Skill> poolManager; // 풀매니저
        private Coroutine skillCoroutine; // 코루틴 변수

        private void Start()
        {
            timer = dataSO.waitingTime;

            //-- 풀 매니저 초기화 --//
            poolManager = new PoolManager<Skill>(transform);
            poolManager.CreatePool(prefab, maxSpawnCount);

            skillCoroutine = StartCoroutine(AutoFireBall());
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

        #region FireBall

        // 스킬 01: FireBall (화염구 던지기)
        public void FireBall()
        {
            // 스캔 범위 내에 적이 없을 때
            if (!scanner.nearestTarget)
            {
                return;
            }

            // 스캔 범위 내에 적이 존재하고 스킬 사용 가능할 때
            if (scanner.nearestTarget)
            {
                canSkill = false;

                hideImage.gameObject.SetActive(true);

                // 타겟 몬스터 위치
                Vector3 targetPos = scanner.nearestTarget.position;

                // 방향 계산
                Vector3 direction = targetPos - transform.position;
                direction = direction.normalized;

                // 스킬 풀링
                Transform fireball = poolManager.GetFromPool(prefab).transform;

                // 스킬 위치 초기화
                fireball.position = transform.position;

                // 회전
                fireball.rotation = Quaternion.FromToRotation(Vector3.up, direction);

                // Skill.cs Init() 호풀
                fireball.GetComponent<Skill>().Init(direction);
            }
        }

        // 01-1: AutoFireBall: 일정 시간마다 FireBall 스킬 발사
        private IEnumerator AutoFireBall()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);

                FireBall();
            }
        }
        #endregion Fire Ball
    }

}
