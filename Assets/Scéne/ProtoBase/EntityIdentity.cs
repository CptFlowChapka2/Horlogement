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
        Color = (Color)dataHolder.entityIdentity[tempIdentity]["color"];
        DefaultDirection =(Vector3)dataHolder.entityIdentity[tempIdentity]["Vector"];
        Sound = (AudioClip)dataHolder.entityIdentity[tempIdentity]["sound"];
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
            case (identityKeys.color1,identityKeys.color0)or (identityKeys.color0,identityKeys.color1):
                return identityKeys.color2;
            
            case (identityKeys.color2,identityKeys.color1)or (identityKeys.color1,identityKeys.color2):
                return identityKeys.color0;
            case (identityKeys.color2,identityKeys.color0)or(identityKeys.color0,identityKeys.color2) :
                return identityKeys.color1;
            //todo : pour 7 identity c'est chiant
        }
        

    }
}
