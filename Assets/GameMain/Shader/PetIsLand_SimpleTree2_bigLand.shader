Shader "URP/PetIsLand_SimpleTree_BigLand"
{
    Properties
    {
        [Enum(Both, 0, Back, 1,Front , 2)] _Cull ("Render Face", Float) = 0
        //  _NoiseMap("NoiseMap", 2D) = "white" {}

        [Header(Maps)][Space(10)][NoScaleOffset][MainTexture]_MainTex("ChangeMap", 2D) = "white" {}

        //_ChangeMap("ChangeMap",2D)="black"{}
        _leafColor2("树叶颜色(浅)",Color)=(1,1,1,1)
        _leafColor1("树叶颜色(深)",Color)=(1,1,1,1)
//        _branchColor1("树枝颜色(浅)",Color)=(1,1,1,1)
//        _branchColor2("树枝颜色(深)",Color)=(0,0,0,1)
//        
        _BlendColor("树颜色混合",Range(0,1))=0
        _leafUpColor("树叶上颜色",Color)=(1,1,1,1)
        _leafDownColor("树叶下颜色",Color)=(1,1,1,1)
        [Hdeader(Emission)][Space(30)]
        _Emission("自发光颜色",Color)=(0,0,0,0)
        _BackEmission("阴影部分自发光颜色",Color)=(0,0,0,0)

        //        _BackColor("背部颜色",Color)=(0,0,0,0)

        [Hdeader(Fresnel)][Space(30)]
        _FresnelColor("FresnelColor",Color)=(1,1,1,1)
        _FresnelPow("FresnelPow",Float)=1
        _FresnelSize("FresnelSize",Float)=1

        _AO("AO",Range(0,1))=0
        _AOPow("AOPow",Float)=0


        _SoftShadow("软化灰部阴影强度",Range(0,2))=0

        _FireEdgeWidth("燃烧亮边宽度",Range(0,1))=0
        [HDR]_FireEdgeColor("燃烧亮边颜色",Color)=(1,1,1,1)
        _FireRange("燃烧范围",Range(0,1)) = 0
        _FireColor("燃烧颜色",Color)=(1,1,1,1)
        _MaskMap("燃烧MaskMap",2D) = "white"{}

        _AlphaCutoff("Alpha Cutoff", Range( 0 , 1)) = 0.35
//        _FadeMaxDistance("FadeMaxDistance",Float)=10
        _PlayerDistance("PlayerDistance",Float)=10
        [Header(Wind)][Space(5)][Toggle(_ENABLEWIND_ON)] _EnableWind("Enable", Float) = 1
        _Speed("WindSpeed",Float) =1
        _WindStrength("WindStrength",Float)=1 


        _Alpha("Alpha",Float)=1
    
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
        LOD 200
//           Stencil
//        {
//            Ref 1
//         
//            Comp Always
//            Pass Replace
//         
//        }

        //Blend One Zero, One Zero

        HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitForwardPass.hlsl"
        CBUFFER_START(UnityPerMaterial)

        half4 _FireColor;

        half _WindStrength;
        half _WindSpeed;
        half _AlphaCutoff;

        half _Speed;

        half _FireRange;
        half4 _Emission, _BackEmission;
        half4 _leafColor1, _leafColor2;
        //half4 _branchColor1, _branchColor2;
        half _BlendColor;
        half _FireEdgeWidth;
        half4 _FireEdgeColor;
        
        half4 _FresnelColor;
        half _FresnelPow;
        half _FresnelSize;
        half4 _BackColor;
        half _AO, _AOPow;
        half _SoftShadow;
        half3 _PlayerPos;//transmit player position
        half _PlayerDistance;

        half4 _leafUpColor;
        half4 _leafDownColor;
        
        CBUFFER_END
        TEXTURE2D(_MainTex);      SAMPLER(sampler_MainTex);
     
        TEXTURE2D(_MaskMap);      SAMPLER(sampler_MaskMap);


        half ObjectPosRand01()
        {
            return frac(UNITY_MATRIX_M[0][3] + UNITY_MATRIX_M[1][3] + UNITY_MATRIX_M[2][3]);
        }
        ENDHLSL
        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            Cull[_Cull]
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            //make fog work
            #pragma multi_compile _RECEIVE_SHADOWS
            #pragma multi_compile _ _ENABLEWIND_ON
            #pragma multi_compile _ _SHADOW_MASK_ALWAYS
            #pragma multi_compile_instancing
            #pragma multi_compile_fog


            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS 

            
            struct appdata
            {
                float4 vertex : POSITION;
                half3 normal : NORMAL;
                half4 color :COLOR;
                half2 uv : TEXCOORD0;
                half2 uv2:TEXCOORD1;
                half2 lightMapUV : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 positionCS_SS : SV_POSITION;
                float4 positionWS : VAR_POSITION;
                half4 baseUV : VAR_BASE_UV;
                half4 normalWS : VAR_NORMAL;
                half4 color :COLOR;
                half3 center :TEXCOORD3;
                half2 uv2:TEXCOORD4;
                float4 screenPos:TEXCOORD2;
            };

            v2f vert(appdata v)
            {
                v2f output = (v2f)0;
                UNITY_SETUP_INSTANCE_ID(v);

                output.positionWS.xyz = TransformObjectToWorld(v.vertex.xyz);
                output.color = v.color;
                output.baseUV.xy = v.uv;
                output.normalWS.xyz = normalize(TransformObjectToWorldNormal(v.normal.xyz));
                output.normalWS.w =1;
                #ifdef _ENABLEWIND_ON
                half WindLeaves = 0;
                half WindStrength = 0;

                half posOffset = v.color.g * 100; //ObjectPosRand01();
                half gradientNoise = (sin(_Speed * (_Time.y + posOffset)) * 0.5 + 0.5) * pow(v.vertex.y, 2.0f);
                half Wind = gradientNoise * _WindStrength / 10000;

                output.positionWS.x += Wind;
                output.positionWS.z += Wind;
                #endif

                output.positionCS_SS = TransformWorldToHClip(output.positionWS.xyz);
                output.baseUV.z = ComputeFogFactor(output.positionCS_SS.z);
                // #if defined(_RECEIVE_SHADOWS)
                output.baseUV.w =1;
                // #endif
                output.uv2 = v.uv2;

              
               
          
                return output;
            }

            half4 frag(v2f i, int face:VFACE) : SV_Target
            {
                
//Diffuse
                //half4 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.baseUV.xy);
//Change Map
                half4 changeTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.baseUV.xy);
                half4 leafCol = lerp(_leafColor1, _leafColor2, changeTex.r);
                half4 finalalbedo = leafCol;
                half3 blendColor = lerp(_leafDownColor,_leafUpColor,i.uv2.y);
                finalalbedo.rgb = lerp(finalalbedo,blendColor,_BlendColor);
//Match Vector
                half3 ViewDir = normalize(_WorldSpaceCameraPos.xyz - i.positionWS);
                half3 N = normalize(i.normalWS.xyz);
                half3 L = normalize(_MainLightPosition.xyz);
           //     half3 H = normalize(ViewDir + L);
                float NdotL = saturate(dot(N, L));
              float NdotLLine =NdotL * 0.5 + 0.5;
                half3 LColor = _MainLightColor.rgb;
//Fresnel
                half3 fresnel = pow(1 - saturate(dot(N, ViewDir)), _FresnelPow) * _FresnelSize * +_FresnelColor;
//Combine-------------------------------------------Start
                half4 finalcolor=0;
//Combine Atten
            
                half atten;
                // ShadowData shadowData = GetShadowMapData(i.normalWS.w);
                // Light light = GetDirectionalShadowMapLight(half(1.0),i.positionWS.xyz,N,shadowData);
                float4  shadowCoord =TransformWorldToShadowCoord(i.positionWS);
                half realtimeShadow = MainLightRealtimeShadow(shadowCoord);
                
                atten = saturate(realtimeShadow + NdotLLine * _SoftShadow);
                NdotL = atten*NdotL;
               
//Combine Lambrt
                finalcolor = lerp(finalalbedo, finalalbedo * _BackEmission, 1 - (NdotL));
                finalcolor.rgb *= LColor;
//Combine IBL
                half3 gi = SampleSH(i.normalWS);
                //gi = SampleSHVertex(i.normalWS);
                finalcolor.rgb += gi * finalalbedo;
//Combine Fresnel                
                finalcolor.rgb += fresnel  * NdotL ;
//Combien Emission
                finalcolor.rgb += lerp(_Emission * _BackEmission, _Emission, NdotL)  * changeTex.g;
//Combine Brun                
               // finalcolor = lerp(finalcolor, finalcolor * _FireColor, _FireRange)+edgeColor;
//Combine AO
                half AO = i.uv2.x*_AOPow;
                finalcolor.rgb = lerp(0,finalcolor.rgb,AO+_AO);
//Combine Fog
                finalcolor.rgb = MixFog(finalcolor.rgb, i.baseUV.z);
                half Alpha = changeTex.a;
                clip(Alpha - _AlphaCutoff);
//Clip Alpha
                // if (i.screenPos.z <0)
                // {
                //     if(distance(i.center.xyz,_PlayerPos)<_PlayerDistance)
                //     {
                //         ditherClip(i.screenPos.xy / i.screenPos.w, saturate(1+i.screenPos.z)+0.5); 
                //     }
                // }
                // return float4(i.color.rgb,1);
                return half4(finalcolor.rgb, 1.0h);
            }
            ENDHLSL
        }
        pass
        {
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
            ColorMask 0
            Cull Off
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ _ENABLEWIND_ON
            //   #pragma shader_feature_local _ALPHATEST_ON
            //   float4 _ShadowBias; 
            //  float3 _LightDirection;


            struct appdata
            {
                //float4 vertex : POSITION;
                float4 positionOS : POSITION;
                // float3 normalOS     : NORMAL;
                half4 color:COLOR;
                half2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                half2 uv : TEXCOORD0;
                float4 positionCS : SV_POSITION;
            };

            float4 GetShadowPositionHClip(appdata v)
            {
                float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);
                #ifdef _ENABLEWIND_ON


                half WindLeaves = 0;
                half WindStrength = 0;

                half posOffset = v.color.g * 100; //ObjectPosRand01();
                half gradientNoise = (sin(_Speed * (_Time.y + posOffset)) * 0.5 + 0.5) * pow(v.positionOS.y, 2.0f);
                half Wind = gradientNoise * _WindStrength / 10000;


                //==========================
                positionWS.x += Wind;
                positionWS.z += Wind;
                #endif
                float4 positionCS = TransformWorldToHClip(positionWS);

                return positionCS;
            }

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;
                UNITY_SETUP_INSTANCE_ID(v);
                o.uv = v.uv.xy;
                o.positionCS = GetShadowPositionHClip(v);
                return o;
            }

            half4 frag(v2f i) : SV_TARGET
            {
                half4 changeTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
                half Alpha = changeTex.a;
                half4 maskMap = SAMPLE_TEXTURE2D(_MaskMap, sampler_MaskMap, i.uv.xy);


                clip(maskMap.r - _FireRange);

                // half AlphaClipThreshold = 0.35;
                half Alphaclip = 0.0;
                //   #ifdef _ALPHATEST_ON
                Alphaclip = (Alpha - _AlphaCutoff);
                // #endif
                clip(Alphaclip);
                return 0;
            }
            ENDHLSL
        }

    }


}