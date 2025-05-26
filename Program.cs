using IsaacDungeonGenerator.Core;
using Raylib_cs;

namespace IsaacDungeonGenerator.Game;

class Program
{
    public static (int minX, int maxX, int minY, int maxY) GetBounds(IEnumerable<Room> rooms)
    {
        int minX = rooms.Min(r => r.Position.X);
        int maxX = rooms.Max(r => r.Position.X);
        int minY = rooms.Min(r => r.Position.Y);
        int maxY = rooms.Max(r => r.Position.Y);
        return (minX, maxX, minY, maxY);
    }
    
    RoomGraphGenerator generator;
    RoomRenderer renderer = new();
    List<Room> rooms = new();
    int offsetX = 0, offsetY = 0;
    int cellSizeW = 48;
    int cellSizeH = 24;
    
    void GenerateLevel()
    {
        var config = new LevelGenerationConfig
        {
            RoomCount = 30,
            TypeRules = new()
            {
                new RoomTypeRule(RoomType.Treasure, required: true),
                new RoomTypeRule(RoomType.Shop, required: true),
                new RoomTypeRule(RoomType.MiniBoss, required: false, chance: 0.5),
                new RoomTypeRule(RoomType.Challenge, required: false, chance: 0.5),
                new RoomTypeRule(RoomType.Rest, required: false, chance: 0.3)
            }
        };
        
        generator = new RoomGraphGenerator(config, new Random().Next());
        rooms = generator.Generate().ToList();

        var (minX, maxX, minY, maxY) = GetBounds(rooms);
        int levelWidth = (maxX - minX + 1) * cellSizeW;
        int levelHeight = (maxY - minY + 1) * cellSizeH;

        offsetX = (Raylib.GetScreenWidth() - levelWidth) / 2 - minX * cellSizeW;
        offsetY = (Raylib.GetScreenHeight() - levelHeight) / 2 - minY * cellSizeH;
    }
    
    public void Start() {
        Raylib.InitWindow(800, 600, "Room Generation");
        Raylib.SetTargetFPS(60);
        GenerateLevel();

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.R))
                GenerateLevel();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            renderer.DrawRooms(rooms, offsetX, offsetY, cellSizeW, cellSizeH);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    
    public static void Main()
    {
        new Program().Start();
    }
}