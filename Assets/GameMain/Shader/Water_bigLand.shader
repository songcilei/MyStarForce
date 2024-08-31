Shader "URP/Water_bigLand"
{
    Properties
    {
//        _MainTex ("Texture", 2D) = "white" {}
        _UVTile("UVTile",Float)=1
        _UVSpeed("UVSpeed",Vector)=(1,1,1,1)
        _NormalScale("NormalScale",Float)=1
        _Normal("Normal",2D)="bump"{}
        _DeepRange("DeepRange",Float)=10
        _DeepColor1("DeepColor1",Color)=(1,1,1,1)
        _DeepColor2("DeepColor2",Color)=(1,1,1,1)
        _ScatterIntensity("ScatterIntensity",Float)=1
        _FresnelColor("FresnelColor",Color)=(1,1,1,1)
        _FresnelAlpha("FresnelAlpha",Range(0,1))=1
        _ReflectAlpha("ReflectAlpha",Range(0,1))=1
        _ReflectMap("ReflectMap",2D)="black"{}
        _CausticsAlpha("CausticsAlpha",Range(0,1))=0.5
        _CausticsMap("_CausticsMap",2D)="black"{}
        _WhiteEdgeRamp("WhiteEdgeRamp",Range(0,1))=0
        _WhiteEdgePow("WhiteEdgePow",Float)=1
        _WhiteEdgeColor("WhiteEdgeColor",Color)=(1,1,1,1)
        _FoamSpeed("FoamSpeed",Float)=1
        _FoamClip("FoamRangeClip(d)",Float)=1
        _FoamRamp("FoamRamp",Float)=1
        _FoamWidth("FoamWidht",Range(0,1))=1
        _FoamTex("FoamTex",2D)="white"{}
        _SpecPow("SpecPow",Float)=1
        _SpecSize("SpecSize",Float)=1 
        [Toggle(_RECEIVE_SHADOWS)]  _ReceiveShadows ("Receive Shadows", Float) = 1  
        _ShadowColor("ShadowColor",Color)=(0,0,0,0)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "UniversalMaterialType" = "Lit"
            "IgnoreProjector" = "True"
        }
        LOD 100

        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma shader_feature _RECEIVE_SHADOWS  
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Water.hlsl"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 N : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos: TEXCOORD1;
                float4 screenPos : TEXCOORD4;
                float4 N : NORMAL;
                //half disMask:TEXCOORD3;
            };

            TEXTURE2D(_Normal);
            SAMPLER(sampler_Normal);
            float4 _Normal_ST;
            TEXTURE2D(_ReflectMap);
            SAMPLER(sampler_ReflectMap);
            float4 _ReflectMap_ST;
            TEXTURE2D(_CausticsMap);
            SAMPLER(sampler_CausticsMap);
            TEXTURE2D(_FoamTex);
            SAMPLER(sampler_FoamTex);
            float _CausticsAlpha;
            float4 _CausticsMap_ST;
            float4 _UVSpeed;
            float _UVTile;
            float _NormalScale;
            float4 _DeepColor1,_DeepColor2;
            float _ReflectAlpha;
            float _DeepRange;
            float4 _FresnelColor;
            float _FresnelAlpha;
            float _WhiteEdgeRamp;
            float _WhiteEdgePow;
            float4 _WhiteEdgeColor;
            float _FoamClip;
            float _FoamWidth;
            float _FoamRamp;
            float _FoamSpeed;
            float _SpecPow;
            float _SpecSize;
            float4 _ShadowColor;
            float4 _FoamTex_ST;
            float3 _LightDirection;

            float _ScatterIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                float4 worldPos = mul(UNITY_MATRIX_M,v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP,float4(worldPos.xyz,1));
                o.worldPos.xyz = worldPos.xyz;
                o.worldPos.w = ComputeFogFactor(o.vertex.z);
                o.uv = v.uv;
                o.N.xyz = mul(v.N,(float3x3)unity_WorldToObject);
                o.N.w = 1;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }
            float4 frag (v2f i) : SV_Target
            {

                float3 N = normalize(i.N.xyz);
                float3 L = normalize(_LightDirection.xyz);
                float3 V = normalize(_WorldSpaceCameraPos.xyz-i.worldPos.xyz);
                float3 H = SafeNormalize(L+V);

//Water Normal
                float2 RealUV1 = i.worldPos.xz / _UVTile + _Time.y*_UVSpeed.xy;
                float2 RealUV2 = i.worldPos.xz / _UVTile + _Time.y * 0.01*_UVSpeed.zw;
                float2 RealUV3 = i.worldPos.xz / _UVTile + _Time.y * 0.06 * _UVSpeed.zw;
                RealUV2 = RealUV2+RealUV3;
                float3 NormapMap1 = UnpackNormalScale(SAMPLE_TEXTURE2D(_Normal,sampler_Normal,i.uv *_Normal_ST.xy+_Normal_ST.zw+RealUV1),_NormalScale);
                float3 NormapMap2 = UnpackNormalScale(SAMPLE_TEXTURE2D(_Normal,sampler_Normal,i.uv *_Normal_ST.xy+_Normal_ST.zw+RealUV2),_NormalScale);
                float3 finalNormal = BlendNormal(NormapMap1,NormapMap2);
//Water Depth
        
                i.screenPos.xy+= finalNormal.xy;
                float3 worldPos = ReconstructWorldPosition_URP(i.vertex,i.screenPos);
//Water Color
                float rampRange = saturate(exp(-length(i.worldPos.xyz - worldPos.xyz)/max(_DeepRange,0)));
           
                float4 waterColor = lerp(_DeepColor1,_DeepColor2,1-rampRange);
//Water Shadow
                float4  shadowCoord =TransformWorldToShadowCoord(i.worldPos);
                half realtimeShadow = MainLightRealtimeShadow(shadowCoord);
                waterColor.rgb = lerp(waterColor.xyz*_ShadowColor.xyz,waterColor.xyz,saturate(realtimeShadow.rrr));
//Water Scatter
                float3 scatter= MF_ScatteringAquatic(V,N,_ScatterIntensity,N);//这里特殊化加工了下  正常来说是MF_ScatteringAquatic(V,L,_ScatterIntensity,N)
                waterColor.rgb += lerp(_DeepColor2.xyz,_DeepColor1.xyz,scatter)*realtimeShadow;//这里特殊化加工了下  正常来说是waterColor.rgb += lerp(_DeepColor1,_DeepColor2,scatter);

//Water Fresnel
                //tbn 优化算法
                float3 tbnN = float3(finalNormal.x,finalNormal.z,-finalNormal.y);
                float fresnel = pow(saturate(1-dot(N,V)),5)*_FresnelAlpha;
                float fresnelN = pow(saturate(1-dot(tbnN,V)),5)*_FresnelAlpha;
                float3 reflectL = normalize(reflect(-L,tbnN));
                float3 reflectVec = normalize(reflect(-V,N))*0.5+0.5;
                waterColor.rgb = lerp(waterColor.xyz,_FresnelColor.xyz,fresnelN);
//Water Reflect
                float4 reflectTex = SAMPLE_TEXTURE2D(_ReflectMap,sampler_ReflectMap,reflectVec.xz* _ReflectMap_ST.xy+finalNormal.xy*0.05 +_ReflectMap_ST.zw);
                waterColor.rgb += reflectTex.xyz*(fresnel+_ReflectAlpha);
//water Under
                float3 underTex = SampleSceneColor(i.screenPos.xy/i.screenPos.w);//
                waterColor.rgb = lerp(underTex.rgb,waterColor.rgb,waterColor.a);
//Water Caustics
                float2 worldUV = worldPos.xz;
                float3 causticsTex = SAMPLE_TEXTURE2D(_CausticsMap,sampler_CausticsMap,worldUV.xy*_CausticsMap_ST.xy+_CausticsMap_ST.zw + finalNormal.xy+float2(_Time.y/10,_Time.y/10)).rgb;
                waterColor.rgb+=causticsTex*(1-waterColor.a)*_CausticsAlpha;
//Water White Edge
                //white edge
                float edge = pow(1-clamp(waterColor.a,_WhiteEdgeRamp,1),_WhiteEdgePow);
                waterColor=lerp(waterColor,_WhiteEdgeColor,edge);
                
//Water foam
                float fomaMap1 =  SAMPLE_TEXTURE2D(_FoamTex,sampler_FoamTex,i.worldPos.xz*_FoamTex_ST.xy+_FoamTex_ST.zw).x;
                float foamRampRange = saturate(pow(rampRange,_FoamRamp));
                float foamDepth = saturate(foamRampRange)*15+fomaMap1-_Time.y*_FoamSpeed;
                float foamRange = saturate(sin(foamDepth)-_FoamWidth-fomaMap1)*smoothstep(max(_FoamClip,0),1,rampRange);//*step(_FoamClip,rampRange)
                foamRange *=(1-foamRampRange);
                waterColor.rgb+=foamRange; 
                
//Water Spec
                float3 spec = pow(saturate(dot(reflectL,V)),_SpecPow)*_SpecSize;
                waterColor.rgb+=spec;
                waterColor.rgb = MixFog(waterColor.rgb,i.worldPos.w);
                // waterColor.rgb = controlColor;
                return float4(waterColor.rgb,1);
            }
            ENDHLSL
        }
    }
}
