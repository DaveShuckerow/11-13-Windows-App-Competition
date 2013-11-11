# HexGenerator.py
from pygame import Surface, Color
import pygame.draw as Draw
import pygame.image as Image
import math

def renderHex(name, size = 64, color = Color(128,128,128), smooth = False, fill = False, dashed = False, width = 5):
    outscribeSize = size*2*math.tan(30*math.pi/180)
    surf = Surface((int(outscribeSize*2)+width*2,int(outscribeSize*2)+width*2))
    surf.fill(Color(0,0,0))
    points = []
    for deg in range(0,360,60):
        rad = math.pi/180 * deg
        points.append((outscribeSize+width+math.cos(rad)*(outscribeSize),
                       outscribeSize+width+math.sin(rad)*(outscribeSize)))
    if smooth:
        Draw.aalines(surf, color, True, points)
    else:
        Draw.lines(surf, color, True, points)
    Image.save(surf,name)
    print ("Hex {} rendered.  Inscribed radius: {}".format(name,outscribeSize*math.cos(30*math.pi/180)))
    
renderHex("Default Hex.png")
renderHex("Smooth Hex.png", smooth=True)
