# 🖼️ Fractals

The application is implemented on the WPF platform (my first project on this platform).  

This app can draw 5 fractals:
* Pythagorean Tree
* Koch curve
* Serpinsky Carpet
* Sierpinski Triangle
* Cantor set

Each fractal can be slightly changed using settings (you can change the recursion depth, zoom in/out, change the start/end color, you can change the angle of the branches of the Pythagorean tree, and so on) 

This is the first version of the project. There is a lot of incorrect code here, but I fixed it in another branch. But this version has been tested by several other people, so most likely it works without errors. 

P. S. You may have noticed that it would most likely be more convenient to pass information about the next iteration in the DrawFractal method. I didn't do this because the terms of reference stated that this function should have only 1 parameter - the depth of recursion (the project was done as homework)
