//Engine
using UnityEngine;

namespace Data.Character
{
    [CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable/Character/Monster")]
    public class MonsterDataSO : ScriptableObject
    {
        //--- 기본 정보 ---//
        [Header("기본 정보")]
        [Tooltip("몬스터의 이름을 설정합니다.")]
        [SerializeField] private string m_name; // 몬스터 종류

        [Tooltip("몬스터 처치 시 플레이어에게 제공하는 경험치(XP)를 설정합니다.")]
        [SerializeField] private float m_xp; // 제공 XP

        [Tooltip("몬스터의 이동 속도를 설정합니다.")]
        [SerializeField] private float m_speed; // 이동 속도

        //--- 전투 스택 ---//
        [Space(10)]
        [Header("전투 스탯")]
        [Tooltip("몬스터가 플레이어에게 가하는 공격 데미지 수치를 설정합니다.")]
        [SerializeField] private float m_attackDamage; // 제공 데미지 수치

        [Tooltip("몬스터의 체력(HP)를 설정합니다.")]
        [SerializeField] private float m_hp; // 최대 HP

        public string Name { get { return m_name; } }
        public float Xp { get { return m_xp; } }
        public float Damage { get { return m_attackDamage; } }
        public float Hp { get { return m_hp; } }
        public float Speed { get { return m_speed; } }
    }
}
