using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Manager
{
    public class MovementManager : NetworkBehaviour
    {
        public static MovementManager Instance { get; private set; }

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


        // Dice
        public readonly SyncList<int> serverMovement = new SyncList<int>();
        public readonly List<int> movement = new List<int>();

        private void Start()
        {
            syncInterval = 0;
            serverMovement.Callback += MovementChange;
        }

        private void MovementChange(SyncList<int>.Operation op, int itemindex, int olditem, int newitem)
        {
            switch (op)
            {
                case SyncList<int>.Operation.OP_CLEAR:
                    movement.Clear();
                    break;
                case SyncList<int>.Operation.OP_ADD:
                    movement.Add(newitem);
                    break;
            }
        }
        
        [Command(requiresAuthority = false)]
        public void CmdAddMovementAmount(int step)
        {
            serverMovement.Add(step);
        }

        [Command(requiresAuthority = false)]
        public void CmdClearMovement()
        {
            serverMovement.Clear();
        }
    }
}