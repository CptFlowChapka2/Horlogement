using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EntityIdentity
{
    private DataHolder holder;


    public void Create(DataHolder dataHolder,identityKeys key,identityKeys key2=default)
    {
        holder = dataHolder;
        identityKeys tempIdentity = MergeIdentity(key,key2);
        IdentityKey = tempIdentity;
        Color = (Color)holder.entityIdentity[tempIdentity]["color"];
        DefaultDirection =(Vector3)holder.entityIdentity[tempIdentity]["Vector"];
        Sound = (AudioClip)holder.entityIdentity[tempIdentity]["sound"];
    }
    public Vector3 DefaultDirection { get; set; }

    public identityKeys IdentityKey { get; set; }

    public Color Color { get; set; }

    public AudioClip Sound { get; set; }

    public identityKeys MergeIdentity(identityKeys a,identityKeys b=default)
    {
         

        if (b == default)
        {
            return a;
        }
        if (a == default)
        {
            return b;
        }

        identityKeys[] identityKeysArray ={a,b} ;

        switch (identityKeysArray[0],identityKeysArray[1])
        {
            default:
                return a;
            
            case (identityKeys.color2,identityKeys.color2) or(identityKeys.color1,identityKeys.color1)or(identityKeys.color0,identityKeys.color0) :
                return a;
            //A=B+C 
            case (identityKeys.color1,identityKeys.color2)or (identityKeys.color2,identityKeys.color1):
                return identityKeys.color0;
            //B=C+D
            case (identityKeys.color2,identityKeys.color3)or (identityKeys.color3,identityKeys.color2):
                return identityKeys.color1;
            //C=D+E
            case (identityKeys.color3,identityKeys.color4)or(identityKeys.color4,identityKeys.color3) :
                return identityKeys.color2;
            //D=E+F
            case (identityKeys.color4,identityKeys.color5)or(identityKeys.color5,identityKeys.color4) :
                return identityKeys.color3;
            //E=F+G
            case (identityKeys.color5,identityKeys.color6)or(identityKeys.color6,identityKeys.color5) :
                return identityKeys.color4;
            //F=G+A
            case (identityKeys.color6,identityKeys.color0)or(identityKeys.color0,identityKeys.color6) :
                return identityKeys.color5;
            //G=A+B
            case (identityKeys.color1,identityKeys.color0)or(identityKeys.color0,identityKeys.color1) :
                return identityKeys.color6;
            
        }
        

    }
}
