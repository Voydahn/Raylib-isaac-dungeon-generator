namespace IsaacDungeonGenerator.Core;


/// <summary>
/// Représente la position d'une salle sur une grille cartésienne discrète.
/// Ce type est immuable.
/// </summary>
public readonly record struct RoomPosition(int X, int Y)
{
    /// <summary>
    /// Calcule la position adjacente dans la direction spécifiée.
    /// Ne modifie pas la position actuelle.
    /// </summary>
    public RoomPosition Offset(DoorDirection direction) => direction switch
    {
        DoorDirection.Up => this with { Y = Y - 1 },
        DoorDirection.Down => this with { Y = Y + 1 },
        DoorDirection.Left => this with { X = X - 1 },
        DoorDirection.Right => this with { X = X + 1 },
        _ => this
    };
}