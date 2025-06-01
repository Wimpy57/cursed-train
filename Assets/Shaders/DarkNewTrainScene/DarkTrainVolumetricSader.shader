Shader "Unlit/DarkTrainVolumetricShader"
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


            half _MaxDistance;
            half _StepSize;
            half _DensityMultiplyer;
            half _NoiseOffset;

            TEXTURE3D(_FogNoise);
            half _DensityThreshold;
            half _NoiseTiling;
            half _LightScattering;    

            
            half4 _Color;
            half4 _LightContribution;
            


            half henyey_greenstein(half angle, half scattering)
            {
                return (1.0 - angle * angle) / (4.0 * PI * pow(1.0 + scattering * scattering - (2.0 * scattering) * angle, 1.5f));
            }
            
            half get_density(half3 worldPos)
            {
                half4 noise = _FogNoise.SampleLevel(sampler_TrilinearRepeat, worldPos * 0.01 * _NoiseTiling, 0);
                half density = dot(noise, noise);
                density = saturate(density - _DensityThreshold) * _DensityMultiplyer;
                return density / 10;
            }


            half4 frag (Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
                half4 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, IN.texcoord);
                
                half depth = SampleSceneDepth(IN.texcoord);
                half3 worldPos = ComputeWorldSpacePosition(IN.texcoord, depth, UNITY_MATRIX_I_VP);

                half3 entryPoint = _WorldSpaceCameraPos;
                half3 viewDir = worldPos - _WorldSpaceCameraPos;
                half viewLength = length(viewDir);
                half3 rayDir = normalize(viewDir);

                half2 pixelCoords = IN.texcoord * _BlitTexture_TexelSize.zw;
                half distLimit = min(viewLength, _MaxDistance);
                half distTravelled = InterleavedGradientNoise(pixelCoords, int(_Time.y / max(HALF_EPS, unity_DeltaTime.x))) * _NoiseOffset;
                half transmittance = 1;
                half4 fogCol = _Color;

                while(distTravelled < distLimit)
                {
                    half3 rayPos = entryPoint + rayDir * distTravelled;
                    
                    half density = get_density(rayPos);
                    Light mainLight = GetMainLight(TransformWorldToShadowCoord(rayPos));
                    
                    if(rayPos.x > -2 && rayPos.x < 3 && rayPos.y < 3.5 && rayPos.y > 0 && rayPos.z > -36 && rayPos.z < 11 && mainLight.shadowAttenuation > .8)
                    {
                        fogCol.rgb +=  _LightContribution.rgb * mainLight.color.rgb *henyey_greenstein(dot(rayDir, mainLight.direction), _LightScattering) * density * _StepSize * mainLight.shadowAttenuation;
                        //fogCol.rgb +=  _LightContribution.rgb   * _StepSize * mainLight.color.rgb*henyey_greenstein(dot(rayDir, mainLight.direction), _LightScattering) * mainLight.shadowAttenuation;
                        //fogCol = (1,1,1,1);
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
