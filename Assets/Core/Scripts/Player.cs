using System;
using UnityEngine;

namespace Core.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public int MaxHp;
        
        public static Player Instance { get; private set; }
        
        public event EventHandler OnHpChanged;
        
        public int Hp
        {
            get => _hp;
            private set
            {
                _hp = value;
                OnHpChanged?.Invoke(this, EventArgs.Empty);
            }
        }

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
            Hp = MaxHp;
        }
        
        public void Hurt(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                //todo player is dead
            }
        }
        
        public void Heal(int heal)
        {
            Hp += heal;
        }
    }
}
