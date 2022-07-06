using UnityEngine;
using UnitEngine;


public class Building : Entity {
    protected override void Awake() {
        base.Awake();
        Test(); //TODO//
    }

    //TODO//
    void Test() {
        SetModel("building/mesh/hutU3");
    }
}
