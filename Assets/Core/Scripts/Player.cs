using UnityEngine;

namespace Core.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _maxHp;
        
        public static Player Instance { get; private set; }

        private int _hp;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _hp = _maxHp;
        }
        
        public void Hurt(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                //todo player is dead
            }
        }

        public void Heal(int heal)
        {
            _hp += heal;
        }
    }
}
