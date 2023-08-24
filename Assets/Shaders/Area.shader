Shader "Unlit/Area"
{
    Properties
    {
        _Color ("Color", Color) = (1.0, 0.0, 0.0, 0.5)
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" "Queue"="Geometry+1"}

        CGINCLUDE
        half4 _Color;
        float4 vert(float4 vertex : POSITION) : SV_POSITION
        { 
            return UnityObjectToClipPos(vertex);
        }
        half4 frag() : SV_Target
        {
            return _Color;
        }
        ENDCG

        Pass
        {
            // 第1パスでは画面上に色は塗らず、ステンシルの構築だけを行う
            ColorMask 0
            ZWrite Off

            // 前面・背面両方について処理を行う
            Cull Off

            // すでに描画された不透明オブジェクト表面のうち、このオブジェクトに
            // 包まれている領域がステンシル値1になるようにする
            Stencil
            {
                ZFailFront DecrWrap
                ZFailBack IncrWrap
            }
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
        Pass
        {
            Cull Front
            ZTest Always
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            // 第2パスで、ステンシル値が0でない領域にだけ色を塗る
            Stencil
            {
                Ref 0
                Comp NotEqual
            }
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    } 
}