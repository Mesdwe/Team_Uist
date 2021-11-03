Shader "LowPolyWater/CurveWater" {
Properties { 

	_BaseColor1 ("Base color", COLOR)  = ( .54, .95, .99, 0.5) 
	_BaseColor2("Base color", COLOR) = (.54, .95, .99, 0.5)

	_SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
    _Shininess ("Shininess", Float) = 10
	_ShoreTex ("Shore & Foam texture ", 2D) = "black" {} 
	 
	_InvFadeParemeter("Auto blend parameter (Edge, Shore, Distance scale)", Vector) = (0.2 ,0.39, 0.5, 1.0)

	_BumpTiling("Foam Tiling", Vector) = (1.0 ,1.0, -2.0, 3.0)
	_BumpDirection("Foam movement", Vector) = (1.0 ,1.0, -1.0, 1.0)
	_viewDepth("viewDepth",float) = 300


	_Foam ("Foam (intensity, cutoff)", Vector) = (0.1, 0.375, 0.0, 0.0) 
	[MaterialToggle] _isInnerAlphaBlendOrColor("Fade inner to color or alpha?", Float) = 0 

	_Intensity("Curve Intensity",Float) = 0.0001
	_Offset("Origin Offset",Vector) = (0,0,0)
}


CGINCLUDE 
	#include "RollInclude.cginc"

	#include "UnityCG.cginc" 
	#include "UnityLightingCommon.cginc" // for _LightColor0

	sampler2D _ShoreTex;
	sampler2D_float _CameraDepthTexture;
  
	uniform float4 _BaseColor1;  
	uniform float4 _BaseColor2;
    uniform float _Shininess;
	uniform float _viewDepth;
	 
	uniform float4 _InvFadeParemeter;
    
	uniform float4 _BumpTiling;
	uniform float4 _BumpDirection;
 
	uniform float4 _Foam; 
  	float _isInnerAlphaBlendOrColor; 
	#define VERTEX_WORLD_NORMAL i.normalInterpolator.xyz 
	float change;

	float _Intensity;
	float3 _Offset;

	struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
 
	
	struct v2f
	{
		float4 pos : SV_POSITION;
		float4 normalInterpolator : TEXCOORD0;
		float4 viewInterpolator : TEXCOORD1;
		float4 bumpCoords : TEXCOORD2;
		float4 screenPos : TEXCOORD3;
		float4 grabPassPos : TEXCOORD4; 
		half3 worldRefl : TEXCOORD6;
		float4 posWorld : TEXCOORD7;
        float3 normalDir : TEXCOORD8;

		UNITY_FOG_COORDS(5)
	}; 
 
	inline half4 Foam(sampler2D shoreTex, half4 coords) 
	{
		half4 foam = (tex2D(shoreTex, coords.xy) * tex2D(shoreTex,coords.zw)) - 0.125;
		return foam;
	}

	v2f vert(appdata_full v)
	{
		v2f o;
		UNITY_INITIALIZE_OUTPUT(v2f, o);

		
		half3 worldSpaceVertex = mul(unity_ObjectToWorld,(v.vertex)).xyz;
		half3 vtxForAni = (worldSpaceVertex).xzz;
 
		half3	offsets = half3(0,0,0);
		half3	nrml = half3(0,1,0);
		
		v.vertex.xyz += offsets;
		 
		half2 tileableUv = mul(unity_ObjectToWorld,(v.vertex)).xz;
		
		o.bumpCoords.xyzw = (tileableUv.xyxy + _Time.xxxx * _BumpDirection.xyzw ) * _BumpTiling.xyzw;

		o.viewInterpolator.xyz = worldSpaceVertex - _WorldSpaceCameraPos;
        o.pos = GetFixedRollClipPos(v.vertex,_Intensity,_Offset);

		//o.pos = UnityObjectToClipPos(v.vertex);
		o.screenPos=ComputeScreenPos(o.pos); 
		o.normalInterpolator.xyz = nrml;
		o.viewInterpolator.w = saturate(offsets.y);
		o.normalInterpolator.w = 1; 
		
		UNITY_TRANSFER_FOG(o,o.pos);
 		half3 worldNormal = UnityObjectToWorldNormal(v.normal); 
   		float4x4 modelMatrix = unity_ObjectToWorld;
        float4x4 modelMatrixInverse = unity_WorldToObject; 
	 	o.posWorld = mul(modelMatrix, v.vertex);
        o.normalDir = normalize( mul(float4(v.normal, 0.0), modelMatrixInverse).xyz); 

        float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
        float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos)); 
        o.worldRefl = reflect(-worldViewDir, worldNormal);

		return o;
	}
 
	 half4 calculateBaseColor(v2f input)  
         {
            float3 normalDirection = normalize(input.normalDir);
 
            float3 viewDirection = normalize(
               _WorldSpaceCameraPos - input.posWorld.xyz);
            float3 lightDirection;
            float attenuation;
 
            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1.0; // no attenuation
               lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               float3 vertexToLightSource = 
                  _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; // linear attenuation 
               lightDirection = normalize(vertexToLightSource);
            }
 
			change = ((input.posWorld.z / _viewDepth) + 1) / 2;
			
			float3 ambientLighting = 
               UNITY_LIGHTMODEL_AMBIENT.rgb * lerp(_BaseColor1.rgb, _BaseColor2.rgb,change);
 
            float3 diffuseReflection = 
               attenuation * _LightColor0.rgb * lerp(_BaseColor1.rgb, _BaseColor2.rgb, change)
               * max(0.0, dot(normalDirection, lightDirection));
 
            float3 specularReflection;
            if (dot(normalDirection, lightDirection) < 0.0) 
               // light source on the wrong side?
            {
               specularReflection = float3(0.0, 0.0, 0.0); 
                  // no specular reflection
            }
            else  
            {
               specularReflection = attenuation * _LightColor0.rgb  * _SpecColor.rgb * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
            }

            return half4(ambientLighting + diffuseReflection  + specularReflection, 1.0);
         }

	half4 frag( v2f i ) : SV_Target
	{ 
 
		half4 edgeBlendFactors = half4(1.0, 0.0, 0.0, 0.0);
		
		#ifdef WATER_EDGEBLEND_ON
			half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
			depth = LinearEyeDepth(depth);
			edgeBlendFactors = saturate(_InvFadeParemeter * (depth-i.screenPos.w));
			edgeBlendFactors.y = 1.0-edgeBlendFactors.y;
		#endif
		
 
        half4 baseColor = calculateBaseColor(i);
       
 
		half4 foam = Foam(_ShoreTex, i.bumpCoords * 2.0);
		baseColor.rgb += foam.rgb * _Foam.x * (edgeBlendFactors.y + saturate(i.viewInterpolator.w - _Foam.y));
		if( _isInnerAlphaBlendOrColor==0)
			baseColor.rgb += 1.0-edgeBlendFactors.x;
		if(  _isInnerAlphaBlendOrColor==1.0)
			baseColor.a  =  edgeBlendFactors.x;
		UNITY_APPLY_FOG(i.fogCoord, baseColor);
		return baseColor;
	}
	
