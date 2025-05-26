namespace IsaacDungeonGenerator.Core;

/// <summary>
/// Représente une salle dans le donjon, identifiée par sa position et son type.
/// Chaque salle peut avoir des connexions (portes) vers d'autres salles.
/// </summary>
public class Room
{
    /// <summary>
    /// Type logique de la salle (ex: Start, Boss, Trésor, etc.).
    /// Peut évoluer au cours de la génération.
    /// </summary>
    public RoomType Type { get; set; }
    
    /// <summary>
    /// Position unique de la salle sur la grille.
    /// </summary>
    public RoomPosition Position { get; }
    
    
    private readonly Dictionary<DoorDirection, Room> _neighbors = new();

    public Room(RoomType type, RoomPosition position)
    {
        Type = type;
        Position = position;
    }

    /// <summary>
    /// Crée une connexion bidirectionnelle entre cette salle et une autre salle.
    /// </summary>
    /// <param name="target">La salle cible à connecter.</param>
    /// <param name="direction">La direction de la porte reliant les deux salles.</param>
    public void ConnectTo(Room target, DoorDirection direction)
    {
        _neighbors[direction] = target;
        target._neighbors[direction.Opposite()] = this;
    }

    /// <summary>
    /// Vérifie si la salle possède une porte dans la direction spécifiée.
    /// </summary>
    public bool HasDoorIn(DoorDirection direction) => _neighbors.ContainsKey(direction);
    
    /// <summary>
    /// Liste en lecture seule des salles connectées à cette salle par direction.
    /// </summary>
    public IReadOnlyDictionary<DoorDirection, Room> Neighbors => _neighbors;
}