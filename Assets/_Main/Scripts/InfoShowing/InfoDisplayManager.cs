using UnityEngine;

public class InfoDisplayManager : MonoBehaviour {

    [SerializeField] KeyCode _infoDisplayKey = KeyCode.Tab;

    [SerializeField] GameObject _infoShowingTip;
    [SerializeField] GameObject _infoDisplayPanel;

    void Update () {
        if (Input.GetKeyDown(_infoDisplayKey)) {
            bool isActive = !_infoDisplayPanel.activeSelf;
            _infoDisplayPanel.SetActive(isActive);

            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;

            _infoShowingTip.SetActive(false);
        }
    }

}