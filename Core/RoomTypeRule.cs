namespace IsaacDungeonGenerator.Core;


/// <summary>
/// Représente une règle de génération appliquée à un type de salle.
/// Elle définit si la salle est obligatoire, facultative, et sa probabilité d’apparition.
/// </summary>
public record RoomTypeRule
{
    /// <summary>
    /// Type de salle concerné par la règle.
    /// </summary>
    public RoomType Type { get; init; }

    /// <summary>
    /// Si vrai, la salle doit absolument apparaître dans la génération.
    /// </summary>
    public bool Required { get; init; }

    /// <summary>
    /// Probabilité que cette salle soit générée, entre 0.0 et 1.0.
    /// Ne s’applique que si la salle n’est pas obligatoire.
    /// </summary>
    public double Chance { get; init; } = 1.0;

    public RoomTypeRule(RoomType type, bool required, double chance = 1.0)
    {
        if (chance < 0.0 || chance > 1.0)
            throw new ArgumentOutOfRangeException(nameof(chance), "Chance must be between 0.0 and 1.0");

        Type = type;
        Required = required;
        Chance = chance;
    }
}