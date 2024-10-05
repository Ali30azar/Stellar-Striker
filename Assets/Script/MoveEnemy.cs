// using UnityEngine;
//
// namespace Script
// {
//     public class MoveEnemy : MonoBehaviour
//     {
//         public MovementPattern movementPattern;
//         private int currentWaypointIndex = 0;
//         private bool movingForward = true;
//
//         void Update()
//         {
//             if (movementPattern == null || movementPattern.waypoints.Length == 0) return;
//
//
//             Vector3 target = movementPattern.waypoints[currentWaypointIndex];
//             transform.position =
//                 Vector3.MoveTowards(transform.position, target, movementPattern.speed * Time.deltaTime);
//
//             if (Vector3.Distance(transform.position, target) < 0.1f)
//             {
//                 switch (movementPattern.movementType)
//                 {
//                     case MovementPattern.MovementType.Loop:
//                         currentWaypointIndex = (currentWaypointIndex + 1) % movementPattern.waypoints.Length;
//                         break;
//                     case MovementPattern.MovementType.PingPong:
//                         if (movingForward)
//                         {
//                             currentWaypointIndex++;
//                             if (currentWaypointIndex >= movementPattern.waypoints.Length)
//                             {
//                                 currentWaypointIndex = movementPattern.waypoints.Length - 2;
//                                 movingForward = false;
//                             }
//                         }
//                         else
//                         {
//                             currentWaypointIndex--;
//                             if (currentWaypointIndex < 0)
//                             {
//                                 currentWaypointIndex = 1;
//                                 movingForward = true;
//                             }
//                         }
//
//                         break;
//                     case MovementPattern.MovementType.Once:
//                         if (currentWaypointIndex < movementPattern.waypoints.Length - 1)
//                         {
//                             currentWaypointIndex++;
//                         }
//
//                         break;
//                 }
//             }
//         }
//     }
// }