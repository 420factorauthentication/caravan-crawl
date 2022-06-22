using UnityEngine;


//Singleton that manages cursor hover interactions//
public class CursorTargeter : MonoBehaviour {
    public static CursorTargeter Manager;

    void Awake() {
        if ((Manager != null) && (Manager != this))
            GameObject.Destroy(Manager.gameObject);
        Manager = this;
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
        }
    }
}
