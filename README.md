# Stack Dash

Hyper-casual WebGL endless runner built in Unity 6 (URP).  
Collect platforms, bridge gaps, avoid obstacles. How long can you survive?

**▶ Play it now:** [hassaanwain.itch.io/stackdash](https://hassaanwain.itch.io/stackdash)

## Tech
- Unity 6, URP
- New Input System
- Object Pooling
- ScriptableObject-driven difficulty
- Procedural level generation
- Event-driven architecture

## Architecture
- GameManager singleton state machine (Menu/Playing/GameOver)
- Data/visual separation on stack system
- Segment base class with GroundSegment and GapSegment
- Bridge-building gap mechanic
- DifficultyConfig ScriptableObject with AnimationCurves
