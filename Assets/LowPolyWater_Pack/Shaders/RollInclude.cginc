uniform float3 _PlayerPos;
uniform float _RollStrength;

inline float4 GetZRollDist(float3 worldPos)
{
    return pow(worldPos.z - _PlayerPos.z, 2);
}

inline float4 GetXZRollDist(float3 worldPos)
{
    float2 tempFloat2 = pow(worldPos.xz - _PlayerPos.xz, 2);
    return tempFloat2.x + tempFloat2.y;
}

inline float4 GetFixedRollClipPos(float4 objVertex,float intensity,float3 offset) {
    float3 worldPos = mul(unity_ObjectToWorld, objVertex);
    float dist2Player = GetXZRollDist(worldPos+offset);
    _RollStrength = intensity;
    worldPos.y -= dist2Player * _RollStrength;

    return mul(UNITY_MATRIX_VP, float4(worldPos, 1));
}

inline float4 GetFixedRollWorldPos(float4 worldPos,float intensity,float3 offset) {
    float dist2Player = GetXZRollDist(worldPos+offset);
    _RollStrength = intensity;
    worldPos.y -= dist2Player * _RollStrength;

    return worldPos;
}