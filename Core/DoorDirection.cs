namespace IsaacDungeonGenerator.Core;

/// <summary>
/// Représente les directions cardinales possibles pour les connexions entre salles.
/// </summary>
public enum DoorDirection { Up, Down, Left, Right }

/// <summary>
/// Extensions utilitaires pour manipuler les directions de portes.
/// </summary>
public static class DoorDirectionExtensions
{
    /// <summary>
    /// Retourne la direction opposée à celle spécifiée.
    /// Utilisé pour connecter deux salles dans les deux sens.
    /// </summary>
    public static DoorDirection Opposite(this DoorDirection dir) => dir switch
    {
        DoorDirection.Up => DoorDirection.Down,
        DoorDirection.Down => DoorDirection.Up,
        DoorDirection.Left => DoorDirection.Right,
        DoorDirection.Right => DoorDirection.Left,
        _ => throw new ArgumentOutOfRangeException()
    };
}