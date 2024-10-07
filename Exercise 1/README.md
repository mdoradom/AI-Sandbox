# Exercise 1

> In this lab, you will create a Unity scene featuring two agents with different movement behaviors. The goal is to implement and demonstrate patrolling and wandering movements for AI agents.

## Scene Setup

### 1. Agents:

- **Agent 1 (Patrolling Movement) 🗺️:**
    - Implement patrolling behavior using waypoints.
    - Apply the ghost smoothing technique for smooth transitions.
    - The initial point and direction should be randomly selected.

- **Agent 2 (Wandering Movement) 🌪️:**
    - Implement wandering behavior where the agent moves unpredictably within the scene.

### 2. Deliverables:

- **Unity Package 📦**: Include the complete Unity project package with your scene and scripts.

- **Video 📹**: Record a video showcasing the behavior of both agents in the scene.

Ensure that both agents' movements are clearly visible and that the scene demonstrates the implemented behaviors effectively.

## The Exercise

You can download the Unity Package in [this link](test.com). It should be a small castle scene with two ncps that implement the two different movements behaivors. 

1. **The first one, _NPC (1)_** should go throug the castle gate, the go up over the walls and keep patrolling over there. This agent has 4 waypoints in the towers, and keeps patrolling them.

2. **The second, _NPC (2)_** should keep moving around the castle endlessly. This one implements the wandering behaivor with a path.

### Controls

- **Movement:** *WASD* to move the camera
- **Up / Down:** *SPACE* & *CTRL* to go up and down
- **Sprint:** *SHIFT* to move faster

### Requeriments

- Unity 2022.3.33f1 
- Project with URP Core
- AI Navigation package installed

### Acknowledgments

- 3D models are made by [Kenney](https://kenney.nl/).
- Skybox made by [Render Knight](https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353). Via [Unity Asset Store](https://assetstore.unity.com).
- Bézier Path Creator tool made by [Sebastian Lague](https://assetstore.unity.com/packages/tools/utilities/b-zier-path-creator-136082). Via [Unity Asset Store](https://assetstore.unity.com).
