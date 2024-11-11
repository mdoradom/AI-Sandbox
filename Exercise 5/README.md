# 🕵️ Exercise 5: Robber Behavior with Behavior Trees (4%)

## Objective

> In this lab, you will recreate the robber behavior shown in the video on Slide 9 of the "Decision Making" section using Behavior Bricks (an implementation of Behavior Trees). This exercise aims to build your understanding of Behavior Trees and their applications in AI for character behavior in Unity.
Setup and Options

To complete this lab, follow these steps to implement the robber's behavior using Behavior Bricks:

1. Behavior Bricks Setup
    - Use the Behavior Bricks asset in Unity to design a Behavior Tree that captures the robber’s behavior as shown in the provided video. Focus on creating an organized tree structure with appropriate conditions and actions.

2. State Representation
    - Implement the robber’s behavior transitions between states, including idle, approach, steal, and hide. Each state should trigger based on specific conditions, such as the cop's proximity.

## Deliverables:

1. **Unity Project Folder 📁**

Submit the entire Unity project folder, ensuring all relevant Behavior Bricks configurations, scripts, and assets are included.

2. **Video Demonstration 📹**

- Record a video (unedited) demonstrating the behavior in action. The video should clearly:
    - Show the robber’s transitions between each state.
    - Demonstrate how each state responds dynamically to changes in the environment (e.g., when the cop approaches or moves away).
- Submit your deliverables through a Google Drive or GitHub link.
 
## The Exercise

In this exercise, we refractored the previous exercise to wrap all the logic of the zombie detection and player movement to be controller via a FSM.

### Controls

- Movement: WASD to move the camera
- Up / Down: SPACE & CTRL to go up and down
- Sprint: SHIFT to move faster

### Requeriments

- Unity 2022.3.33f1
- Project with URP Core
- AI Navigation package installed
    
### Autors
 - [Mario Dorado Martínez](https://github.com/mdoradom)
 - [Marta Jover valero](https://github.com/MartaGnarta)
    
