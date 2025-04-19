using UnityEngine;

namespace Core.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected int Hp;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Extinguisher"))
            {
                Hp--;
                if (Hp <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
