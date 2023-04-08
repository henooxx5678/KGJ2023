// using UnityEngine;
// using UnityEngine.Events;

// namespace LoopEscapeGame.Players {
    
//     public class PlayerMoveActionsHandler : InputActionsHandler {

//         [Header("Actions")]
//         [SerializeField] string _moveActionName = "Move";
//         [SerializeField] string _sprintActionName = "Sprint";
        
        
//         public UnityEvent<Vector2> onMove;
//         public UnityEvent<bool> onSetSprint;


//         protected override void ProcessAction (string actionName, object argument) {
//             if (actionName == _moveActionName) {

//                 if (argument is Vector2) {
//                     onMove?.Invoke((Vector2) argument);
//                 }
//                 else {
//                     LogWrongArgumentType();
//                 }

//             }
//             else if (actionName == _sprintActionName) {

//                 if (argument is bool) {
//                     onSetSprint?.Invoke((bool) argument);
//                 }
//                 else {
//                     Debug.LogWarning("Wrong argument type!");
//                 }

//             }
//         }

        
//     }

// }