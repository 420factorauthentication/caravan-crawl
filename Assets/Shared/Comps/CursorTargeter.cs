using UnityEngine;


public class CursorTargeter : MonoBehaviour {
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
        }
    }
}
