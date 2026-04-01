using UnityEngine;

public class CrosshairFollow : MonoBehaviour{

    private void Start() {
        Cursor.visible = false;
    }

    void Update(){
        transform.position = Input.mousePosition;
    }
}