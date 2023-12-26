using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Toggle_Individual : MonoBehaviour
{
  [SerializeField] public string highlightStyle = "Toggle";
  [SerializeField] public bool individualObjOnly = false;
  [SerializeField] public bool toggleAllOn = false;
  [SerializeField] public bool toggleBackwards = false;
  public Material newMaterialRef;
  public Material tumourMaterial;
  public Material opticMaterial;
  public Material carotidMaterial;
  public Material tumourOpaque;
  public Material opticOpaque;
  public Material carotidOpaque;
  private bool isVisible = false; // Start as false
  private int count;
  private bool toggleAllBool;


  private void Start(){
    ToggleToStart(false);
  }

  private void Update()
  {
    if (Keyboard.current.spaceKey.wasPressedThisFrame){
       if(highlightStyle == "Toggle"){
         if(toggleAllOn){
           ToggleAll(toggleAllBool);
           toggleAllBool = !toggleAllBool;
           print(toggleAllBool);
         }
         else if (count >=3){
           count = 0;
           ToggleToStart(false);
         }
         else if (isVisible && count >= 3){
           count = 0;
         }
         /*else if (isVisible && count < 3) // This is for when you want to turn the AR overlay off with each press
         {
             ToggleVisibility(false); // Tell ToggleVisibility to be false
             isVisible = false;
         }*/
         else
         {

             if(individualObjOnly){
               count++;
               ToggleVisibilitySingleObj();
             }
             /*else if (toggleBackwards)
             {
               count--;
               ToggleVisibilityBackwards();
             }*/
             else
             {
               count++;
               ToggleVisibility(true);
             }
             isVisible = true;
           }
         }

       else if
       (highlightStyle == "Colour")
       {
         if (count >=3){
           count = 0;
           AllTransparentWhite();
         }
         else if (isVisible && count >= 3){
           count = 0;
         }
         else
         {
             count++;
             ToggleColour(true);
             isVisible = true;
         }
       }
       else if (highlightStyle == "Opacity")
       {
         ToggleAll(true);
         if (count == 0){
           ToggleTransparency(true);
           count++;
           //print("Count ==0: " + count);
         }
         else if (count > 3){
           print(count);
           //ToggleTransparency(true);
           count = 0;
           TransparencyToNormal();
           count++;
           //print("Count >3 : " + count);
         }
         else
         {
           print(count);
           ToggleTransparency(true);
           count++;
           //print("else condition: " + count);
         }
       }
       else {
         count = 0;
         ToggleToStart(true);
         //print("Greater Else" + count);
       }
     }
   }

   private void ToggleVisibility(bool toggle){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   for (int i = 0; i <= count-1; i++){
     if (toggle)
     {
       renderers[i].enabled = true;
     }
     else
     {
       // Toggle is not true so it must be false
       renderers[i].enabled = false;
     }
   }
 }

 /*private void ToggleVisibilityBackwards()
 {
     Renderer[] renderers = GetComponentsInChildren<Renderer>();

     if (count >= 0)
     {
         // Enable the renderer of the child object at the current index
         renderers[count].enabled = true;
         count--; // Decrement the index for the next call
     }

     if (count < 0)
     {
         // If all objects have been rendered, reset the index to the last one
         count = renderers.Length - 1;
     }
 }*/

 private void ToggleVisibilitySingleObj(){
 Renderer[] renderers = GetComponentsInChildren<Renderer>();
 for (int i = 0; i <= count-1; i++)
 {
   if (i == 0)
   {
     renderers[i].enabled = true;
     renderers[i+2].enabled = false;
   }
   else if(i == 1){
     renderers[i].enabled = true;
     renderers[i-1].enabled = false;
     //print("i: " + i);
   }
   else if (i == 2){
     renderers[i].enabled = true;
     renderers[i-1].enabled = false;
     //print("i: " + i);
   }
   else {
     ToggleAll(true);
   }
 }
}

 private void ToggleToStart(bool On){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   foreach (Renderer renderer in renderers){
     renderer.enabled = On;
     count = 0;
   }
 }

 private void ToggleAll(bool On){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   if(On) {
     foreach (Renderer renderer in renderers)
     {
       renderer.enabled = true;
     }
   } else {
     foreach (Renderer renderer in renderers)
     {
       renderer.enabled = false;
     }
   }
 }

 private void AllTransparentWhite(){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   foreach (Renderer renderer in renderers){
     renderer.material = newMaterialRef;
     renderer.enabled = true;
   }
 }

 private void ToggleColour(bool ToggleColour){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   for (int i = 0; i <= count-1; i++)
   {
     if (i == 0)
     {
       renderers[i].material = tumourMaterial;
     }
     else if(i == 1){
       renderers[i].material = carotidMaterial;
     }
     else if (i == 2){
       renderers[i].material = opticMaterial;
     }
     else {
       return;
     }
   }
 }

 private void ToggleTransparency(bool ToggleTransparency){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   for (int i = 0; i <= count-1; i++)
   {
     if (i == 0)
     {
       renderers[i].material = tumourOpaque;
       renderers[i+2].material = opticMaterial;
       //print("i: " + i);
     }
     else if(i == 1){
       renderers[i].material = carotidOpaque;
       renderers[i-1].material = tumourMaterial;
       //print("i: " + i);
     }
     else if (i == 2){
       renderers[i].material = opticOpaque;
       renderers[i-1].material = carotidMaterial;
       //print("i: " + i);
     }
     else {
       TransparencyToNormal();
     }
   }
 }

 private void TransparencyToNormal(){
   Renderer[] renderers = GetComponentsInChildren<Renderer>();
   for (int i = 0; i <= renderers.Length-1; i++)
   {
     if (i == 0)
     {
       renderers[i].material = tumourMaterial;
     }
     else if(i == 1){
       renderers[i].material = carotidMaterial;
     }
     else if (i == 2){
       renderers[i].material = opticMaterial;
     }
     else {
       return;
     }
   }
 }


}