ENDCG

Subshader
{
	Tags {"RenderType"="Transparent" "Queue"="Transparent"}
	
	Lod 500
	ColorMask RGB
	
	GrabPass { "_RefractionTex" }
	
	Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Cull Off
		
			CGPROGRAM
		
			#pragma target 3.0
		
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
		
			#pragma multi_compile WATER_EDGEBLEND_ON WATER_EDGEBLEND_OFF 
		
			ENDCG
	}
	        Pass {    
            Tags { "LightMode" = "ForwardAdd" } 
            // pass for additional light sources
            Blend One One // additive blending 
            
            CGPROGRAM
            
            #pragma multi_compile_lightpass
            
            #pragma vertex vert  
            #pragma fragment frag 
            
//            #include "UnityCG.cginc"
            //#include "RollInclude.cginc"
            //uniform float4 _LightColor0; 
            // color of light source (from "Lighting.cginc")
            uniform float4x4 unity_WorldToLight; // transformation 
            // from world to light space (from Autolight.cginc)
            #if defined (DIRECTIONAL_COOKIE) || defined (SPOT)
                uniform sampler2D _LightTexture0; 
                // cookie alpha texture map (from Autolight.cginc)
            #elif defined (POINT_COOKIE)
                uniform samplerCUBE _LightTexture0; 
                // cookie alpha texture map (from Autolight.cginc)
            #endif
            
            // User-specified properties
            uniform float4 _Color; 
            //uniform float4 _SpecColor; 
            //uniform float _Shininess;
            //            float _Intensity;
            //float3 _Offset;
            struct vertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct vertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                // position of the vertex (and fragment) in world space 
                float4 posLight : TEXCOORD1;
                // position of the vertex (and fragment) in light space
                float3 normalDir : TEXCOORD2;
                // surface normal vector in world space
            };
            
            vertexOutput vert(vertexInput input) 
            {
                vertexOutput output;
                
                float4x4 modelMatrix = unity_ObjectToWorld;
                float4x4 modelMatrixInverse = unity_WorldToObject;
                output.pos = GetFixedRollClipPos(input.vertex,_Intensity,_Offset);
                
                output.posWorld = mul(modelMatrix, input.vertex);
                //output.posWorld = mul(modelMatrix, input.vertex);
                
                output.posWorld = GetFixedRollWorldPos(output.posWorld,_Intensity,_Offset);
                output.posLight = mul(unity_WorldToLight, output.posWorld);
                output.normalDir = normalize(
                mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
                //output.pos = UnityObjectToClipPos(input.vertex);
                return output;

            }
            
            float4 frag(vertexOutput input) : COLOR
            {
                float3 normalDirection = normalize(input.normalDir);
                
                float3 viewDirection = normalize(
                _WorldSpaceCameraPos - input.posWorld.xyz);
                float3 lightDirection;
                float attenuation = 1.0;
                // by default no attenuation with distance

                #if defined (DIRECTIONAL) || defined (DIRECTIONAL_COOKIE)
                    lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                #elif defined (POINT_NOATT)
                    lightDirection = normalize(
                    _WorldSpaceLightPos0 - input.posWorld.xyz);
                #elif defined(POINT)||defined(POINT_COOKIE)||defined(SPOT)
                    float3 vertexToLightSource = 
                    _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
                    float distance = length(vertexToLightSource);
                    attenuation = 1.0 / distance; // linear attenuation 
                    lightDirection = normalize(vertexToLightSource);
                #endif
                
                float3 diffuseReflection = 
                attenuation * _LightColor0.rgb * _Color.rgb
                * max(0.0, dot(normalDirection, lightDirection));
                
                float3 specularReflection;
                if (dot(normalDirection, lightDirection) < 0.0) 
                // light source on the wrong side?
                {
                    specularReflection = float3(0.0, 0.0, 0.0); 
                    // no specular reflection
                }
                else // light source on the right side
                {
                    specularReflection = attenuation * _LightColor0.rgb 
                    * _SpecColor.rgb * pow(max(0.0, dot(
                    reflect(-lightDirection, normalDirection), 
                    viewDirection)), _Shininess);
                }
                
                float cookieAttenuation = 1.0; 
                // by default no cookie attenuation
                #if defined (DIRECTIONAL_COOKIE)
                    cookieAttenuation = tex2D(_LightTexture0, 
                    input.posLight.xy).a;
                #elif defined (POINT_COOKIE)
                    cookieAttenuation = texCUBE(_LightTexture0, 
                    input.posLight.xyz).a;
                #elif defined (SPOT)
                    cookieAttenuation = tex2D(_LightTexture0, 
                    input.posLight.xy / input.posLight.w 
                    + float2(0.5, 0.5)).a;
                #endif

                return float4(cookieAttenuation 
                * (diffuseReflection + specularReflection), 1.0);
            }
            
            ENDCG
        }
}


Fallback "Transparent/Diffuse"
}
