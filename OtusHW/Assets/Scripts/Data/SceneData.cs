using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Data
{
    public class SceneData: MonoBehaviour
    {
        public Transform redTeamSpawnPoint;
        public Transform blueTeamSpawnPoint;

        public Transform bulletsRoot;

        public Entity redSoldier;
        public Entity blueSoldier;

        public int unitsPerTeam;
        public float spawnRadius;
    }
}