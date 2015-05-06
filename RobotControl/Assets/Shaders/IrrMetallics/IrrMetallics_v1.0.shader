//Copyright© 2012 Jorge Pinal N. for the Irreverent Software™ Team.
//This shader can not be used outside the Unity Engine, and can not be edited, modified or altered in any way. You can not modify this copyright notice.
//Shader provided by the Irreverent Software™ Team. You can know more reading the EULA and the Credits.txt File provided with this package.

Shader "IrreverentShaders/IrrMetallics_v1.0"
{
	Properties 
	{
_MainTexture("_MainTexture", 2D) = "black" {}
_MainColor("_MainColor", Color) = (1,1,1,1)
_NormalMap("_NormalMap", 2D) = "black" {}
_Shininess("_Shininess", Range(0,1) ) = 0.5
_Specularity("_Specularity", Range(0,1) ) = 0.5
_GlossMask("_GlossMask", 2D) = "black" {}
_Reflections("_Reflections", Cube) = "black" {}
_Reflective("_Reflective", Range(0,1) ) = 0.5

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _MainTexture;
float4 _MainColor;
sampler2D _NormalMap;
float _Shininess;
float _Specularity;
sampler2D _GlossMask;
samplerCUBE _Reflections;
float _Reflective;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_MainTexture;
float2 uv_NormalMap;
float3 viewDir;
float2 uv_GlossMask;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Sampled2D0=tex2D(_MainTexture,IN.uv_MainTexture.xy);
float4 Multiply0=_MainColor * Sampled2D0;
float4 Sampled2D1=tex2D(_NormalMap,IN.uv_NormalMap.xy);
float4 UnpackNormal0=float4(UnpackNormal(Sampled2D1).xyz, 1.0);
float4 TexCUBE0=texCUBE(_Reflections,float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ));
float4 Multiply2=TexCUBE0 * TexCUBE0.aaaa;
float4 Multiply3=_Reflective.xxxx * Multiply2;
float4 Sampled2D2=tex2D(_GlossMask,IN.uv_GlossMask.xy);
float4 Multiply1=Sampled2D2.aaaa * _Shininess.xxxx;
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Multiply0;
o.Normal = UnpackNormal0;
o.Emission = Multiply3;
o.Specular = Multiply1;
o.Gloss = _Specularity.xxxx;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}