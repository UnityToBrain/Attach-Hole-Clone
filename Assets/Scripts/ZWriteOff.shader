Shader "Unlit/ZWriteOff"
{
   
    SubShader
    {
        Tags { "RenderType"="Opaque" }
       
        Pass
        {
           ZWrite Off  
        }
    }
}
