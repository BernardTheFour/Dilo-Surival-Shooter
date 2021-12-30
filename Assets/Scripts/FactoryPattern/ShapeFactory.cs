using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeFactory
{
    //use getShape method to get object of type shape 
    public Shape getShape(String shapeType)
    {
        if (shapeType == null)
        {
            return null;
        }
        if (shapeType.Equals("CIRCLE", StringComparison.OrdinalIgnoreCase))
        {
            return new Circle();

        }
        else if (shapeType.Equals("RECTANGLE", StringComparison.OrdinalIgnoreCase))
        {
            return new Rectangle();

        }
        else if (shapeType.Equals("SQUARE", StringComparison.OrdinalIgnoreCase))
        {
            return new Square();
        }

        return null;
    }
}
