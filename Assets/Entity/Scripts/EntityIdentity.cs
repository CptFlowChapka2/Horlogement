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
        ColorMain = ((Dictionary<string,Material>)holder.entityIdentity[tempIdentity]["color"])["main"];
        ColorTrail = ((Dictionary<string,Material>)holder.entityIdentity[tempIdentity]["color"])["trail"];
        DefaultDirection =(Vector3)holder.entityIdentity[tempIdentity]["Vector"];
        SoundBounce = ((Dictionary<string,AudioClip>)holder.entityIdentity[tempIdentity]["sound"])["bounce"];
        SoundFuse = ((Dictionary<string,AudioClip>)holder.entityIdentity[tempIdentity]["sound"])["fuse"];
    }
    public Vector3 DefaultDirection { get; set; }

    public identityKeys IdentityKey { get; set; }

    public Material ColorTrail { get; set; }
    public Material ColorMain { get; set; }

    public AudioClip SoundBounce { get; set; }
    public AudioClip SoundFuse { get; set; }

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

        return (identityKeysArray[0], identityKeysArray[1]) switch
        {
            //A= B+C or C+B or E+D or D+E or F+G G+F 
            (identityKeys.color1, identityKeys.color2) or (identityKeys.color2, identityKeys.color1) 
                or (identityKeys.color4,identityKeys.color3)or (identityKeys.color3,identityKeys.color4)
                or (identityKeys.color6,identityKeys.color5)or(identityKeys.color5,identityKeys.color6)=> identityKeys.color0,
            //B=C+D
            (identityKeys.color0, identityKeys.color2) or (identityKeys.color2, identityKeys.color0) 
                or (identityKeys.color5,identityKeys.color3)or (identityKeys.color3,identityKeys.color5)
                or (identityKeys.color4,identityKeys.color6)or(identityKeys.color6,identityKeys.color4)=> identityKeys.color1,
            //C=D+E
            (identityKeys.color0, identityKeys.color1) or (identityKeys.color1, identityKeys.color0) 
                or (identityKeys.color3,identityKeys.color6)or (identityKeys.color6,identityKeys.color3)
                or (identityKeys.color4,identityKeys.color5)or(identityKeys.color5,identityKeys.color4) => identityKeys.color2,
            //D=E+F
            (identityKeys.color6, identityKeys.color0) or (identityKeys.color0, identityKeys.color6) 
                or (identityKeys.color4,identityKeys.color1)or (identityKeys.color1,identityKeys.color4)
                or (identityKeys.color5,identityKeys.color2)or(identityKeys.color2,identityKeys.color5)=> identityKeys.color3,
            //E=F+G
            (identityKeys.color1, identityKeys.color3) or (identityKeys.color3, identityKeys.color1) 
                or (identityKeys.color5,identityKeys.color1)or (identityKeys.color1,identityKeys.color5)
                or (identityKeys.color6,identityKeys.color2)or(identityKeys.color2,identityKeys.color6) => identityKeys.color4,
            //F=G+A
            (identityKeys.color4, identityKeys.color1) or (identityKeys.color1, identityKeys.color4) 
                or (identityKeys.color1,identityKeys.color6)or (identityKeys.color6,identityKeys.color1)
                or (identityKeys.color3,identityKeys.color2)or(identityKeys.color2,identityKeys.color3) => identityKeys.color5,
            //G=A+B
            (identityKeys.color5, identityKeys.color0) or (identityKeys.color0, identityKeys.color5) 
                or (identityKeys.color3,identityKeys.color1)or (identityKeys.color1,identityKeys.color3)
                or (identityKeys.color2,identityKeys.color4)or(identityKeys.color4,identityKeys.color2) => identityKeys.color6,
            _ => a
        };

    }
}
