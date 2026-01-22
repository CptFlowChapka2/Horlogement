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
            (identityKeys.B, identityKeys.G) or (identityKeys.G, identityKeys.B) 
                or (identityKeys.E,identityKeys.D)or (identityKeys.D,identityKeys.E)
                or (identityKeys.G,identityKeys.F)or(identityKeys.F,identityKeys.G)=> identityKeys.A,
            //B=C+D
            (identityKeys.A, identityKeys.C) or (identityKeys.C, identityKeys.A) 
                or (identityKeys.F,identityKeys.D)or (identityKeys.D,identityKeys.F)
                or (identityKeys.E,identityKeys.G)or(identityKeys.G,identityKeys.E)=> identityKeys.B,
            //C=D+E
            (identityKeys.A, identityKeys.B) or (identityKeys.B, identityKeys.A) 
                or (identityKeys.D,identityKeys.G)or (identityKeys.G,identityKeys.D)
                or (identityKeys.E,identityKeys.F)or(identityKeys.F,identityKeys.E) => identityKeys.C,
            //D=E+F
            (identityKeys.G, identityKeys.A) or (identityKeys.A, identityKeys.G) 
                or (identityKeys.E,identityKeys.B)or (identityKeys.B,identityKeys.E)
                or (identityKeys.F,identityKeys.C)or(identityKeys.C,identityKeys.F)=> identityKeys.D,
            //E=F+G
            (identityKeys.A, identityKeys.D) or (identityKeys.D, identityKeys.A) 
                or (identityKeys.F,identityKeys.B)or (identityKeys.B,identityKeys.F)
                or (identityKeys.G,identityKeys.C)or(identityKeys.C,identityKeys.G) => identityKeys.E,
            //F=G+A
            (identityKeys.E, identityKeys.A) or (identityKeys.A, identityKeys.E) 
                or (identityKeys.B,identityKeys.C)or (identityKeys.C,identityKeys.B)
                or (identityKeys.D,identityKeys.C)or(identityKeys.C,identityKeys.D) => identityKeys.F,
            //G=A+B
            (identityKeys.F, identityKeys.A) or (identityKeys.A, identityKeys.F) 
                or (identityKeys.D,identityKeys.B)or (identityKeys.B,identityKeys.D)
                or (identityKeys.C,identityKeys.E)or(identityKeys.E,identityKeys.C) => identityKeys.G,
            _ => a
        };

    }
}
