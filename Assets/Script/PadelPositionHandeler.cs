using System;
using System.Linq;
using UnityEngine;

namespace Script
{
    public class PadelPositionHandeler : MonoBehaviour
    {
        private void Update()
        {
            if (Spawner.Instance.padels == null) return;
            if (Spawner.Instance.padels.All(padel => padel.setOnPosition))
            {

                foreach (var padel in Spawner.Instance.padels)
                {
                    padel.start = true;
                }
            }
        }
    }
}