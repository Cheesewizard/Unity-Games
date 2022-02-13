using System.Collections.Generic;
using System.Linq;
using Extensions;
using Mirror;
using UnityEngine;

namespace Game.GameBoard
{
    public class Route : MonoBehaviour
    {
        public Transform[] childObjects;
        public List<Transform> childNodesList = new List<Transform>();

        private void Start()
        {
            childNodesList = FillNodes();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var childNodes = FillNodes();

            for (var i = 0; i < childNodes.Count; i++)
            {
                var currentPos = childNodes[i].position;
                if (i > 0)
                {
                    var prevPos = childNodes[i - 1].position;
                    Gizmos.DrawLine(prevPos, currentPos);
                }
            }
        }

        private List<Transform> FillNodes()
        {
            var childNodes = new List<Transform>();
            // childNodesList.Clear();

            // Find the nested gameboard data
            var gameBoard = transform.FindObjectsWithTag("GameBoard").FirstOrDefault();
            if (gameBoard != null) childObjects = gameBoard.GetComponentsInChildren<Transform>();

            // Skip one as this contains the parent object which we dont want to include
            foreach (var child in childObjects.Skip(1))
            {
                childNodes.Add(child);
            }

            return childNodes;
        }
    }
}