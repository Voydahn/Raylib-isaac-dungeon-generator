using IsaacDungeonGenerator.Core;
using Raylib_cs;

namespace IsaacDungeonGenerator.Game;

/// <summary>
/// Définit le style visuel associé à un type de salle.
/// </summary>
public class RoomStyleDefinition
{
    public RoomType Type { get; }
    public Color FillColor { get; }
    public Color BorderColor { get; }

    private RoomStyleDefinition(RoomType type, Color fillColor, Color borderColor)
    {
        Type = type;
        FillColor = fillColor;
        BorderColor = borderColor;
    }

    private static readonly Dictionary<RoomType, RoomStyleDefinition> _styles = new()
    {
        [RoomType.Unknown] = new(RoomType.Unknown, Color.DarkGray, Color.Black),
        [RoomType.Start] = new(RoomType.Start, Color.Green, Color.DarkGreen),
        [RoomType.Boss] = new(RoomType.Boss, Color.Red, Color.Maroon),
        [RoomType.Treasure] = new(RoomType.Treasure, Color.Gold, Color.Orange),
        [RoomType.Shop] = new(RoomType.Shop, Color.Brown, Color.DarkBrown),
        [RoomType.Secret] = new(RoomType.Secret, Color.DarkBlue, Color.Blue),
        [RoomType.MiniBoss] = new(RoomType.MiniBoss, Color.Pink, Color.Maroon),
        [RoomType.Challenge] = new(RoomType.Challenge, Color.LightGray, Color.DarkGray),
        [RoomType.Rest] = new(RoomType.Rest, Color.SkyBlue, Color.DarkGreen),
        [RoomType.Exit] = new(RoomType.Exit, Color.Purple, Color.Violet),
    };

    public static RoomStyleDefinition Get(RoomType type) => _styles[type];
}