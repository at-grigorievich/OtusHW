using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Services
{
    public class CharacterMoveController: MonoBehaviour
    {
        [SerializeField] private Entity character;

        private void Update()
        {
            //Vector3 direction = MoveInput.GetDirection();
            //ref Direction direction = ref character.GetData<Direction>();
            //direction.Value = direction;
        }
    }
}