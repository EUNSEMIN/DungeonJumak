//Engine
using UnityEngine;

namespace Data.Object
{
    [CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable/Skill")]
    public class SkillDataSO: ScriptableObject
    {
        [Header("스킬 정보")]
        public string name; // 이름
        [Tooltip("스킬의 ID를 설정합니다. (0: 기본공격, 1: 불꽃고리, 2: 바람베기, 계속 추가.. ")]
        public int skillId; // 스킬 ID
        public int prefabId; // 프리팹 ID
        public float damage; // 적용 데미지
        public float per; // 관통력
        public float knockBack; // 적용 넉백력
        public float coolTime; // 쿨타임
        public int count; // 스킬 투사체 수
        public float speed; // 스킬 속도
        public float duration; // 스킬 지속 시간
    }
}