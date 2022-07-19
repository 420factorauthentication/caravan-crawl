using UnityEngine;
using DelegateEngine;
namespace ConditionEngine /*;*/ {


// ========================================= //
// A collection of delegates returning bools //
// ========================================= //
public struct Condition {
    public BoolExpr BoolExprs;
    public bool Evaluate() {
        foreach (BoolExpr func in BoolExprs.GetInvocationList())
            if (func() == false) return false;
        return true;
    }
}




// ========================================================== //
// Static methods to use as Conditions for playing a HandCard //
// ========================================================== //
public static class TargetConditions {
    public static bool NodeHasEntity<T>(int nodeCol, int nodeRow) {

    }
    public static bool NodeHasEntity<T>(Node node) {

    }
    public static bool NodeHasEntity<T>(GameObject nodeObj) {

    }
}


/***************************************************************************/ }
