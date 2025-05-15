using UnityEngine;

namespace ATG.Characters
{
    public class Player: MonoBehaviour
    {
        public float Speed = 1f;
        public float Hp = 20f;
        
        public void DoMove(Transform currentTarget)
        {
            if(currentTarget == null) return;
            
            var direction = (currentTarget.position - transform.position);
            if(direction.sqrMagnitude < 0.5f) return;
            
            var next = transform.position + direction.normalized * Time.deltaTime * Speed;
            transform.position = next;
        }

        public void DoDamage(int dmg)
        {
            Hp -= dmg;
            Debug.Log($"damage done: {Hp}");
        }
    }
}