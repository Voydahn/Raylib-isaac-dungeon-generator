namespace IsaacDungeonGenerator.Core;

/// <summary>
/// Représente le type logique d'une salle.
/// Utilisé pour définir son rôle dans la structure du niveau.
/// </summary>
public enum RoomType
{
    /// <summary>
    /// Type par défaut pour une salle non encore classifiée.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Salle de départ du joueur. Point d'origine de l'exploration.
    /// </summary>
    Start,

    /// <summary>
    /// Salle de boss principal. Termine généralement le niveau.
    /// </summary>
    Boss,

    /// <summary>
    /// Contient un coffre ou une récompense précieuse.
    /// </summary>
    Treasure,

    /// <summary>
    /// Contient un marchand. Le joueur peut y acheter des objets.
    /// </summary>
    Shop,

    /// <summary>
    /// Salle secrète, accessible de manière non évidente.
    /// </summary>
    Secret,

    /// <summary>
    /// Salle avec un mini-boss ou un combat spécial.
    /// </summary>
    MiniBoss,

    /// <summary>
    /// Salle à défi optionnel avec récompense à la clé.
    /// </summary>
    Challenge,

    /// <summary>
    /// Salle de repos, sans ennemis. Peut contenir un soin ou un checkpoint.
    /// </summary>
    Rest,

    /// <summary>
    /// Salle de sortie du niveau, potentiellement différente du boss.
    /// </summary>
    Exit
}