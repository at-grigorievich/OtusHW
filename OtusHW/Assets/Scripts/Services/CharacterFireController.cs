using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Services
{
    public sealed class CharacterFireController: MonoBehaviour
    {
        [SerializeField] private Entity character;

        private void Update()
        {
            if (FireInput.IsFirePressDown())
            {
                character.SetData(new FireRequest() );
            }
        }
    }
}