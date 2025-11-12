Summary of Project Modifications and New Features

To expand on the tutorial's base game, I added two main elements: a visual decoration to make the scene richer, and a new gameplay mechanic that increases difficulty.

1. Decorative Element: Imported Tree
I wanted the scene to feel more alive, so I imported a 3D tree from Free3D.com using Unity's Import New Asset option. When I first placed it in the scene, the Game View turned brown and stopped rendering properly. After checking the imported object's hierarchy, I found it had its own active Camera component, which was conflicting with the Main Camera. Removing that component fixed the issue and restored the normal view.

2. New Gameplay Mechanic: The "Dog" Enemy
To make gameplay less repetitive, I introduced a dog character that the player must avoid hitting. If the player accidentally shoots it with hay, the game ends.

For the setup, I used a dog model from an imported asset pack. Its material appeared pink due to shader issues, which I fixed by switching its material to the Standard shader. I then created a "Dog Container" GameObject as the main parent, adding the Sheep.cs script, Rigidbody, and Box Collider to it.

Initially, the dog didn't move correctly because its Animator components conflicted with the movement script, so I disabled these. Then, I noticed it ran in the opposite direction, so I rotated the child model 180° on the Y-axis and adjusted the Sheep.cs script to move using Vector3.back when the object's Tag is "Dog."

To make it appear in-game, I modified the SheepSpawner script by adding a dogPrefab variable, to which I assigned my new Dog Container prefab via the Inspector. I then used a random check to make the dog appear roughly one-fifth of the time.

Finally, I implemented the dog's unique gameplay rules. I modified the collision logic in Sheep.cs so that if a hay bale hits an object tagged "Dog," the game immediately calls GameOver(). During playtesting, I also discovered a physics bug where dogs would fly erratically instead of falling. I fixed this by modifying the Drop() method to bypass physics for dogs, destroying them instantly. This change also solved a second bug, preventing fallen dogs from counting towards the dropped sheep limit.

Conclusion
These updates made the game more visually interesting and added a small but meaningful layer of strategy. The process also helped me practice debugging, prefab setup, and managing object behavior in Unity.