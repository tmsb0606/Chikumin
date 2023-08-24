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
            // ��1�p�X�ł͉�ʏ�ɐF�͓h�炸�A�X�e���V���̍\�z�������s��
            ColorMask 0
            ZWrite Off

            // �O�ʁE�w�ʗ����ɂ��ď������s��
            Cull Off

            // ���łɕ`�悳�ꂽ�s�����I�u�W�F�N�g�\�ʂ̂����A���̃I�u�W�F�N�g��
            // ��܂�Ă���̈悪�X�e���V���l1�ɂȂ�悤�ɂ���
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

            // ��2�p�X�ŁA�X�e���V���l��0�łȂ��̈�ɂ����F��h��
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