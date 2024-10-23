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

        /*[Header("스킬 하이드 이미지")]
        [SerializeField] private Image hideImage;*/

        [Header("풀 생성용 프리팹")]
        [SerializeField] private Skill prefab;

        [Header("풀 생성 개수")]
        [SerializeField] private int maxSpawnCount = 5;

        private float currentDuration = 0; // 현재 지속 시간
        private float timer = 0; 
        private PoolManager<Skill> poolManager; 
        private Coroutine skillCoroutine; 

        private void Start()
        {
            poolManager = new PoolManager<Skill>(transform);
            poolManager.CreatePool(prefab, maxSpawnCount);

            // 00.FireBall 스킬 자동 발사
            skillCoroutine = StartCoroutine(AutoFireBall()); 
        }

        private void Update()
        {
            /*if (hideImage.gameObject.activeSelf)
            {
                hideImage.fillAmount = timer / dataSO.coolTime;
            }*/

            switch (dataSO.skillId)
            {
                // 00.FireBall (Auto)
                case 0:
                    break;
                // 01.FireRing (Drawing)
                case 1:
                    if (transform.childCount != 0)
                    {
                        transform.Rotate(Vector3.back * dataSO.speed * Time.deltaTime);
                        currentDuration += Time.deltaTime;
                        if (currentDuration >= dataSO.duration)
                        {
                            currentDuration = 0f;
                            Demolition();
                            //hideImage.gameObject.SetActive(true);
                        }
                    }
                    else if (transform.childCount == 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }

                    break;
                default:
                    break;
            }

            CoolTime();
        }

        #region Common Method

        //!Common Method : CoolTime
        private void CoolTime()
        {
            if (!canSkill /*&& hideImage.gameObject.activeSelf*/)
            {
                timer += Time.deltaTime;

                if (timer > dataSO.coolTime)
                {
                    canSkill = true;

                    //hideImage.gameObject.SetActive(false);

                    timer = 0f;
                }
            }
        }

        #endregion

        #region FireBall

        // 스킬 00:FireBall (화염구 던지기)
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

                //hideImage.gameObject.SetActive(true);

                // 타겟 몬스터 위치
                Vector3 targetPos = scanner.nearestTarget.position;

                // 방향 벡터 계산
                Vector3 direction = targetPos - transform.position;
                direction = direction.normalized;

                // 풀링
                Transform fireball = poolManager.GetFromPool(prefab).transform;

                // position 설정
                fireball.position = transform.position;

                // 회전
                fireball.rotation = Quaternion.FromToRotation(Vector3.up, direction);

                // Skill.cs Init() 호출
                fireball.GetComponent<Skill>().Init(direction);
            }
        }

        // 0AutoFireBall: 일정 시간마다 FireBall 스킬 발사
        private IEnumerator AutoFireBall()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);

                FireBall();
            }
        }
        #endregion Fire Ball

        #region Fire Ring

        // 스킬 01:FireShield (플레이어 주변을 도는 불꽃 고리)
        public void FireRing()
        {
            if (canSkill)
            {
                canSkill = false;
                Batch();
            }
        }

        private void Batch()
        {
            Transform skillRound = poolManager.GetFromPool(prefab).transform; ;

            skillRound.transform.parent = transform;

            skillRound.transform.localPosition = Vector3.zero;
            skillRound.transform.localRotation = Quaternion.Euler(90, 0, 0);

            for (int i = 0; i < dataSO.count; i++)
            {
                // 풀링
                Transform skill = poolManager.GetFromPool(prefab).transform;

                // 부모 오브젝트 위치 설정
                skill.parent = transform;

                //local position 설정
                skill.localPosition = Vector3.zero;
                skill.localRotation = Quaternion.Euler(0, 0, 0);

                // 회전 방향 벡터 계산
                Vector3 rotVec = Vector3.forward * 360 * i / dataSO.count;

                // 회전
                skill.Rotate(rotVec);

                // 이동
                skill.Translate(skill.up * 2f, Space.World);

                // Skill.cs Init() 호출
                skill.GetComponent<Skill>().Init(Vector3.zero); 
            }
        }

        private void Demolition()
        {
            Transform[] childs = GetComponentsInChildren<Transform>();

            //Un Pool
            foreach (var child in childs)
            {
                if (child.gameObject == transform.gameObject)
                {
                    continue;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        #endregion

    }

}
