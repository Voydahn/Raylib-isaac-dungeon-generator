namespace IsaacDungeonGenerator.Core;

/// <summary>
/// Génère un graphe de salles connectées pour former une carte de donjon.
/// L'algorithme repose sur une expansion progressive depuis une salle de départ.
/// </summary>
public class RoomGraphGenerator
{
    private readonly LevelGenerationConfig _config;
    private readonly Random _rng;
    
    /// <param name="roomCount">Nombre total de salles à générer.</param>
    /// <param name="seed">Graine aléatoire pour rendre les générations reproductibles.</param>
    public RoomGraphGenerator(LevelGenerationConfig config, int seed)
    {
        _config = config;
        _rng = new Random(seed);
    }

    /// <summary>
    /// Génère un graphe de salles connectées aléatoirement, en partant d'une salle centrale.
    /// L'agencement est influencé par la graine fournie à la construction.
    /// </summary>
    public IReadOnlyList<Room> Generate()
    {
        var rooms = new List<Room>();
        var startRoom = new Room(RoomType.Start, new RoomPosition(0, 0));
        rooms.Add(startRoom);

        var openSet = new List<Room> { startRoom };

        if (_config.RoomCount < 3)
            throw new InvalidOperationException("Le nombre de salles doit être supérieur à 2.");
        
        while (rooms.Count < _config.RoomCount && openSet.Any())
        {
            // soit on prend le dernier (DFS), soit un aléatoire, soit le premier (BFS)
            var index = _rng.Next(openSet.Count);
            var current = openSet[index];
            openSet.RemoveAt(index);

            foreach (var direction in GetShuffledDirections())
            {
                if (!current.HasDoorIn(direction))
                {
                    var newPos = current.Position.Offset(direction);
                    if (rooms.All(r => r.Position != newPos))
                    {
                        var newRoom = new Room(RoomType.Unknown, newPos);
                        current.ConnectTo(newRoom, direction);
                        rooms.Add(newRoom);
                        openSet.Add(newRoom);

                        if (rooms.Count >= _config.RoomCount)
                            break;
                    }
                }
            }
        }

        AssignSpecialRooms(rooms);

        return rooms;
    }
    
    private bool AreNeighbors(Room a, Room b)
    {
        return a.Neighbors.Values.Contains(b);
    }
    
    private Room FindFurthestRoom(Room start, List<Room> allRooms)
    {
        return allRooms
            .Where(r => r != start)
            .OrderByDescending(r => GetManhattanDistance(r.Position, start.Position))
            .First();
    }

    private int GetManhattanDistance(RoomPosition a, RoomPosition b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    /// <summary>
    /// Attribue des types spéciaux à certaines salles après génération brute.
    /// </summary>
    private void AssignSpecialRooms(List<Room> rooms)
    {
        var startRoom = rooms.First();
        var bossRoom = FindFurthestRoom(startRoom, rooms);
        startRoom.Type = RoomType.Start;
        bossRoom.Type = RoomType.Boss;

        var candidates = rooms
            .Where(r => r.Type == RoomType.Unknown)
            .OrderBy(_ => _rng.Next())
            .ToList();

        foreach (var rule in _config.TypeRules)
        {
            if (!rule.Required && _rng.NextDouble() > rule.Chance)
                continue;

            IEnumerable<Room> filtered = candidates;

            // Règle spéciale : éviter les voisins du boss
            if (rule.Type == RoomType.Shop)
                filtered = filtered.Where(r => !AreNeighbors(r, bossRoom));

            var room = filtered.FirstOrDefault();
            if (room != null)
            {
                room.Type = rule.Type;
                candidates.Remove(room);
            }
        }
    }

    /// <summary>
    /// Retourne les directions cardinales dans un ordre aléatoire.
    /// Utilisé pour varier les connexions entre salles.
    /// </summary>
    private IEnumerable<DoorDirection> GetShuffledDirections()
    {
        return Enum.GetValues<DoorDirection>().OrderBy(_ => _rng.Next());
    }
}
