using UnityEngine;
using UnitEngine;


// ===================================================== //
// A building on a Node. Created by cards, effects, etc. //
// ===================================================== //
public class Building : Entity {
    protected override void Awake() {
        base.Awake();
        Test(); //TODO//
    }

    // -- TODO -- //
    void Test() {
        SetModel("building/mesh/hutU3");
    }
}
