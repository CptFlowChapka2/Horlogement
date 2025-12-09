using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EntityIdentity
{
    private Vector3 defaultDirection;
    private Color color;
    private AudioClip sound;
    private DataHolder holder;


    public void Create(DataHolder dataHolder,Color key,Color key2=default)
    {
        holder = dataHolder;
        Color tempColor = MergeColor(key,key2);
        Color = (Color)dataHolder.entityIdentity[tempColor]["color"];
        DefaultDirection =(Vector3)dataHolder.entityIdentity[tempColor]["Vector"];
        //Sound = (AudioClip)dataHolder.entityIdentity[tempColor]["sound"];
    }
    public Vector3 DefaultDirection
    {
        get => defaultDirection;
        set => defaultDirection = value;
    }
    
    public Color Color
    {
        get => color;
        set => color = value;
    }
    public AudioClip Sound
    {
        get => sound;
        set => sound = value;
    }

    public Color MergeColor(Color a,Color b=default)
    {
         

        if (b == default)
        {
            return a;
        }

        int[] chosenMerge =ChooseColorMerge(a,b) ;

        switch (chosenMerge[0],chosenMerge[1])
        {
            default:
                return a;
            
            case (2,2) or(1,1)or(0,0) :
                return a;
            case (1,0)or (0,1):
                return holder.allColor[2];
            
            case (2,1)or (1,2):
                return holder.allColor[0];
            case (2,0)or(0,2) :
                return holder.allColor[1];
            //todo : pour 7 identity c'est chiant
        }
        

    }

    private int[] ChooseColorMerge(Color a, Color b)
    {
        int[] toReturn=new []{0,0};
        if (a==holder.allColor[0])
        {
            toReturn[0] = 0;
        }
        else if (a==holder.allColor[1])
        {
            toReturn[0] = 1;
        }
        else if (a==holder.allColor[2])
        {
            toReturn[0] = 2;
        }
        if (b==holder.allColor[0])
        {
            toReturn[1] = 0;
        }
        if (b==holder.allColor[1])
        {
            toReturn[1] = 1;
        }
        if (b==holder.allColor[2])
        {
            toReturn[1] = 2;
        }

        return toReturn;
    }

    private Vector3 ColorToVector(Color a)
    {
        Vector3 colorAsVector = new Vector3(a.r,a.b,a.g);
        return colorAsVector;
    }
    
}
