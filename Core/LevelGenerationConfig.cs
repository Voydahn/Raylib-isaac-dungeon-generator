namespace IsaacDungeonGenerator.Core;


/// <summary>
/// Contient les paramètres de génération pour un type de niveau.
/// Permet de définir les règles de salle ainsi que les contraintes spécifiques.
/// </summary>
public class LevelGenerationConfig
{
    /// <summary>
    /// Nombre total de salles (y compris Start et Boss).
    /// </summary>
    public int RoomCount { get; init; } = 15;

    /// <summary>
    /// Règles d'apparition des types de salle (obligatoires ou aléatoires).
    /// </summary>
    public List<RoomTypeRule> TypeRules { get; init; } = new();
}