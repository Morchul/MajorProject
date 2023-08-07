# MajorProject
The major project for my BSc in Games Programming

**How to combine the strengths of current Artificial Intelligence techniques and extend them to support non-interruptible actions.**

## Abstract
During this project the strengths and weaknesses of current Artificial Intelligence (AI) techniques were determined using several reputable sources. A new AI structure was then created, based on the AI agent model (Russel and Norvig, 2020) and with an additional fourth layer between decision and actuation as discussed in the GDC (2017) talk. Each layer was assigned one AI technique based on which strength best suited the needs of the layer. To test the created structure, an environment was created with a basic AI in it. Observation, white-box and unit testing confirmed the correct functionality of the AI in the environment. The structure's value in industry use was determined through qualitative analysis.
Building on above work, a structure has been created that consists of an outer framework to define how each layer interacts with each other. The inner structure contains the implementation of each layer by one AI technique. Each layer implementation can be viewed as a module and be easily exchanged with a different AI technique. This emphasises the single responsibility principle and allows the developer to make a wide range of individual adjustments by using the framework of the structure. By separating the actuation from the AI and putting it on the agent, the system also supports non-interruptible actions without interfering with the AI logic.
