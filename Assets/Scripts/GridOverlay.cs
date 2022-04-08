// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GridOverlay : MonoBehaviour
// {
//     private Material lineMaterial;

//     void CreateLineMaterial (){
//         if(!lineMaterial){
//             var shader  = Shader.Find("Hidden/Internal-Colored");
//             lineMaterial = new Material(shader);

//             lineMaterial.hideFlags = HideFlags.HideAndDontSave
//             lineMaterial.setInt('_SrcBlend', (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//             lineMaterial.setInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//             lineMaterial.setInt("_Zwirte", 0);
//             lineMaterial.setInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
//         }
//     }

//     private void OnDisable(){
//         DestroyImmediate(lineMaterial);
//     }

//     private void OnPostRender() {
//         CreateLineMaterial();
//         lineMaterial.SetPass(0);

//         GL.Begin(GL.LINES);



//         GL.End();    
//     }
// }
