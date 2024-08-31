#ifndef HOWELL_WATER_SELF
#define HOWELL_WATER_SELF
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
struct SceneDepth
{
    float raw;
    float linear01;
    float eye;
};
inline float3 m_UnityWorldToViewPos( in float3 pos )
{
    return mul(UNITY_MATRIX_V, float4(pos, 1.0)).xyz;
}

SceneDepth SampleDepth(float4 screenPos)
{
    SceneDepth depth = (SceneDepth)0;
	

    screenPos.xyz /= screenPos.w;

    depth.raw = SampleSceneDepth(screenPos.xy);
    depth.eye = LinearEyeDepth(depth.raw, _ZBufferParams);
    depth.linear01 = Linear01Depth(depth.raw, _ZBufferParams) ;

    return depth;
}




float3 ReconstructWorldPosition(float4 screenPos)
{
    //=================================
    screenPos.xyz /= screenPos.w;
    float depthGap = SampleSceneDepth(screenPos.xy);

    #if UNITY_REVERSED_Z
    depthGap = 1 - depthGap;
    #endif
    float3 depth_g1 = (float3(screenPos.x, screenPos.y, depthGap));
    float4 depth_g2 = (float4((depth_g1 * 2.0 - 1.0), 1.0)); //转换到NDC
    float4 depth_g3 = mul(unity_CameraInvProjection, depth_g2); //转换到View space
    float3 depth_g4 = (depth_g3.xyz / depth_g3.w);
    float3 localInvertDepthDir = depth_g4 * float3(1, 1, 1); //反向z
    float3 reposWS = mul(unity_MatrixInvV, float4(localInvertDepthDir, 1.0)).xyz; //转换到World space
    return reposWS;
}

float3 ReconstructWorldPosition_URP(float4 positionOS,float4 screenPos)
{
    float2 uv  = positionOS.xy / _ScaledScreenParams.xy;

    #if UNITY_REVERSED_Z
        real depth = SampleSceneDepth(uv);
    #else
        real depth = lerp(UNITY_NEAR_CLIP_VALUE,1,SampleSceneDepth(uv));
    #endif
    float3 worldPos = ComputeWorldSpacePosition(uv,depth,UNITY_MATRIX_I_VP);

    return worldPos;
    
}


float MF_ScatteringAquatic (float3 V, float3 L, float Intensity, float3 VertexNormal)
{
    float3 Flatten   = lerp(float3(1.2, 1.2, 0.7), float3(0.4, 0.4, 1.4), 0);
    float Scattering;
    Scattering  = pow((1.0 - V.y),2) * (1.0 + dot(L, V * float3(-1, 1, -1)));
    Scattering *= dot(VertexNormal, V * Flatten);
    Scattering *= Intensity;
    return Scattering;
}




#endif
