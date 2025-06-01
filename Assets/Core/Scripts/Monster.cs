    using System.Collections;
    using Core.Scripts.EnemyStateMachine.MonsterStateMachine;
    using UnityEngine;

    namespace Core.Scripts
{
    public class Monster : Enemy
    {
        
        protected override void Awake()
        {
            CurrentState = new MonsterIdleState();
        }

        public override void Die(bool destroyObject = true)
        {
            EnemyAnimator.SetBool("IsDead", true);
            EnemyAnimator.SetBool("IsChasing", false);
            EnemyAnimator.SetBool("IsAgro", false);
            StartCoroutine(Disappear(3f));
            base.Die(false);
        }

        private IEnumerator Disappear(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
