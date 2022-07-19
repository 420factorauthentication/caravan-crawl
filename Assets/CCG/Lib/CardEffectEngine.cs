using System;
using UnityEngine;
using UnitEngine;
namespace CardEffectEngine /*;*/ {




// =============================================== //
// Describes one thing a HandCard does when played //
// =============================================== //
public interface ICardEffect {
    // Coordinates of which Node will be affected by this CardEffect //
    // -- Offsets from Node hovered by mouse when Card is played -- //
    int ColOffset { get; set; }
    int RowOffset { get; set; }

    // -- Implement all gameplay actions here -- //
    void Activate();
}




// =================================================== //
// A CardEffect that interacts with Entities on a Node //
// =================================================== //
public interface IEntityCardEffect : ICardEffect {
    // The specific unit/building class to spawn/destroy/modify/etc //
    Type EntityType { get; set; }

    // How many Entities to spawn/destroy/modify/etc //
    // -- Depth first searches the Node children -- //
    int Count { get; set; }
}




// =========================================== //
// A CardEffect that spawns Entities on a Node //
// =========================================== //
public struct EntitySpawnEffect : IEntityCardEffect {
    public EntitySpawnEffect(int colOffset, int rowOffset, Type entityType, int count) {
        ColOffset = colOffset;
        RowOffset = rowOffset;
        EntityType = entityType;
        Count = count;
    }

    public Type EntityType { get; set; }
    public int Count { get; set; }
    public int ColOffset { get; set; }
    public int RowOffset { get; set; }

    public void Activate() {
        Transform hoveredTr = CursorTargeter.NewRayHit.transform;
        if (hoveredTr == null) return;
        Node hoveredNode = hoveredTr.GetComponent<Node>();
        if (hoveredNode == null) return;

        int targetNodeCol = hoveredNode.Col + ColOffset;
        int targetNodeRow = hoveredNode.Row + RowOffset;
        Node targetNode = HexGrid.GetNodeAt(targetNodeCol, targetNodeRow);

        targetNode.AddEntities(EntityType, Count);
    }
}




// ================================================================== //
// A CardEffect that destroys Entities on a Node (depth-first search) //
// ================================================================== //
public struct EntityDestroyEffect : IEntityCardEffect {
    public EntityDestroyEffect(int colOffset, int rowOffset, Type entityType, int count) {
        ColOffset = colOffset;
        RowOffset = rowOffset;
        EntityType = entityType;
        Count = count;
    }

    public Type EntityType { get; set; }
    public int Count { get; set; }
    public int ColOffset { get; set; }
    public int RowOffset { get; set; }

    public void Activate() {
        Transform hoveredTr = CursorTargeter.NewRayHit.transform;
        if (hoveredTr == null) return;
        Node hoveredNode = hoveredTr.GetComponent<Node>();
        if (hoveredNode == null) return;

        int targetNodeCol = hoveredNode.Col + ColOffset;
        int targetNodeRow = hoveredNode.Row + RowOffset;
        Node targetNode = HexGrid.GetNodeAt(targetNodeCol, targetNodeRow);

        targetNode.RemoveEntities(EntityType, Count);
    }
}




// ========================================================================== //
// A CardEffect that changes stats of Entities on a Node (depth-first search) //
// ========================================================================== //
//public struct EntityStatEffect : IEntityCardEffect {
    
//}


/***************************************************************************/ }
