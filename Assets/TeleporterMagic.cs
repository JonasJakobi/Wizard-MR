using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TeleporterMagic : MonoBehaviour
{
    [Header("References")]

    /// <summary>
    /// Prefab from which to spawn the spell instance
    /// </summary>
    public GameObject teleportPreviewPrefab;
    
    [SerializeField]
    protected HandPositionInfo hand;

    [SerializeField]
    private Transform playerPosition;

    [SerializeField]
    private Vector3 teleportPreviewSpawnPoint;

    /// <summary>
    /// Reference to the spawned lightball
    /// </summary>
    protected GameObject spellInstance;
    [SerializeField]
    //Internal Variables
    /// <summary>
    /// How long the player has his hand in the right position for the light spell (open palm, fingers together)
    /// </summary>
    private float timeSinceSpellAllowed = 0;
    [SerializeField]
    /// <summary>
    /// How long the player has his hand in the wrong position if previously in the right position for the light spell (open palm, fingers together)
    /// </summary>
    private float timeSinceSpellNotAllowedAnymore = 0;


    [Header("Variables to tweak")]
    [SerializeField]
    private float spawnDelay = 0.2f;
    [SerializeField]
    private float despawnTime = 0.2f;

    [SerializeField]
    private float teleportTime = 2.2f;
    [SerializeField]
    private float afterTeleportCD = 0.5f;
    
    bool waitingForTeleportCD = false;

    // Update is called once per frame
    void Update()
    {
        if(waitingForTeleportCD) return;
        //Players hand is right:
        if(ConditionForSpell()){
            timeSinceSpellAllowed += Time.deltaTime;
            timeSinceSpellNotAllowedAnymore = 0;

            //update pos if exists
            if(spellInstance != null){
                UpdatePreviewPos();
            }
        }
        else{
            timeSinceSpellNotAllowedAnymore += Time.deltaTime; 
            
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
        if(timeSinceSpellAllowed > teleportTime){
            Teleport();
        }
    }
    private void Teleport(){
        playerPosition.position = spellInstance.transform.position;
        DeleteMagic();
        timeSinceSpellAllowed = 0;
        StartCoroutine(TeleportCD());
    
    }
    private IEnumerator TeleportCD(){
        waitingForTeleportCD = true;
        yield return new WaitForSeconds(afterTeleportCD);
        waitingForTeleportCD = false;
    }


    protected bool ConditionForSpell(){
        return hand.CanCastTeleport();
    }
    
    protected void TrySpawnMagic(){
        if(spellInstance != null){
            return;
        }
        //See if we can spawn the preview
        Vector3? v = CastRayOutOfIndexFinger();
        if(v != null){
            teleportPreviewSpawnPoint = v.Value;
            spellInstance = Instantiate(teleportPreviewPrefab, teleportPreviewSpawnPoint, Quaternion.identity);
        }
    }
    protected void DeleteMagic(){
        if(spellInstance != null){
                Destroy(spellInstance);
                spellInstance = null;
            }
        
    }
    protected void UpdatePreviewPos(){
        if(spellInstance == null){
            return;
        }
        Vector3? v = CastRayOutOfIndexFinger();
        if(v != null){
            spellInstance.transform.position = v.Value;
        }
    }
    private Vector3? CastRayOutOfIndexFinger(){
        Transform indexFingerTip = hand.GetFingerTip(HandPositionInfo.Finger.Index);
        RaycastHit hit;
        if(Physics.Raycast(indexFingerTip.position, hand.wrist.forward, out hit)){
            return hit.point;
        }
        else{
            return null;
        }
    }



}
