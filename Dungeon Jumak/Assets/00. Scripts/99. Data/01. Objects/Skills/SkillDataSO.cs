//Engine
using UnityEngine;

namespace Data.Object
{
    [CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable/Skill")]
    public class SkillDataSO: ScriptableObject
    {
        //-- 기본 정보 --//
        [Header("스킬 정보")]
        [Tooltip("스킬의 이름을 설정합니다.")]
        [SerializeField] private string name; // 스킬 이름

        [Tooltip("스킬의 ID를 설정합니다. (0: 기본공격, 1: 불꽃고리, 2: 바람베기, 계속 추가.. ")]
        [SerializeField] private int id; // ID

        [Tooltip("스킬이 몬스터에게 가하는 공격 데미지 수치를 설정합니다.")]
        [SerializeField] private float damage; // 적용 데미지

        [Tooltip("스킬의 플레이어 관통력 수치를 설정합니다.")]
        [SerializeField] private float per; // 관통력

        [Tooltip("스킬의 플레이어 넉백력 수치를 설정합니다.")]
        [SerializeField] private float knockBack; // 적용 넉백력

        public string Name { get { return name; } }
        public int ID { get { return id; } }
        public float Damage { get { return damage; } }
        public float Per { get { return per; } }
        public float KnockBack { get { return knockBack; } }
    }
}