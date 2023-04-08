// using System;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Events;

// namespace LoopEscapeGame.Players {
    
//     public class PlayerLookActionsHandler : InputActionsHandler {

//         [Header("Actions")]
//         [SerializeField] string _lookActionName = "Look";

//         public UnityEvent<Vector2> onLook;


//         protected override void ProcessAction (string actionName, object argument) {
//             if (actionName == _lookActionName) {

//                 if (argument is Vector2) {
//                     onLook?.Invoke((Vector2)argument);
//                 }
//                 else {
//                     LogWrongArgumentType();
//                 }

//             }
//         }



//     }
// }