# AI-Tribes
Artificial Intelligence experiments for an AI course

#### Writeup here because don't have time

Path Following:
    We used an object called Patrol Path. If you select it in the scene, it highlights a path that the humans used to follow while looking for food.

Path Finding:
    The animals use A* path finding with a navmesh to find a path to wherever their current target is.

Decision Tree:
    The humans use a very basic decision tree to decide what functions to call in very basic scenarios. Its datafile is Assets/humanDT.txt if you'd like to take a look. Basically the three choices are "Predators visible", "Returning to Home", "Food Found".

Behaviour Tree:
    We tried to use RAIN to implement a decision tree, and while we got the logic in place, we could not make RAIN actually do anything once a choice was made.
    
    [NEED DIAGRAM]

State Machine:
    The animals currently operate on a state machine using Unity's built-in Mecanim state machine meant for animations. The state machine was used to determine when to call what scripts.
    
    [DIAGRAMMMS]

Bayes:
    BAYES

Genetic Evolution:
    We did genetic evolution a little wrong. Instead of having a pool of genes to pick from, our prey class evolved by having two prey in-game find each other and produce an offspring with genes picked and mutated between the two of them. Its a much slower and much less useful process, but it is interesting to see develop in the world. 
        Thresholds: 
            Strength, Forward speed, Turn speed, Sight distance, Eating speed, Max health.
        Population size:
            about 30 in the world at a time.
        Bits used in chromosomes?
            As many bits as there are in a float
        Phenotype range?
            between -1% to +1% of the original value to mutate.
        
        What happened when you evolved each separately?
            Things evolved significantly slower. No optimal values were found within a reasonable amount of time.
        What happened 
