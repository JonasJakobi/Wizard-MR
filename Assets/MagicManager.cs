using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    [Header("References")]

    /// <summary>
    /// Prefab from which to spawn the spell instance
    /// </summary>
    public GameObject spellPrefab;
    
    [SerializeField]
    protected HandPositionInfo hand;

    [SerializeField]
    private Transform lightBallSpawnPoint;

    /// <summary>
    /// Reference to the spawned lightball
    /// </summary>
    protected GameObject spellInstance;

    //Internal Variables
    /// <summary>
    /// How long the player has his hand in the right position for the light spell (open palm, fingers together)
    /// </summary>
    private float timeSinceSpellAllowed = 0;
    /// <summary>
    /// How long the player has his hand in the wrong position if previously in the right position for the light spell (open palm, fingers together)
    /// </summary>
    private float timeSinceSpellNotAllowedAnymore = 0;


    [Header("Variables to tweak")]
    [SerializeField]
    private float spawnDelay = 0.1f;
    [SerializeField]
    private float despawnTime = 0.3f;
    


    // Update is called once per frame
    void Update()
    {
        //Players hand is right:
        if(ConditionForSpell()){
            timeSinceSpellAllowed += Time.deltaTime;
            timeSinceSpellNotAllowedAnymore = 0;

            //grow ball if it exists
            if(spellInstance != null){
                ShrinkOrExpandMagic(true);
            }
        }
        //players hand is wrong:
        else{
            //shrink ball if it already exists
            if (spellInstance != null){
                timeSinceSpellNotAllowedAnymore += Time.deltaTime;
                ShrinkOrExpandMagic(false);    
            }

        } 

        //Its been wrong too long, delete
        if (timeSinceSpellNotAllowedAnymore > despawnTime){
                timeSinceSpellAllowed = 0;
                timeSinceSpellNotAllowedAnymore = 0;
                DeleteMagic();
            }
        //Its been long enough correct, spawn the ball.
        if(timeSinceSpellAllowed > spawnDelay){
            TrySpawnMagic();
        }
        else{
            DeleteMagic();
        }
    }

    protected bool ConditionForSpell(){
        return hand.CanCastLight();
    }
    
    protected void TrySpawnMagic(){
        //Spawn light ball
            if(spellInstance == null){
                spellInstance = Instantiate(spellPrefab, lightBallSpawnPoint);
            }
    }
    protected void DeleteMagic(){
        if(spellInstance != null){
                Destroy(spellInstance);
                spellInstance = null;
            }
        
    }
    protected void ShrinkOrExpandMagic(bool expand){
        spellInstance.GetComponent<LightBall>().SetExpand(expand);  
    }

    public GameObject GetSpellInstance() {
        return spellInstance;
    }

}
