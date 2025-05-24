Shader "Unlit/VolumetricFogShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1 ,1)
        _MaxDistance("Max Distance", float) = 200
        _StepSize("Step Size", Range(0.1, 20)) = 1
        _DensityMultiplyer("Density Multiplyer", Range(0, 10)) =1
        _NoiseOffset("Noise Offset", float) = 0
        
        _FogNoise("Fog Noise", 3D) = "white"{}
        _NoiseTiling("Noise Tiling", float) = 1
        _DensityThreshold("Density Threshold", Range(0, 1)) = 0.1
        _LightScattering("Light Scattering", Range(0, 1)) = 0.2
        
        [HDR] _LightContribution("Light Contribution", Color) = (1,1,1,1) 
    }
    SubShader
    {
        Tags {"RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOW_SCREEN

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"


            float _MaxDistance;
            float _StepSize;
            float _DensityMultiplyer;
            float _NoiseOffset;

            TEXTURE3D(_FogNoise);
            float _DensityThreshold;
            float _NoiseTiling;
            float _LightScattering;    

            
            float4 _Color;
            float4 _LightContribution;
            


            float henyey_greenstein(float angle, float scattering)
            {
                return (1.0 - angle * angle) / (4.0 * PI * pow(1.0 + scattering * scattering - (2.0 * scattering) * angle, 1.5f));
            }
            
            float get_density(float3 worldPos)
            {
                float4 noise = _FogNoise.SampleLevel(sampler_TrilinearRepeat, worldPos * 0.01 * _NoiseTiling, 0);
                float density = dot(noise, noise);
                density = saturate(density - _DensityThreshold) * _DensityMultiplyer;
                return density;
            }


            half4 frag (Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
                float4 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, IN.texcoord);
                
                float depth = SampleSceneDepth(IN.texcoord);
                float3 worldPos = ComputeWorldSpacePosition(IN.texcoord, depth, UNITY_MATRIX_I_VP);

                float3 entryPoint = _WorldSpaceCameraPos;
                float3 viewDir = worldPos - _WorldSpaceCameraPos;
                float viewLength = length(viewDir);
                float3 rayDir = normalize(viewDir);

                float2 pixelCoords = IN.texcoord * _BlitTexture_TexelSize.zw;
                float distLimit = min(viewLength, _MaxDistance);
                float distTravelled = InterleavedGradientNoise(pixelCoords, int(_Time.y / max(HALF_EPS, unity_DeltaTime.x))) * _NoiseOffset;
                float transmittance = 1;
                float4 fogCol = _Color;

                while(distTravelled < distLimit)
                {
                    float3 rayPos = entryPoint + rayDir * distTravelled;
                    
                    float density = get_density(rayPos);
                    Light mainLight = GetMainLight(TransformWorldToShadowCoord(rayPos));
                    
                    if(mainLight.shadowAttenuation > .8 && rayPos.x > -2 && rayPos.x < 3)
                    {
                        Light mainLight = GetMainLight(TransformWorldToShadowCoord(rayPos));
                        //fogCol.rgb +=  _LightContribution.rgb * mainLight.color.rgb *henyey_greenstein(dot(rayDir, mainLight.direction), _LightScattering) * density * _StepSize * mainLight.shadowAttenuation;
                       
                        fogCol.rgb +=  _LightContribution.rgb * mainLight.color.rgb *henyey_greenstein(dot(rayDir, mainLight.direction), _LightScattering)  * _StepSize * mainLight.shadowAttenuation;
                        transmittance *= exp(-density * _StepSize);
                    }
                    distTravelled += _StepSize;
                }

                
                
                return lerp(col, fogCol, 1.0 - saturate(transmittance));
            }
            
            ENDHLSL
        }
    }
}
