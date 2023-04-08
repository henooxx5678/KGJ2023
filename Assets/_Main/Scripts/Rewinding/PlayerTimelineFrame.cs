using UnityEngine;

public struct PlayerTimelineFrame {

    public Vector3 position;
    public Quaternion rotation;
    public Quaternion camRotation;
    

    public PlayerTimelineFrame (Vector3 position, Quaternion rotation, Quaternion camRotation) {
        this.position = position;
        this.rotation = rotation;
        this.camRotation = camRotation;
    }


    public void SetToPlayer (PlayerStatusManager playerStatusManager) {
        playerStatusManager.SetStatus(position, rotation, camRotation);
    }
        

}