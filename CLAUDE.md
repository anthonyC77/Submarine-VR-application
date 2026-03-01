# CLAUDE.md

## Personal information
- ** Anthony
- ** Développeur senior C# depuis 2007 et chef de projet tchnique sur des applications .Net depuis les WebForms et Winforms jusqu'à .Net Core 8 et 9 et Angular. Ayant réalisé une application Unity déployé sur mon casque Oculus Quest 1.


This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity 2019.4.2f1 VR application targeting the **Oculus Quest** (Android). It is an educational/therapeutic experience combining musical interaction, planetary exploration, an underwater Neptune scene, fractal visualization, and EMDR therapy support.

**Build target:** Android (Oculus Quest) via Unity Editor
**Build pipeline:** Use Unity Editor → File → Build Settings → Android → Build
**Deploy to device:** `adb install <build>.apk` or use Oculus Developer Hub

## Scene Structure (Build Order)

| Index | Scene | Purpose |
|-------|-------|---------|
| 0 | `Assets/Scenes/Menu.unity` | Main menu with VR pointer navigation |
| 1 | `Assets/Scenes/Main.unity` | Kikongi music instrument + planetary system |
| 2 | `Assets/Scenes/Neptune/Neptune.unity` | Underwater world (fish, dolphin, cleanup, fractal) |
| 3 | `Assets/Scenes/Start.unity` | Intro/splash screen |

## Architecture Overview

### Shared Infrastructure

- **`TagNames.cs`** — Central `Names` static class with all GameObject tag/name string constants. Always use these instead of inline strings.
- **`Helper.cs`** — Static utility library: note-to-planet mapping, material/color lookup, enum conversion, swimming state.
- **`HelperController.cs`** — Gameplay state manager.
- **`eNote.cs`** / **`ePlanet.cs`** / **`ePositionType.cs`** — Core enumerations shared across all systems.

### System: Kikongi Music Instrument (`Assets/Scripts/Kikongi/`)

A virtual marimba/xylophone with **record and playback** via the Command pattern:

- **`ICommand.cs`** — Interface with `Execute()`, `DatePlay`, `HitVol`.
- **`PlayNoteKikongiCommand.cs`** — Plays a note with velocity-based volume and pitch randomization. Serializable for XML save.
- **`CommandManager.cs`** (Singleton `MonoBehaviour`) — Manages the command buffer; coordinates `Start()` / `Stop()` recording, `Play(id)` / `Rewind()` playback as coroutines, and `SaveInFile()` → XML.
- **`SavePlayInFile.cs`** — XML serialization/deserialization of command sequences.
- **`Recorder.cs`** — UI state machine (ChooseRead / Rec / Stop) driving button clickability.
- **`GrabMailloche.cs`** — Extends `OVRGrabbable`; custom mallet grab logic.
- **`MaillocheMovement.cs`** — Mallet physics and collision-to-command dispatch.

### System: Planetary Scene (`Assets/Scripts/Planet*.cs`)

Eight planets that respond to musical notes and physics collisions:

- **`Planet.cs`** — Per-planet behavior: rotation around the sun, wall transparency fade, position-mode transitions (INIT → WALL → SPACE → AROUNDSUN).
- **`Planets.cs`** — Collection manager for all planets.
- **`PlanetPosition.cs`** — Computes world position from `ePlanet` + `ePositionType`.
- `Helper.cs` maps `eNote` values to specific `ePlanet` values.

### System: Neptune Underwater Scene (`Assets/Scripts/Neptune/`)

Several independent subsystems:

- **Fish flocking:** `FlockManager.cs` spawns N fish; `Flock.cs` runs per-fish steering. `DOTS/FlockComponents.cs` contains ECS component structs prepared for a future high-performance rewrite.
- **Dolphin AI:** `PlayWithDolphin.cs` alternates the dolphin between two play zones with smooth lerp movement.
- **Player swimming:** `Swim.cs` detects both hands touching a swim-ball and moves the player toward it.
- **Garbage cleanup:** `CatchGarbages.cs` counts collected trash objects and triggers particle/audio effects when the quota is reached; activates/deactivates objects in response.
- **Dynamite sequence:** `DynamiteExplodes.cs` manages before/explosion/after visual states and rock destruction.
- **Fractal journey:** `FractalJourney/Explorer.cs` drives a shader-based fractal visualization via `OVRInput`; updates material parameters (zoom, pan, rotation) with smooth lerp.
- **EMDR therapy:** `CommandEMDR.cs` / `ToEMDR.cs` — eye-movement therapy activation/switching.
- **Kikongi variant:** `KikongiTwoFaces.cs` / `MaillocheKikongiTwoFaces.cs` — a two-faced Kikongi instrument using `eNotes2Faces` (16 positions across 2 faces × 8 notes).

### Input & VR Integration

- **`VRInput.cs`** — Wraps `OVRInput` (index trigger, thumbstick, face buttons) for menu interaction.
- **`PhysicPointer.cs`** — Raycast-based VR pointer for menu buttons.
- All grabbable objects extend **`OVRGrabbable`**; hands use `OVRGrabber`.
- Hand tags: `Names.HAND`, `Names.HANDLEFT`, `Names.HANDRIGHT`.

### Audio

- **`MyAudioManager.cs`** / **`MySound.cs`** — Custom audio manager.
- Oculus OSP (spatial audio) integration via `Assets/Oculus/AudioManager/`.
- Note playback uses `AudioSource.pitch` randomization: `lowPitchRange = 0.75f`, `highPitchRange = 1.5f`.

## Key Conventions

- All tag/name string lookups go through the `Names` static class in `TagNames.cs`.
- Singletons (e.g. `CommandManager`) are set in `Awake()` via `instance = this`.
- Scene transitions use `LevelLoader.cs` / `MainMenu.cs` by scene name string.
- French is used in commit messages and some variable names (e.g. `Joueur`, `mailloche`).
