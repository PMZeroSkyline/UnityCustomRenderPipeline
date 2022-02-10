#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

#include "BRDF.hlsl"
#include "Shadows.hlsl"
#include "GI.hlsl"

float3 IncomingLight (Surface surface, Light light) {
	return saturate(dot(surface.normal, light.direction) * light.attenuation) * light.color;
}
float3 GetLighting (Surface surface, BRDF brdf, Light light) {
    return IncomingLight(surface, light) * DirectBRDF(surface, brdf, light);
}
Light GetDirectionalLight (int index, Surface surfaceWS, ShadowData shadowData)
{
    Light light;
    light.color = _DirectionalLightColors[index].rgb;
    light.direction = _DirectionalLightDirections[index].xyz;
    DirectionalShadowData dirShadowData = GetDirectionalShadowData(index, shadowData);
    light.attenuation = GetDirectionalShadowAttenuation(dirShadowData, shadowData, surfaceWS);
    return light;
}
float3 GetLighting (Surface surfaceWS, BRDF brdf, GI gi)
{
    ShadowData shadowData = GetShadowData(surfaceWS);

    shadowData.shadowMask = gi.shadowMask;

    float3 color = gi.diffuse * brdf.diffuse;
    for (int i = 0; i < GetDirectionLightCont(); i++)
    {
        Light light = GetDirectionalLight(i, surfaceWS, shadowData);
        color += GetLighting(surfaceWS, brdf, light);
    }
   
    return color;
}
#endif