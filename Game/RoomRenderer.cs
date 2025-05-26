using IsaacDungeonGenerator.Core;
using Raylib_cs;

namespace IsaacDungeonGenerator.Game;

/// <summary>
/// Gère l'affichage visuel des salles du donjon à l'écran.
/// Ce renderer utilise Raylib pour dessiner une représentation 2D du graphe.
/// </summary>
public class RoomRenderer
{
    /// <summary>
    /// Dessine l'ensemble des salles et leurs connexions sur l'écran.
    /// Les positions sont recalculées en fonction d'un offset global et d'un espacement configurable.
    /// </summary>
    /// <param name="rooms">Les salles à dessiner.</param>
    /// <param name="offsetX">Décalage horizontal pour centrer la carte.</param>
    /// <param name="offsetY">Décalage vertical pour centrer la carte.</param>
    /// <param name="cellSize">Taille d'une cellule de salle en pixels.</param>
    /// <param name="spacing">Espacement visuel entre les salles.</param>
    public void DrawRooms(
        IEnumerable<Room> rooms,
        int offsetX,
        int offsetY,
        int cellSizeW = 64,
        int cellSizeH = 64,
        int spacing = 8
    )
    {
        int paddedSizeW = cellSizeW - spacing;
        int paddedSizeH = cellSizeH - spacing;
        var mouse = Raylib.GetMousePosition();
        
        foreach (var room in rooms)
        {
            int x = room.Position.X * cellSizeW + offsetX + spacing / 2;
            int y = room.Position.Y * cellSizeH + offsetY + spacing / 2;

            var style = RoomStyleDefinition.Get(room.Type);

            bool isHovered = Raylib.CheckCollisionPointRec(mouse, new Rectangle(x, y, paddedSizeW, paddedSizeH));
            
            // Coloration spécifique pour les salles spéciales (Start, Boss)
            // Remplissage
            Raylib.DrawRectangle(x, y, paddedSizeW, paddedSizeH, style.FillColor);

            // Bordure
            Raylib.DrawRectangleLines(x, y, paddedSizeW, paddedSizeH, isHovered ? Color.Yellow : style.BorderColor);

            // Texte
            // Raylib.DrawText(room.Type.ToString(), x + 4, y + 4, 12, Color.Black);

            // Portes
            foreach (var (dir, neighbor) in room.Neighbors)
            {
                var doorSizeW = 4;
                var doorSizeH = 4;
                
                var doorX = x + (dir == DoorDirection.Left ? - (doorSizeW/2) : 
                    dir == DoorDirection.Right ? paddedSizeW - (doorSizeW/2) :
                    paddedSizeW / 2 - (doorSizeW/2));

                var doorY = y + (dir == DoorDirection.Up ? - (doorSizeH/2) :
                    dir == DoorDirection.Down ? paddedSizeH - (doorSizeH/2) :
                    paddedSizeH / 2 - (doorSizeH/2));

                Raylib.DrawRectangle(doorX, doorY, doorSizeW, doorSizeH, Color.Red);
                
                // --- Jointure dans l'espace entre les salles ---
                int centerAX = x + paddedSizeW / 2;
                int centerAY = y + paddedSizeH / 2;

                int neighborX = neighbor.Position.X * cellSizeW + offsetX + spacing / 2;
                int neighborY = neighbor.Position.Y * cellSizeH + offsetY + spacing / 2;

                int centerBX = neighborX + paddedSizeW / 2;
                int centerBY = neighborY + paddedSizeH / 2;

                // Pont dans l'espace entre deux salles (juste dans le spacing)
                DrawDoorBridge(centerAX, centerAY, centerBX, centerBY, Math.Min(doorSizeW, doorSizeH), Color.Red);
            }
        }
    }
    
    private void DrawDoorBridge(int centerAX, int centerAY, int centerBX, int centerBY, int thickness, Color color)
    {
        if (centerAX == centerBX)
        {
            // Vertical
            int minY = Math.Min(centerAY, centerBY);
            Raylib.DrawRectangle(centerAX - thickness / 2, minY + 1, thickness, Math.Abs(centerAY - centerBY) - 2, color);
        }
        else if (centerAY == centerBY)
        {
            // Horizontal
            int minX = Math.Min(centerAX, centerBX);
            Raylib.DrawRectangle(minX + 1, centerAY - thickness / 2, Math.Abs(centerAX - centerBX) - 2, thickness, color);
        }
    }
}