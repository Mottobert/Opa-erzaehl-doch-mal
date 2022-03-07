// Main Light
void MainLight_half(float3 WorldPos, out half3 Direction, out half3 Color)
{
#if SHADERGRAPH_PREVIEW
   Direction = half3(0.5, 0.5, 0);
   Color = 1;
#else
#if SHADOWS_SCREEN
   half4 clipPos = TransformWorldToHClip(WorldPos);
   half4 shadowCoord = ComputeScreenPos(clipPos);
#else
   half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
   Light mainLight = GetMainLight(shadowCoord);
   Direction = mainLight.direction;
   Color = mainLight.color;
#endif
}

// Additional Lights
void AdditionalLights_half(half3 WorldPosition, out half3 Direction, out half3 Color)
{
#if SHADERGRAPH_PREVIEW
   Direction = half3(0.5, 0.5, 0);
   Color = 1;
#else
#if SHADOWS_SCREEN
   half4 clipPos = TransformWorldToHClip(WorldPosition);
   half4 shadowCoord = ComputeScreenPos(clipPos);
#else
   half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
#endif
   int pixelLightCount = GetAdditionalLightsCount();
   for (int i = 0; i < pixelLightCount; ++i)
   {
       Light light = GetAdditionalLight(i, WorldPosition);
       Direction  = light.direction;
       Color = light.color;
   }
#endif
}