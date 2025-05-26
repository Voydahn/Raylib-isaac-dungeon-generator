# 🧩 IsaacDungeonGenerator

A demo of a visual dungeon room generation tool inspired by games like *The Binding of Isaac*. This application generates and displays randomized dungeon layouts with various room types, using Raylib for rendering and C# for the logic.

## 🎮 Features

- Procedural generation of connected rooms with customizable rules.
- Visual rendering using [Raylib-cs](https://github.com/ChrisDill/Raylib-cs).
- Dynamic placement of special rooms like:
  - Start
  - Boss
  - Treasure
  - Shop
  - MiniBoss
  - Challenge
  - Rest
- Interactive key to regenerate new layouts (`R`).

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Raylib-cs (included via NuGet or manually referenced)
- A C# IDE like Visual Studio, JetBrains Rider, or VS Code.

### 📦 Build and Run

```bash
dotnet build
dotnet run --project IsaacDungeonGenerator
```

> Use the `R` key to regenerate a new dungeon layout in the window.

## 🧠 Project Structure

```
IsaacDungeonGenerator/
├── Core/              # Generation logic and data models
├── Game/              # Visual rendering and game loop
├── DTO/               # Placeholder for potential data exports
├── Program.cs         # Entry point
```

## ⚠️ Disclaimer

This is an unfinished application. It is provided **as-is**, primarily intended for portfolio demonstration purposes.

This project is currently a **work in progress** and is shared in its current state. It may not yet be production-ready.

## 📄 License

This project is shared for **learning and demonstration purposes only**. Feel free to explore and get inspired, but it's not licensed for commercial use.
