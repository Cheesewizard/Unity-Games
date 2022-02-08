using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Manager.Player.Spawn
{
    [Obsolete]
    public class PlayerSpawnManager : NetworkBehaviour
    {
        public static PlayerSpawnManager Instance { get; private set; }
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }


        public GameObject playerSpawnObject;
        private Queue<Transform> _spawns = new Queue<Transform>();

        private void Start()
        {
            QueuePlayerSpawns();
        }


        [Command]
        public void SpawnPlayerAt(GameObject player)
        {
            if (_spawns.Count <= 0)
            {
                return;
            }

            var spawn = _spawns.Dequeue();
            SetPlayerSpawn(player, spawn);
        }
        
        private void QueuePlayerSpawns()
        {
            var spawns = playerSpawnObject.GetComponentsInChildren<Transform>();
            
            // Skip 1 element since GetComponentsInChildren returns the parent transform too
            foreach (var spawn in spawns.Skip(1))
            {
                _spawns.Enqueue(spawn.transform);
            }
        }

        [Server]
        private void SetPlayerSpawn(GameObject player, Transform spawnPoint)
        {
            player.transform.position = spawnPoint.position;
        }
    }
}